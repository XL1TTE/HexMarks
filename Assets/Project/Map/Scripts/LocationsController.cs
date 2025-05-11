using System.Collections.Generic;
using Project.Data.CMS.Tags;
using Project.Data.SaveFile;
using Project.EventBus;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Map
{
    public class LocationsController: MonoBehaviour{
        [Inject]
        private void Construct(RuntimeDataProvider runtimeData){
            m_RuntimeDataProvider = runtimeData;
        }
        private RuntimeDataProvider m_RuntimeDataProvider;

        private List<MapLocationView> m_allLocations = new();

        void Awake()
        {
            for(int i = 0; i < transform.childCount; i++){
                if(transform.GetChild(i).TryGetComponent<MapLocationView>(out var location)){
                    m_allLocations.Add(location);
                }
            }
        }

        void OnEnable()
        {
            foreach (var loc in m_allLocations)
            {
                loc.OnPointerClickEvent += ProccessTransitionToLocation;
            }
        }

        void OnDisable()
        {
            foreach(var loc in m_allLocations){
                loc.OnPointerClickEvent -= ProccessTransitionToLocation;
            }
        }

        private void ProccessTransitionToLocation(MapLocationView location){
            var model = location.GetLocationModel();
            
            var tagLoc = model.GetTag<TagMapLocation>();
            
            if(tagLoc is TagDungeon){
                
                m_RuntimeDataProvider.m_CurrentLocationModel = model;
                
                SceneManager.LoadScene("BattleScene", LoadSceneMode.Single);
            }
        }
    
    }
}
