using System;
using System.Collections.Generic;
using System.Linq;
using CMSystem;
using Project.Actors;
using Project.Factories;
using UnityEngine;
using Zenject;

namespace Project.Data.SaveFile{
    
    [Serializable]
    public class SaveFile{
        public SaveFile(SavePlayerState playerState){
            m_PlayerState = playerState;
        }
        
        public SavePlayerState m_PlayerState = new SavePlayerState();          
    }
    
    [Serializable]
    public class SavePlayerState{
        public SavePlayerState(List<SaveHeroState> heroes){
            m_Heroes = heroes;
        }
        public SavePlayerState(){}
        
        [SerializeField]
        private List<SaveHeroState> m_Heroes = new();
        public IReadOnlyList<SaveHeroState> GetHeroes() => m_Heroes;
        
        public void SaveHeroState(HeroView hero){
            var heroStateId = hero.GetState().id;
            
            var heroStats = hero.GetState().GetStats();
            
            var saveState = m_Heroes.First(h => h.id == heroStateId);
            
            saveState.m_Stats = heroStats;
        }
    }
    
    
    public class RuntimeDataProvider{
        
        [Inject]
        private void Construct(ISaveSystem saveSystem){
            m_PlayerState = saveSystem.GetCurrentSave().m_PlayerState;
        }
        
        public SavePlayerState m_PlayerState;
        public CMSEntity m_CurrentLocationModel;    
    }
}
