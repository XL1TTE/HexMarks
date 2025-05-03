
using System.Collections;
using System.Collections.Generic;
using Project.DataResolving;
using Project.Layouts;
using Project.Player;
using UnityEngine;

namespace Project.Enemies.AIs{

    public class EnemyAI : IDataResolverUser
    {
        public EnemyAI(EnemyModel model){
            m_Model = model;
        }
        private EnemyModel m_Model;
        
        public IReadOnlyList<DataRequierment> GetDataRequests()
        {
            return new List<DataRequierment>{
              //new DataRequierment("PlayerHand", typeof(CardHand)),
              new DataRequierment("Player", typeof(PlayerData))  
            };
        }
        
        public IEnumerator GetAITurnSequence(DataContext context)
        {
            var player = context.Get<PlayerData>("Player");


            Debug.Log($"Enemy {m_Model} taking his turn...");
            yield return new WaitForSeconds(2f);
            
            player.TakeDamage(m_Model.GetEnemyDamage());

            Debug.Log($"Enemy {m_Model} finished his turn...");

            yield return null;
        }
    }

}
