
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
              new DataRequierment("PlayerInBattle", typeof(PlayerInBattle))  
            };
        }
        
        public IEnumerator GetAITurnSequence(DataContext context)
        {
            var player = context.Get<PlayerInBattle>("PlayerInBattle");
            
            player.TakeDamage(m_Model.GetEnemyDamage());

            yield return null;
        }
    }

}
