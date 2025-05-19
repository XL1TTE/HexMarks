using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Project.Data.CMS.Tags.Enemies;
using Project.DataResolving;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.TurnSystem;
using Project.Utilities;
using Project.Utilities.Extantions;
using Project.Wrappers;
using UnityEngine;
using UnityEngine.UI;
using XL1TTE.Animator;
using XL1TTE.GameActions;
using Zenject;

namespace Project.Game.Battle.Controllers
{
    public class BattleTurnsController: MonoBehaviour{
        
        [Inject]
        private void Construct(SignalBus signalBus, ContextResolver contextResolver){
            m_SignalBus = signalBus;
            m_ContextResolver = contextResolver;
        }
        void OnEnable()
        {
            m_SignalBus.Subscribe<BattleStageReadySignal>(BattleStageReadyProccess);
            
            m_SignalBus.Subscribe<HeroTurnSignal>(OnHeroTurnInteraction);
            m_SignalBus.Subscribe<EnemyTurnSignal>(OnEnemyTurnInteraction);
            
            m_SignalBus.Subscribe<EnemyDiedSignal>(OnEnemyDiedInteraction);
            m_SignalBus.Subscribe<HeroDiedSignal>(OnHeroDiedInteraction);

            m_EndTurnButton.onClick.AddListener(OnEndTurnButtonClicked);
        }
        void OnDisable()
        {
            m_SignalBus.Unsubscribe<BattleStageReadySignal>(BattleStageReadyProccess);
            
            m_SignalBus.Unsubscribe<HeroTurnSignal>(OnHeroTurnInteraction);
            m_SignalBus.Unsubscribe<EnemyTurnSignal>(OnEnemyTurnInteraction);

            m_SignalBus.Unsubscribe<EnemyDiedSignal>(OnEnemyDiedInteraction);
            m_SignalBus.Unsubscribe<HeroDiedSignal>(OnHeroDiedInteraction);

            m_EndTurnButton.onClick.RemoveListener(OnEndTurnButtonClicked);


            FreeTurnMarker();
        }

        void OnDestroy()
        {
            m_TurnsQueue = null;
            m_TurnMarker = null;
            
            m_MarkerTween = null;
            
            if(m_NextTurnProccessRoutine != null){
                m_NextTurnProccessRoutine.Kill();
                m_NextTurnProccessRoutine = null;
            }
            
            m_NextTurnProccessValidators = null;
        }

        private SignalBus m_SignalBus;    
        private ContextResolver m_ContextResolver;
        
                    
        [SerializeField] private Sprite m_TurnMarkerSprite;
        [SerializeField] private Vector3 m_TurnMarkerScale;

        [SerializeField] private Button m_EndTurnButton;

        private List<ITurnTaker> m_TurnsQueue;
        
        private SpriteRenderer m_TurnMarker;
        private Tween m_MarkerTween;
        
        // just to limit btn click spam
        private AwaitableCoroutine m_NextTurnProccessRoutine;
    
        private List<Func<bool>> m_NextTurnProccessValidators = new();
        

        public void AddNextTurnValidator(Func<bool> awaiter) =>
            m_NextTurnProccessValidators.Add(awaiter);
        public void RemoveNextTurnValidator(Func<bool> awaiter) =>
            m_NextTurnProccessValidators.Remove(awaiter);


        private void BattleStageReadyProccess(BattleStageReadySignal signal)
        {
            FreeTurnMarker();
            m_TurnsQueue = CreateTurnQueue(signal.Stage);
            if (m_NextTurnProccessRoutine == null)
            {
                m_NextTurnProccessRoutine = new AwaitableCoroutine(this, ProccessNextTurn());
            }
        }

        private void OnHeroTurnInteraction(HeroTurnSignal signal)
        {
            // Make it able to end turn.
            m_EndTurnButton.interactable = true;
            
            var hero = signal.hero;

            MarkTurnTaker(hero.m_view.gameObject);
            
            hero.RequestCardsDraw();
            
            m_SignalBus.SendSignal(new RequestCardsDraggingStateSwitchSignal(true));
        }

        private void OnEnemyTurnInteraction(EnemyTurnSignal signal)
        {
            StartCoroutine(OnEnemyTurnRoutine(signal));
        }

        private IEnumerator OnEnemyTurnRoutine(EnemyTurnSignal signal)
        {
            FreeTurnMarker();

            var enemy = signal.GetEnemy();
            var enemyModel = enemy.GetState().GetModel();

            if (enemyModel.Is<TagOnTurnAbilities>(out var onTurn))
            {

                enemy.StopIdleAnimation();
                
                yield return onTurn.ExecuteOnTurnAbilities(enemy, m_ContextResolver);
                
                enemy.StartIdleAnimation();

            }

            yield return ProccessNextTurn();
        }

        private void OnHeroDiedInteraction(HeroDiedSignal signal)
        {
            var hero = signal.hero;

            m_TurnsQueue.TryRemove((t) => t is HeroTurnTaker tt && tt.GetHero() == hero);
        }

        private void OnEnemyDiedInteraction(EnemyDiedSignal signal){
            var enemy = signal.GetEnemy();

            m_TurnsQueue.TryRemove((t) => t is EnemyTurnTaker tt && tt.GetEnemy() == enemy);
        }

        private IEnumerator ProccessNextTurn()
        {
            yield return new WaitForEndOfFrame();
            
            if (m_TurnsQueue.Count == 0) { yield break; }
            
            if(!m_TurnsQueue.Any((t) => t is HeroTurnTaker)) {yield break;}
            
            FreeTurnMarker();

            m_EndTurnButton.interactable = false;

            while (m_NextTurnProccessValidators.Any(v => !v.Invoke()))
            {
                yield return null;
            }
            
            yield return new WaitForSeconds(0.15f);
            
            var turnTaker = m_TurnsQueue.Dequeue();
            
            // Adds as last in turn cycle.
            m_TurnsQueue.Enqueue(turnTaker);
            
            // Send eather HeroTurnSignal or EnemyTurnSignal.
            turnTaker.SendTurnNotification();
            
            // Nullify next turn routine because we done.
            m_NextTurnProccessRoutine = null;
        }

        private List<ITurnTaker> CreateTurnQueue(BattleStage battleStage)
        {
            var enemies = battleStage.GetEnemies();
            var heroes = battleStage.GetHeroes();

            var turnTakers = new List<ITurnTaker>();

            foreach (var e in enemies)
            {
                turnTakers.Add(new EnemyTurnTaker(m_SignalBus, e));
            }
            
            foreach(var h in heroes){
                turnTakers.Add(new HeroTurnTaker(m_SignalBus, h));
            }
            
            turnTakers = turnTakers.OrderByDescending((t) => t.GetInitiative()).ToList();

            return turnTakers;
        }

        private void OnEndTurnButtonClicked()
        {
            
            if(m_NextTurnProccessRoutine == null){
                m_SignalBus.SendSignal(new RequestCardsDraggingStateSwitchSignal(false));
                m_NextTurnProccessRoutine = new AwaitableCoroutine(this, ProccessNextTurn());
            }
        }

        private void MarkTurnTaker(GameObject turnTaker)
        {
            var transform = turnTaker.transform;

            var marker_pos_offeset = new Vector3((transform.GetWidth() / 2) + 0.25f, 0, 0);
            var marker_pos = transform.position + marker_pos_offeset;

            m_TurnMarker = FloatingIconUtility.ShowWorldIcon(m_TurnMarkerSprite, marker_pos, transform, scale: m_TurnMarkerScale);
            m_MarkerTween = m_TurnMarker.transform.DOLocalMoveX(m_TurnMarker.transform.localPosition.x + 0.25f, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }
        private void FreeTurnMarker(){
            if(m_MarkerTween != null){
                m_MarkerTween.Kill();
            }
            if (m_TurnMarker != null)
            {
                FloatingIconUtility.HideWorldIcon(m_TurnMarker);
            }
        }
    }
}
