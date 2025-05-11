using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using CMSystem;
using Project.Actors;
using Project.Actors.Stats;
using Project.Data.CMS.Tags.Heroes;
using Project.Data.SaveFile;
using UnityEngine;
using Zenject;

namespace Project.Factories{
    public interface ISaveSystem{
        SaveFile LoadSave();
        IEnumerator SaveData();
    }
    
    [Serializable]
    public class InitialSaveConfig{
        [SerializeField] public List<CMSEntityPfb> m_StartWithHeroes = new();
    }

    public class SaveSystem : ISaveSystem
    {
        private readonly string m_SaveFilePath = "";
        
        public SaveSystem(DiContainer container, InitialSaveConfig initialSaveConfig)
        {
            m_saveFile = container.Resolve<SaveFile>();
            m_initialSaveConfig = initialSaveConfig;
        }
        private SaveFile m_saveFile;
        private InitialSaveConfig m_initialSaveConfig;
        
        public IEnumerator SaveData()
        {
            throw new System.NotImplementedException();
        }

        public SaveFile LoadSave()
        {            
            
            foreach(var h in m_initialSaveConfig.m_StartWithHeroes)
            {
                
                var heroModel = CMS.Get<CMSEntity>(h.GetId());

                HeroStats stats = new HeroStats();

                if(heroModel.Is<TagStats>(out var tagStats)){
                    stats = tagStats.GetDefaultStats();
                }

                var hero = new SaveHeroState(stats, h.GetId());
                m_saveFile.m_PlayerState.m_Heroes.Add(hero);
            }

            return m_saveFile;
        }
    }
}

