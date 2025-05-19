using Project.Cards;
using UnityEngine;

namespace Project.EventBus.Signals{
    public class CardPlayedSignal: ISignal{
        
        public CardPlayedSignal(Card card){
            this.card = card;
        }
        public readonly Card card;
    }
}
