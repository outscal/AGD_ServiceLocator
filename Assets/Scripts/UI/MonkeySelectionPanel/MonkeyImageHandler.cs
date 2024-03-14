using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MonkeyImageHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        private Image m_MonkeyImage;
        private MonkeyCellController m_OwnerMonkeyCellController;
        private Sprite m_SpriteToSet;

        Canvas m_Canvas;
        RectTransform m_RectTransform;
        private Vector3 m_OriginalPosition;
        private Vector3 m_OriginalAnchoredPosition;

        public void ConfigureImageHandler(Sprite spriteToSet, MonkeyCellController ownerMonkeyCellController)
        {
            this.m_SpriteToSet = spriteToSet;
            this.m_OwnerMonkeyCellController = ownerMonkeyCellController;
        }

        private void Awake()
        {
            m_MonkeyImage = GetComponent<Image>();
            m_MonkeyImage.sprite = m_SpriteToSet;

            m_RectTransform = GetComponent<RectTransform>();
            m_OriginalPosition = m_RectTransform.position;
            m_OriginalAnchoredPosition = m_RectTransform.anchoredPosition;
        }

        private void Start()
        {
            // We can access parent canvas only after this game object is spawned
            m_Canvas = GetComponentInParent<Canvas>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            m_RectTransform.anchoredPosition += eventData.delta * m_Canvas.scaleFactor;
            m_OwnerMonkeyCellController.MonkeyDraggedAt(m_RectTransform.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetMoneky();
            m_OwnerMonkeyCellController.MonkeyDroppedAt(eventData.position);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            m_MonkeyImage.color = new Color(1, 1, 1, 0.6f);
        }

        private void ResetMoneky()
        {
            m_MonkeyImage.color = new Color(1, 1, 1, 1);

            m_RectTransform.position = m_OriginalPosition;
            m_RectTransform.anchoredPosition = m_OriginalAnchoredPosition;
            GetComponent<LayoutElement>().enabled = false;
            GetComponent<LayoutElement>().enabled = true;
        }
    }
}