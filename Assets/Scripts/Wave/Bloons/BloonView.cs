using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Bloon
{
    public class BloonView : MonoBehaviour
    {
        private BloonController controller;
        public BloonController Controller { get => controller; set => controller = value; }
        
        private SpriteRenderer spriteRenderer;
        private Animator animator;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        private void Update() => controller.FollowWayPoints();

        public void SetRenderer(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;

        public void PopBloon()
        {
            animator.enabled = true;
            animator.Play("Pop", 0);
        }

        public void PopAnimationPlayed()
        {
            spriteRenderer.sprite = null;
            gameObject.SetActive(false);
        }
    }
}