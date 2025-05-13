using UnityEngine;

namespace Project.ObjectInteractions
{
    [RequireComponent(typeof(Interactable))]
    public abstract class InteractionsBase: MonoBehaviour{
        protected virtual void Awake(){
            TryGetComponent<IHaveInteractions>(out m_subject);
            TryGetComponent<Interactable>(out m_interactable);
        }
        public virtual void Initialize(IHaveInteractions a_subject) => m_subject = a_subject;

        protected IHaveInteractions m_subject;
        protected Interactable m_interactable;

        public void ChangeColor(Color a_color) => m_subject.GetRenderer().color = a_color;
        public void ChangeSprite(Sprite a_sprite) => m_subject.GetRenderer().sprite = a_sprite;
        public void SetSortingOrder(int order) => m_subject.GetSortingGroup().sortingOrder = order;
    }
}
