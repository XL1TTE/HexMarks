using System.Collections.Generic;
using CardTags;
using CMSystem;
using DG.Tweening;
using Project.Cards;
using UnityEngine;

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
        
        public CardView Get(string model_id, bool isActiveByDefault = true){
            if(!m_freeObjects.ContainsKey(model_id) || m_freeObjects[model_id].Count == 0){
                
                if(!CMS.Get<CMSEntity>(model_id).Is<TagPrefab>(out var tagPrefab)){
                    throw new System.Exception("Cannot instantiate prefab from cms model without TagPrefab");
                }
                var prefab = tagPrefab.prefab;
        
                var cardView = Instantiate(prefab, transform);
                return ConfigObject(cardView, isActiveByDefault);
            }
        
            return ConfigObject(m_freeObjects[model_id].Dequeue(), isActiveByDefault);
        }
        
        public CardView Get(string model_id, Transform parent){
            var cardView = Get(model_id);
            cardView.gameObject.transform.SetParent(parent);
            return cardView;
        }
        
        public void Return(CardView a_cardView){
            ConfigObject(a_cardView, false);
            a_cardView.gameObject.transform.SetParent(m_PoolContainer);
            a_cardView.gameObject.transform.localPosition = transform.localPosition;

            var model_id = a_cardView.GetCardState().GetModel().id;

            if (m_freeObjects.ContainsKey(model_id)){
                m_freeObjects[model_id].Enqueue(a_cardView);
            }
            else{
                var queue = new Queue<CardView>();
                queue.Enqueue(a_cardView);
                m_freeObjects.TryAdd(model_id, queue);
            }
        }


        private CardView ConfigObject(CardView a_obj, bool isActive)
        {
            a_obj.gameObject.transform.DOKill(true);
            
            var cardObject = a_obj.gameObject;
            
            cardObject.SetActive(isActive);
            
            return a_obj;
        }
    }
}

