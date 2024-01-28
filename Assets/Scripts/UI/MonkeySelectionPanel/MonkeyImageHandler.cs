using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class MonkeyImageHandler : MonoBehaviour,IDragHandler,IEndDragHandler,IPointerDownHandler
    {
        private Image monkeyImage;
        private MonkeyCellController owner;
        private Sprite spriteToSet;
        private RectTransform imageRect;

        public void ConfigureImageHandler(Sprite spriteToSet, MonkeyCellController owner)
        {
            this.spriteToSet = spriteToSet;
            this.owner = owner;
        }


        private void Awake()
        {
            monkeyImage = GetComponent<Image>();
            monkeyImage.sprite = spriteToSet;
            imageRect = GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData)
        {
            imageRect.anchoredPosition += eventData.delta;
            owner.MonkeyDraggedAt(imageRect.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ResetMonkey();
        }
    }
}