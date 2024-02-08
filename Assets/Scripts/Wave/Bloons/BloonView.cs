using UnityEngine;

namespace ServiceLocator.Wave.Bloon
{
    public class BloonView : MonoBehaviour
    {
        public BloonController Controller { get ; set ; }
        
        private SpriteRenderer spriteRenderer;
        private Animator animator;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        private void Update() => Controller.FollowWayPoints();

        public void SetRenderer(Sprite spriteToSet)
        {
            if (spriteToSet != null)
                spriteRenderer.sprite = spriteToSet;
        }


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
    }
}