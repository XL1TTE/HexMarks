using System.Collections;
using System.Collections.Generic;
using Project.Data.CMS.Tags;
using GameData;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Map
{
    public class LocationsController: MonoBehaviour{
        [Inject]
        private void Construct(GameDataTracker runtimeData){
            m_RuntimeDataProvider = runtimeData;
        }
        private GameDataTracker m_RuntimeDataProvider;

        private List<MapLocationView> m_allLocations = new();

        void Awake()
        {
            for (int i = 0; i < transform.childCount; i++){
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
                
                m_RuntimeDataProvider.SetCurrentLocation(model);
                
                StartCoroutine(TransitToScene("BattleScene"));
            }
        }
        
        private IEnumerator TransitToScene(string sceneName){
            var scene_loading = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            
            yield return new WaitUntil(() => scene_loading.isDone);
        }
    
    }
}
