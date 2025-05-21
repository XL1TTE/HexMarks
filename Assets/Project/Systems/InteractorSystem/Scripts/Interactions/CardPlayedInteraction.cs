using Project.EventBus;
using UnityEngine;
using Zenject;

namespace Project.InteractorSystem
{
    public class CardPlayedInteraction : InteractionBase, ICardPlayInteraction
    {     
        public void OnCardPlayed()
        {
            throw new System.NotImplementedException();
        }
    }
}
