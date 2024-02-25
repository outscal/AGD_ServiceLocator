using System.Collections;
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
        private bool hasTimerStart = false;

        public bool HasTimerStart
        {
            get { return hasTimerStart; }
            set { hasTimerStart = value; }
        }

        private bool hasTimerComplete = false;
        public bool HasTimerComplete
        {
            get { return hasTimerComplete; }
        }

        private float timer = 0f;
        private const float maxTime = 3f;
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

            if(hasTimerStart)
            {
                timer += Time.deltaTime;
                if(timer >= maxTime)
                {
                    hasTimerComplete = true;
                    hasTimerStart = false;
                }

            }
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

        public IEnumerator RegenerateTimer()
        {
            yield return null;
        }
    }
}