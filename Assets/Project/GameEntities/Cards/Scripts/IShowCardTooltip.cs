using Project.Data.CMS.Tags.Generic;
using Project.ObjectInteractions;
using Project.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Cards
{
    public class IShowCardTooltip: MonoBehaviour{
        
        [SerializeField] CardView m_card;
        
        [SerializeField] Interactable m_interactable;
        


        void OnEnable()
        {
            m_interactable.AddPointerEnterListener(ShowToolTip);
            m_interactable.AddPointerExitListener(HideToolTip);
        }

        void OnDisable()
        {
            m_interactable.RemovePointerEnterListener(ShowToolTip);
            m_interactable.RemovePointerExitListener(HideToolTip);
        }


        private void HideToolTip(BaseEventData eventData)
        {
            CardToolTip.Hide();
        }

        private void ShowToolTip(BaseEventData eventData)
        {
            var card_model = m_card.GetModel();

            string card_name = "";
            string card_desc = "";

            if (card_model.Is<TagName>(out var tagName)) { card_name = tagName.name; }
            if (card_model.Is<TagDescription>(out var tagDesc)) { card_desc = tagDesc.desc; }
            

            CardToolTip.Show(card_name, card_desc);
        }
    }
}

