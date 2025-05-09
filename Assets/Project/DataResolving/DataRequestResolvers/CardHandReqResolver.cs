using Project.Layouts;

namespace Project.DataResolving.DataRequestResolvers{
    public class CardHandReqResolver : IDataRequestResolver
    {
        public void SetHand(CardHand cardHand){
            CardHand = cardHand;
        }
        private CardHand CardHand;
        
        public bool CanResolve(DataRequierment req)
        {
            return req.GetReqName() == "CardHand" && req.GetReqDataType() == typeof(CardHand);
        }

        public object Resolve(DataRequierment req)
        {
            return CardHand;
        }
    }
}
