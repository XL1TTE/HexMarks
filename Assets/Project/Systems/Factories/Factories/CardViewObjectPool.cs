using System.Collections.Generic;
using CardTags;
using CMSystem;
using DG.Tweening;
using Project.Cards;
using UnityEngine;
using Zenject;

namespace Project.Factories{
    public class CardViewObjectPool : MonoBehaviour{

        public static CardViewObjectPool current;
        public void Awake()
        {
            if(current == null){
                current = this;
            }
        }

        [SerializeField] private Transform m_PoolContainer;
        
        private Dictionary<string, Queue<CardView>> m_freeObjects = new();
        
        private DiContainer m_Container;
        public CardViewObjectPool Init(DiContainer container){
            m_Container = container;
            return this;
        }
        
        public CardView Get(string model_id, bool isActiveByDefault = true){

            var card_model = CMS.Get<CMSEntity>(model_id);
            if (!m_freeObjects.ContainsKey(model_id) || m_freeObjects[model_id].Count == 0){
                if (!card_model.Is<TagPrefab>(out var tagPrefab)){
                    throw new System.Exception("Cannot instantiate prefab from cms model without TagPrefab");
                }
                var prefab = tagPrefab.prefab;
        
                var cardView = m_Container.InstantiatePrefabForComponent<CardView>(prefab, transform);
                return ConfigObject(cardView, card_model, isActiveByDefault);
            }
        
            return ConfigObject(m_freeObjects[model_id].Dequeue(), card_model, isActiveByDefault);
        }
        
        public CardView Get(string model_id, Transform parent){
            var cardView = Get(model_id);
            cardView.gameObject.transform.SetParent(parent);
            return cardView;
        }
        
        public void Return(CardView a_cardView){
            
            var model = a_cardView.GetModel();
            var model_id = model.id;

            ConfigObject(a_cardView, model, false);

            a_cardView.gameObject.transform.SetParent(m_PoolContainer);
            a_cardView.gameObject.transform.localPosition = transform.localPosition;  

            if (m_freeObjects.ContainsKey(model_id)){
                m_freeObjects[model_id].Enqueue(a_cardView);
            }
            else{
                var queue = new Queue<CardView>();
                queue.Enqueue(a_cardView);
            m_freeObjects.TryAdd(model_id, queue);
            }
        }


        private CardView ConfigObject(CardView a_obj, CMSEntity model, bool isActive)
        {
            a_obj.GetRenderer().sprite = model.GetTag<TagPrefab>().prefab.GetRenderer().sprite;            
            a_obj.gameObject.transform.DOKill(true);
            
            var cardObject = a_obj.gameObject;
            
            cardObject.SetActive(isActive);
            
            return a_obj;
        }
    }
}

