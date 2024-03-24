using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ServiceLocator.UI
{
    public class LockedCellHandler : MonoBehaviour, IPointerDownHandler
    {
        private RectTransform rectTransform;
        private Image overlayImage;
        private MonkeyCellController owner;

        public void ConfigureImageHandler(MonkeyCellController owner)
        {
            this.owner = owner;
        }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            overlayImage = GetComponent<Image>();
        }

        public void OnPointerDown(PointerEventData eventData) => owner.UnlockMonkey();
    }
}