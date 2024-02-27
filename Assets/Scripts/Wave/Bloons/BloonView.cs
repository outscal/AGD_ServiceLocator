using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Wave.Bloon
{
    public enum RegenerateType
    {
        YES,
        NO
    }

    public class BloonView : MonoBehaviour
    {
        [SerializeField] private RegenerateType regenerateType;
        public BloonController Controller { get; set; }
        private SpriteRenderer spriteRenderer;
        private Animator animator;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            Controller.FollowWayPoints();
        }

        public void SetRenderer(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;

        public void SetSortingOrder(int sortingOrder) => spriteRenderer.sortingOrder = sortingOrder;

        public void PopBloon()
        {
            animator.enabled = true;
            animator.Play("Pop", 0);
        }

        public void PopAnimationPlayed()
        {
            spriteRenderer.sprite = null;
            gameObject.SetActive(false);
            Controller.OnPopAnimationPlayed();
        }

        public RegenerateType GetRegenerateType()
        {
            return regenerateType;
        }

        
    }
}