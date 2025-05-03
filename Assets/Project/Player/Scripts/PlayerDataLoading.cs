using Project.Factories;
using UnityEngine;
using Zenject;

namespace Project.Player{
    
    public class PlayerDataLoading: MonoBehaviour{
        
        [Inject]
        private void Construct(IPlayerDataSaver playerSave){
            playerSave.LoadPlayerData();
        }
    }
    
}
