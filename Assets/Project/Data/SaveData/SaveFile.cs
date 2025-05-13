using System;
using System.Collections.Generic;
using Project.Actors;

namespace SaveData{
    
    [Serializable]
    public class SaveFile{
        private SaveFile() {}
        
        private static SaveFile p_current;
        private static bool isInit = false;
        
        static SaveFile(){
            if(!isInit){
                p_current = new SaveFile();
            }
        }
        
        private PlayerData m_playerData = new PlayerData();
        public static PlayerData AccessPlayerData() => p_current.m_playerData;
        public static void ClearSave() => p_current = new SaveFile();
    }
    
    public class PlayerData{
        private List<HeroState> m_Heroes = new();
        public IReadOnlyList<HeroState> GetHeroes() => m_Heroes;
        public void AddHero(HeroState s_Hero) => m_Heroes.Add(s_Hero);
        public void ClearAllHeroes() => m_Heroes.Clear();
        public void RemoveHero(HeroState s_Hero) => m_Heroes.Remove(s_Hero);
        public void RemoveHero(string id) => m_Heroes.RemoveAll((h) => h.m_id == id);
    }
}
