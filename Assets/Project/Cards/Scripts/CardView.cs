using UnityEngine;

namespace Project.Cards{
    public class CardView : MonoBehaviour
    {
        private CardController m_controller;

        public void Init(CardController a_controller)
        {
            m_controller = a_controller;
        }
    }
}

