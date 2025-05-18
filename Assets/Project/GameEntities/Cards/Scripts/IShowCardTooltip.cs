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
            var model = m_card.GetCardState().GetModel();

            string name = "";
            string desc = "";

            if (model.Is<TagName>(out var tagName)) { name = tagName.name; }
            if (model.Is<TagDescription>(out var tagDesc)) { desc = tagDesc.desc; }

            CardToolTip.Show(name, desc);
        }
    }
}

