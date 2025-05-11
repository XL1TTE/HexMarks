using System;
using CMSystem;
using DG.Tweening;
using Project.Data.CMS.Tags;
using Project.Data.CMS.Tags.Generic;
using Project.EventBus;
using Project.Utilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Map
{
    public class MapLocationView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {

        #region Visuals
        [SerializeField] private Sprite m_MarkerSprite;
        [SerializeField] private Transform m_MarkerTransform;

        private SpriteRenderer m_Marker;
        private Tween m_MarkerFloatingTween;
        #endregion


        [SerializeField] private CMSEntityPfb m_LocationPfb;
        private CMSEntity m_LocationModel;
        public CMSEntity GetLocationModel() => m_LocationModel;
        
        
        private string Name;
        private string m_Description;
        
        public event Action<MapLocationView> OnPointerClickEvent;

        void OnDisable()
        {
            m_MarkerFloatingTween.Kill();
            ToolTipManager.HideTooltip();
            if(m_Marker != null){
                FloatingIconUtility.HideWorldIcon(m_Marker);
            }
        }

        void Start()
        {
            m_LocationModel = CMS.Get<CMSEntity>(m_LocationPfb.GetId());

            if (!m_LocationModel.Is<TagMapLocation>())
            {
                throw new System.Exception("CMSEntity of location missing TagMapLocation!");
            }
            
            if (m_LocationModel.Is<TagName>(out var tagName)) { Name = tagName.name; }
            if (m_LocationModel.Is<TagDescription>(out var tagDesc)) { m_Description = tagDesc.desc; }
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClickEvent?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ToolTipManager.ShowTooltip(Name, m_Description);
            
            m_Marker = FloatingIconUtility.ShowWorldIcon(m_MarkerSprite, m_MarkerTransform.position, m_MarkerTransform, scale: Vector3.one);

            m_MarkerFloatingTween = m_Marker.transform.DOLocalMoveY(m_Marker.transform.localPosition.y + 1, 0.5f).SetLoops(-1, LoopType.Yoyo);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ToolTipManager.HideTooltip();
            FloatingIconUtility.HideWorldIcon(m_Marker);

            m_MarkerFloatingTween.Kill();
        }
    }
}
