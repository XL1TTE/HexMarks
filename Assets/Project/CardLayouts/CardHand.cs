using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DG.Tweening;
using Project.Cards;
using Project.DataResolving.DataRequestResolvers;
using Project.Factories;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Project.Layouts
{
    public class CardHand : MonoBehaviour, ICardLayout
    {
        
        [Inject]
        private void Construct(CardHandReqResolver cardHandResolver){
            cardHandResolver.SetHand(this);
        }
        
        protected List<CardView> m_ClaimedItems = new();
        public IReadOnlyList<CardView> GetAllItems() => m_ClaimedItems;
        [SerializeField, Range(1.0f, 10.0f)] protected float m_Spacing = 1.0f;
        [SerializeField, Range(0.1f, 2f)] protected float m_AlignDuration = 0.25f;


        [Header("Hover Effect")]
        [SerializeField, Range(0.5f, 3f)]
        private float m_HoverSpacingMultiplier = 1.5f;
        [SerializeField]
        private float m_HoverYOffset = 0.5f;
        private CardView m_HoveredCard;

        private bool m_NeedsAlignment = false;
        private List<Tween> m_ActiveAlignTweens = new List<Tween>();

        public virtual bool TryClaim(CardView a_card)
        {
            a_card.LeaveLayout();
            a_card.SetLayout(this);
            
            m_ClaimedItems.Add(a_card);
            
            a_card.GetTransform().SetParent(transform);

            a_card.AddDragBeginListener(OnBeginDrag);
            a_card.AddDragEndListener(OnEndDrag);
            
            a_card.AddPointerEnterListener(OnPointerEnterCard);
            a_card.AddPointerExitListener(OnPointerExitCard);

            RequestAlignment();
            return true;
        }

        public virtual void Release(CardView a_card)
        {
            m_ClaimedItems.Remove(a_card);

            a_card.RemoveDragBeginListener(OnBeginDrag);
            a_card.RemoveDragEndListener(OnEndDrag);

            a_card.RemovePointerEnterListener(OnPointerEnterCard);
            a_card.RemovePointerExitListener(OnPointerExitCard);

            RequestAlignment();
        }
        public virtual void ClearHand()
        {
            List<CardView> temp = new (m_ClaimedItems);
            foreach (var item in temp)
            {
                Release(item);
            }

            foreach (var card in temp)
            {
                CardViewObjectPool.current.Return(card);
            }

            RequestAlignment();
        }

        private void OnPointerEnterCard(CardView card)
        {
            m_HoveredCard = card;
            RequestAlignment();
        }

        private void OnPointerExitCard(CardView card)
        {
            m_HoveredCard = null;
            RequestAlignment();
        }


        protected void OnBeginDrag()
        {
            m_HoveredCard = null;
            RequestAlignment();
        }

        protected void OnEndDrag()
        {
            RequestAlignment();
        }

        protected void RequestAlignment()
        {
            m_NeedsAlignment = true;
        }

        public virtual void AlignItems()
        {
            m_ActiveAlignTweens.ForEach(t => t?.Kill());
            m_ActiveAlignTweens.Clear();

            if (m_ClaimedItems.Count == 0) return;

            var cardsToAlign = m_ClaimedItems.Where(c => !c.IsDragging()).ToList();

            float totalWidth = (cardsToAlign.Count - 1) * m_Spacing;
            float startX = -totalWidth / 2f;

            for (int i = 0; i < cardsToAlign.Count; i++)
            {
                CardView card = cardsToAlign[i];

                // Target coordinates
                float x = startX + i * m_Spacing;
                float y = 0;
                float z = i + 1;

                // If any card hovered
                if (m_HoveredCard != null && !m_HoveredCard.IsDragging())
                {
                    int hoverIndex = cardsToAlign.IndexOf(m_HoveredCard);

                    if (i == hoverIndex)
                    {
                        y += m_HoverYOffset;
                    }
                    
                    if (i < hoverIndex)
                    {
                        x -= m_Spacing * (m_HoverSpacingMultiplier - 1) * 0.5f;
                    }
                    
                    if (i > hoverIndex)
                    {
                        x += m_Spacing * (m_HoverSpacingMultiplier - 1) * 0.5f;
                    }
                }

                Vector3 targetPos = new Vector3(x, y, z);
                
                m_ActiveAlignTweens.Add(
                    card.GetTransform()
                        .DOLocalMove(targetPos, m_AlignDuration)
                        .SetEase(Ease.OutQuad)
                );
            }

            m_NeedsAlignment = false;

        }

        protected virtual void LateUpdate()
        {
            if (m_NeedsAlignment)
            {
                AlignItems();
            }
        }
    }
}
