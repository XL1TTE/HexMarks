using System;
using System.Collections.Generic;
using CMSystem;
using Project.Actors;
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
        
        private List<SaveHeroState> m_Heroes = new();
        public IReadOnlyList<SaveHeroState> GetHeroes() => m_Heroes;
    }
    
    
    public class RuntimeDataProvider{
        
        [Inject]
        private void Construct(SaveFile save){
            m_PlayerState = save.m_PlayerState;
        }
        
        public SavePlayerState m_PlayerState;
        public CMSEntity m_CurrentLocationModel;    
    }
}
