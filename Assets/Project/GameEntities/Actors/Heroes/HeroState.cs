using System;
using System.Collections.Generic;
using CMSystem;
using Project.Actors.Stats;
using UnityEngine;

namespace Project.Actors{


    public class HeroState{
        public HeroState(string id, CMSEntity model, HeroStats stats, HeroDeck deck){
            m_id = id;
            m_stats = stats;
            m_model = model;
            m_deck = deck;
        }
        
        public string m_id;
        public HeroStats m_stats;   
        public HeroDeck m_deck;
        public CMSEntity m_model;
    }
    
    public class HeroDeck{
        public HeroDeck(List<CMSEntity> cardModels){
            m_cardModels = cardModels;
        }
        private List<CMSEntity> m_cardModels = new();
        public IReadOnlyList<CMSEntity> GetCards() => m_cardModels;
    }
    
}
