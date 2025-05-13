using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CMSystem;
using Google.Protobuf;
using Project.Actors;
using Project.Actors.Stats;
using Project.Data.CMS.Tags.Heroes;
using SaveData;
using SaveDataProto.DataClasses;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Project.Factories{
    public interface ISaveSystem{
        void LoadSave();
        IEnumerator SaveData();
        void CreateNewSaveFile();
    }
    
    [Serializable]
    public class InitialSaveConfig{
        [SerializeField] public List<CMSEntityPfb> m_StartWithHeroes = new();
    }

    public class SaveSystem : ISaveSystem
    {
        private static readonly string m_SaveDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "MyGames/HexMarks/SaveData");

        private static readonly string m_SaveFilePath = Path.Combine(m_SaveDirectoryPath, "SaveFile.sav");
        
        [Inject]
        private SaveSystem(InitialSaveConfig initialSaveConfig)
        {
            m_initialSaveConfig = initialSaveConfig;

            if (!Directory.Exists(m_SaveDirectoryPath))
            {
                Directory.CreateDirectory(m_SaveDirectoryPath);
            }

            LoadSave();
        }
        
        private InitialSaveConfig m_initialSaveConfig;
        
        public void CreateNewSaveFile(){
            var protoSave = CreateNewSave();
            
            SaveFile.ClearSave();
            
            SaveFileMapper.protoSaveToSaveFile(protoSave);
        }      
        
        public IEnumerator SaveData()
        {            
            
            var protoSave = SaveFileMapper.SaveFileToProtoSave();

            var saveTask = Task.Run(() =>{
                using (var stream = File.Open(m_SaveFilePath, FileMode.Create))
                {
                    using (var codedStream = new CodedOutputStream(stream))
                    {
                        protoSave.WriteTo(codedStream);
                        codedStream.Flush();
                    }
                }
            });
            
            while(!saveTask.IsCompleted){
                yield return null;
            }
            
            Debug.Log($"Data saved. \n data: {JsonFormatter.Default.Format(protoSave)}");
        }

        public void LoadSave()
        {
            protoSaveFile proto_saveFile;
            
            if (!File.Exists(m_SaveFilePath)){ proto_saveFile = CreateNewSave(); }
            else{
                var binarySave = File.ReadAllBytes(m_SaveFilePath);
                
                proto_saveFile = protoSaveFile.Parser.ParseFrom(binarySave);
            }
            
            SaveFileMapper.protoSaveToSaveFile(proto_saveFile);
            
            Debug.Log($"Save data was loaded.\n data: {JsonFormatter.Default.Format(proto_saveFile)}");
        }
        
        private protoSaveFile CreateNewSave(){
            
            var save = new protoSaveFile();
            
            save.PlayerData = new protoPlayerSave();
            
            var s_heroes = save.PlayerData.Heroes;
            
            foreach (var h in m_initialSaveConfig.m_StartWithHeroes)
            {
                var s_hero = new protoHero();
                
                s_hero.Id = Guid.NewGuid().ToString();
                s_hero.IdModel = h.GetId();

                var heroModel = CMS.Get<CMSEntity>(h.GetId());

                if (heroModel.Is<TagStats>(out var tagStats))
                {
                    var d_stats = tagStats.GetDefaultStats();
                    
                    s_hero.Stats = new protoHeroStats
                    {
                        HandCapacity = d_stats.m_MaxCardsInHand,
                        BaseStats = new protoActorStats{
                            Health = d_stats.m_BaseStats.m_Health,
                            MaxHealth = d_stats.m_BaseStats.m_MaxHealth,
                            Initiative = d_stats.m_BaseStats.m_Initiative,
                        } 
                    };
                }
                
                s_heroes.Add(s_hero);
            }
            
            return save;
        }
    }
}

