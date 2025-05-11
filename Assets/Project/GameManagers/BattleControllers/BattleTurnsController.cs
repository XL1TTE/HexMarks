using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Project.Data.CMS.Tags.Enemies;
using Project.DataResolving;
using Project.Enemies;
using Project.EventBus;
using Project.EventBus.Signals;
using Project.JobSystem;
using Project.TurnSystem;
using Project.Utilities;
using Project.Utilities.Extantions;
using Project.Wrappers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Game.Battle.Controllers
{
    public class BattleTurnsController: MonoBehaviour{
        
        [Inject]
        private void Construct(SignalBus signalBus, DataResolver dataResolver){
            m_SignalBus = signalBus;
            m_DataResolver = dataResolver;
        }
        void OnEnable()
        {
            m_SignalBus.Subscribe<BattleStartSignal>(OnBattleStartInteraction);
            m_SignalBus.Subscribe<HeroTurnSignal>(OnHeroTurnInteraction);
            m_SignalBus.Subscribe<EnemyTurnSignal>(OnEnemyTurnInteraction);
            
            m_SignalBus.Subscribe<EnemyDiedSignal>(OnEnemyDiedInteraction);

            m_EndTurnButton.onClick.AddListener(OnEndTurnButtonClicked);
        }

        void OnDisable()
        {
            m_SignalBus.Unsubscribe<BattleStartSignal>(OnBattleStartInteraction);
            m_SignalBus.Unsubscribe<HeroTurnSignal>(OnHeroTurnInteraction);
            m_SignalBus.Unsubscribe<EnemyTurnSignal>(OnEnemyTurnInteraction);

            m_SignalBus.Unsubscribe<EnemyDiedSignal>(OnEnemyDiedInteraction);

            m_EndTurnButton.onClick.RemoveListener(OnEndTurnButtonClicked);

            if (m_TurnMarker != null)
            {
                m_MarkerTween.Kill();
                FloatingIconUtility.HideWorldIcon(m_TurnMarker);
            }
        }

        private SignalBus m_SignalBus;
        private DataResolver m_DataResolver;
        private List<ITurnTaker> m_TurnsQueue;  
        
        [SerializeField] private Sprite m_TurnMarkerSprite;
        [SerializeField] private Vector3 m_TurnMarkerScale;
        private SpriteRenderer m_TurnMarker;
        private Tween m_MarkerTween;
        
        private AwaitableCoroutine m_NextTurnProccessRoutine;
        
        [SerializeField] private Button m_EndTurnButton;


        private List<Func<bool>> m_NextTurnProccessValidators = new();

        public void AddNextTurnValidator(Func<bool> awaiter) =>
            m_NextTurnProccessValidators.Add(awaiter);
        public void RemoveNextTurnValidator(Func<bool> awaiter) =>
            m_NextTurnProccessValidators.Remove(awaiter);

        private IEnumerator OnHeroTurnInteraction(HeroTurnSignal signal)
        {
            m_EndTurnButton.interactable = true;
            
            var hero = signal.GetHero();
            var hero_transform = hero.gameObject.transform;
            
            var marker_pos_offeset = new Vector3((hero.gameObject.transform.GetWidth() / 2) + 0.25f, 0, 0);
            var marker_pos = hero_transform.position + marker_pos_offeset;

            m_TurnMarker = FloatingIconUtility.ShowWorldIcon(m_TurnMarkerSprite, marker_pos, hero.transform, scale: m_TurnMarkerScale);

            m_MarkerTween = m_TurnMarker.transform.DOLocalMoveX(m_TurnMarker.transform.localPosition.x + 0.25f, 0.5f).SetLoops(-1, LoopType.Yoyo);

            yield return null;
        }

        private IEnumerator OnEnemyTurnInteraction(EnemyTurnSignal signal)
        {
            var enemy = signal.GetEnemy();
            var enemyModel = enemy.GetController().GetModel();

            if (enemyModel.Is<TagOnTurnActions>(out var onTurn))
            {
                foreach (var a in onTurn.actions)
                {
                    var context = m_DataResolver.Resolve(a);
                    yield return a.GetAction(enemy, context);
                }
            }

            yield return ProccessNextTurn();
        }

        private IEnumerator OnEnemyDiedInteraction(EnemyDiedSignal signal){
            var enemy = signal.GetEnemy();

            m_TurnsQueue.TryRemove((t) => t is EnemyTurnTaker tt && tt.GetEnemy() == enemy);
            yield return null;
        }

        private IEnumerator OnBattleStartInteraction(BattleStartSignal signal)
        {
            m_TurnsQueue = CreateTurnQueue(signal);
            yield return ProccessNextTurn();
        }

        private IEnumerator ProccessNextTurn()
        {
            m_EndTurnButton.interactable = false;

            while (m_NextTurnProccessValidators.Any(v => !v.Invoke()))
            {
                yield return null;
            }
            
            yield return new WaitForSeconds(0.15f);
            
            if (m_TurnMarker != null){
                m_MarkerTween.Kill();
                FloatingIconUtility.HideWorldIcon(m_TurnMarker);
            }

            if (m_TurnsQueue.Count == 0) { yield break; }
            var turnTaker = m_TurnsQueue.Dequeue();
            m_TurnsQueue.Enqueue(turnTaker);
            
            turnTaker.SendTurnNotification();

            m_NextTurnProccessRoutine = null;
        }

        private List<ITurnTaker> CreateTurnQueue(BattleStartSignal signal)
        {
            var enemies = signal.GetEnemiesInBattle();
            var heroes = signal.GetHeroesInBattle();

            var temp = new List<ITurnTaker>();

            foreach (var e in enemies)
            {
                temp.Add(new EnemyTurnTaker(m_SignalBus, e));
            }
            
            foreach(var h in heroes){
                temp.Add(new HeroTurnTaker(m_SignalBus, h));

            }

            return TurnsUtility.CreateTurnsQueue(temp);
        }


        private void OnEndTurnButtonClicked()
        {
            if(m_NextTurnProccessRoutine == null){
                m_NextTurnProccessRoutine = new AwaitableCoroutine(this, ProccessNextTurn());
            }
        }

    }
}
