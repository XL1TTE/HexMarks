using Project.Layouts;
using XL1TTE.GameActions;

namespace Project.DataResolving.DataRequestResolvers{
    public class CardHandReqResolver : IDataRequestResolver
    {
        public void SetHand(CardHand cardHand){
            CardHand = cardHand;
        }
        private CardHand CardHand;
        
        public bool CanResolve(DataRequest req)
        {
            return req.Key == "CardHand" && req.Type == typeof(CardHand);
        }

        public object Resolve(DataRequest req)
        {
            return CardHand;
        }
    }
}
