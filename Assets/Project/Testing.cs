using System.Collections;
using System.Collections.Generic;
using Project.Cards;
using Project.Factories;
using UnityEngine;
using Zenject;

namespace Project
{
    public class Testing : MonoBehaviour
    {
        [Inject]
        private void Construct(ICardFactory a_cardFactory){
            m_cardFactory = a_cardFactory;
        }
        
        ICardFactory m_cardFactory;
        
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Space)){
                Card card = m_cardFactory.CreateNewCard();
            }
        }
    }
}
