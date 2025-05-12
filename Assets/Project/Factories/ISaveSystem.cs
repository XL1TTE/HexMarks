using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
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
        
        SaveFile GetCurrentSave();
    }
    
    [Serializable]
    public class InitialSaveConfig{
        [SerializeField] public List<CMSEntityPfb> m_StartWithHeroes = new();
    }

    public class SaveSystem : ISaveSystem
    {
        private static readonly string m_SaveDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyGames/HexMarks/SaveData");

        private static readonly string m_SaveFilePath = Path.Combine(m_SaveDirectoryPath, "SaveFile.sav");
        
        public SaveSystem(InitialSaveConfig initialSaveConfig)
        {
            m_initialSaveConfig = initialSaveConfig;

            if (!Directory.Exists(m_SaveDirectoryPath))
            {
                Directory.CreateDirectory(m_SaveDirectoryPath);
            }

            LoadSave();
        }
        private InitialSaveConfig m_initialSaveConfig;
        private SaveFile m_CurrentSave;
        
        public IEnumerator SaveData()
        {            
            var saveJson = JsonUtility.ToJson(m_CurrentSave);

            Debug.Log(saveJson);

            byte[] saveBytes = Encoding.UTF8.GetBytes(saveJson);
            
            using(FileStream stream = File.Open(m_SaveFilePath, FileMode.OpenOrCreate)){
                Task writeTask = stream.WriteAsync(saveBytes).AsTask();
                
                yield return new WaitUntil(() => writeTask.IsCompleted);
            
                stream.Close();
            }
        }

        public SaveFile LoadSave()
        {
            
            if (!File.Exists(m_SaveFilePath)){ m_CurrentSave = CreateNewSave(); return m_CurrentSave; }

            var saveJson = File.ReadAllText(m_SaveFilePath);

            m_CurrentSave = JsonUtility.FromJson<SaveFile>(saveJson);
            
            Debug.Log(saveJson);
            
            return m_CurrentSave;
        }

        private SaveFile CreateNewSave(){
            var startHeroes = new List<SaveHeroState>();
            foreach (var h in m_initialSaveConfig.m_StartWithHeroes)
            {
                var heroModel = CMS.Get<CMSEntity>(h.GetId());

                HeroStats stats = new HeroStats();

                if (heroModel.Is<TagStats>(out var tagStats))
                {
                    stats = tagStats.GetDefaultStats();
                }

                var hero = new SaveHeroState(stats, h.GetId());
                startHeroes.Add(hero);
            }
            var playerState = new SavePlayerState(startHeroes);
            
            return new SaveFile(playerState);
        }

        public SaveFile GetCurrentSave() => m_CurrentSave;
    }
}

