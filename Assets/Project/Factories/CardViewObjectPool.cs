using System.Collections.Generic;
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
            m_DefaulObjScale = m_prefab.gameObject.transform.localScale;
        }

        private Vector3 m_DefaulObjScale;

        [SerializeField] private Transform m_PoolContainer;
        [SerializeField] private CardView  m_prefab;
        
        private Queue<CardView> m_freeObjects = new();
        
        public CardView Get(bool isActiveByDefault = true){
            if(m_freeObjects.Count > 0){
                return ConfigObject(m_freeObjects.Dequeue(), isActiveByDefault);
            }
            var cardView = Instantiate(m_prefab, transform);
            return ConfigObject(cardView, isActiveByDefault);
        }
        
        public CardView Get(Transform parent){
            var cardView = Get();
            cardView.gameObject.transform.SetParent(parent);
            return cardView;
        }
        
        public void Return(CardView a_cardView){
            ConfigObject(a_cardView, false);
            a_cardView.gameObject.transform.SetParent(m_PoolContainer);
            a_cardView.gameObject.transform.localPosition = transform.localPosition;
            m_freeObjects.Enqueue(a_cardView);
        }


        private CardView ConfigObject(CardView a_obj, bool isActive)
        {
            var cardObject = a_obj.gameObject;
            
            cardObject.SetActive(isActive);
            
            return a_obj;
        }
    }
}

