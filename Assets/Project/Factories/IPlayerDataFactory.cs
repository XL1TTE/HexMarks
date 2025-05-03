using System.Collections;
using Project.Player;
using Zenject;

namespace Project.Factories{
    public interface IPlayerDataSaver{
        IEnumerator LoadPlayerData();
        IEnumerator SavePlayerData();
    }

    public class SaveSystem : IPlayerDataSaver
    {
        [Inject]
        private void Construct(PlayerData playerData){
            m_playerData = playerData;
        }
        private PlayerData m_playerData;
        
        public IEnumerator LoadPlayerData()
        {
            m_playerData = new PlayerData();
            yield return null;
        }

        public IEnumerator SavePlayerData()
        {
            throw new System.NotImplementedException();
        }
    }
}

