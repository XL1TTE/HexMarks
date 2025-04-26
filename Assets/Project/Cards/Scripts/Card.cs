namespace Project.Cards{
    public class Card{
        public Card(CardView a_view, CardModel a_model, CardController a_controller){
            m_View = a_view;
            m_Model = a_model;
            m_Controller = a_controller;
        }
        public CardView m_View;
        public CardModel m_Model;
        public CardController m_Controller;
    }
    
    public class CardDefenition{
        
    }
}

