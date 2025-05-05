using System;
using System.Collections;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.Factories{
    public interface IPlayerDataFactory{
        PlayerData LoadPlayerData();
        IEnumerator SavePlayerData();
    }

    [Serializable]
    public class PlayerDataFactory : IPlayerDataFactory
    {
        [SerializeField] private string m_SaveFilePath;
        
        [SerializeField] private PlayerData m_defaultData;
        
        public PlayerData LoadPlayerData()
        {
            if(m_defaultData == null){return new PlayerData(); }
            return m_defaultData;
        }

        public IEnumerator SavePlayerData()
        {
            throw new System.NotImplementedException();
        }
    }
}

