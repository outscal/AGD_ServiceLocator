using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MonkeyImageHandler : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        private Image monkeyImage;
        private MonkeyCellController owner;
        private Sprite spriteToSet;
        private RectTransform rectTransfrom;
        private Vector3 originalPosition;
        private Vector3 originalAnchoredPosition;
        public void ConfigureImageHandler(Sprite spriteToSet, MonkeyCellController owner)
        {
            this.spriteToSet = spriteToSet;
            this.owner = owner;
        }


        private void Awake()
        {
            monkeyImage = GetComponent<Image>();
            monkeyImage.sprite = spriteToSet;
            rectTransfrom = GetComponent<RectTransform>();
            originalPosition = rectTransfrom.position;
            originalPosition = rectTransfrom.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransfrom.anchoredPosition += eventData.delta;
            owner.MonkeyDraggedAt(rectTransfrom.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetMonkey();
            owner.MonkeyDroppedAt(eventData.position);
        }

        private void ResetMonkey()
        {
            monkeyImage.color = new Color(1, 1, 1, 1);
            rectTransfrom.position = originalPosition;
            rectTransfrom.anchoredPosition = originalAnchoredPosition;
            GetComponent<LayoutElement>().enabled = false; 
            GetComponent<LayoutElement>().enabled = true; 
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            monkeyImage.color = new Color(1, 1, 1, 0.6f);
        }
    }
}