using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DG.Tweening;
using Project.Cards;
using Project.Factories;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Layouts
{
    public class CardHand : MonoBehaviour, ICardLayout
    {
        protected List<CardView> m_ClaimedItems = new();
        public IReadOnlyList<CardView> GetAllItems() => m_ClaimedItems;
        [SerializeField, Range(1.0f, 10.0f)] protected float m_Spacing = 1.0f;
        [SerializeField, Range(0.1f, 2f)] protected float m_AlingDuration = 0.25f;

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

            RequestAlignment();
            return true;
        }

        public virtual void Release(CardView a_card)
        {
            m_ClaimedItems.Remove(a_card);

            a_card.RemoveDragBeginListener(OnBeginDrag);
            a_card.RemoveDragEndListener(OnEndDrag);

            RequestAlignment();
        }
        public virtual void ClearHand()
        {
            List<CardView> temp = new (m_ClaimedItems);
            foreach (var item in m_ClaimedItems)
            {
                item.RemoveDragBeginListener(OnBeginDrag);
                item.RemoveDragEndListener(OnEndDrag);
            }
            
            m_ClaimedItems.Clear();
            
            foreach(var card in temp){
                CardViewObjectPool.current.Return(card);
            }
            
            RequestAlignment();
        }

        protected void OnBeginDrag()
        {
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
            foreach (var tween in m_ActiveAlignTweens)
            {
                if (tween.IsActive()) tween.Kill();
            }
            m_ActiveAlignTweens.Clear();

            var align = m_ClaimedItems.Where(i => !i.IsDragging()).ToList();

            var m_position = gameObject.transform.localPosition;

            float totalWidth = (align.Count - 1) * m_Spacing;

            float startOffset = -totalWidth / 2f;

            for (int i = 0; i < align.Count; i++)
            {
                var c = align[i];

                var x_pos = m_position.x + startOffset + (m_Spacing * i);

                var tween = c.GetTransform().DOLocalMove(new Vector3(x_pos, 0, m_position.z + i + 1), m_AlingDuration);
                m_ActiveAlignTweens.Add(tween);
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
