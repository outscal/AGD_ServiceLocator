using UnityEngine;
using ServiceLocator.Wave.Bloon;

namespace ServiceLocator.Player.Projectile
{
    public class ProjectileView : MonoBehaviour
    {
        private ProjectileController controller;
        private SpriteRenderer spriteRenderer;

        private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();

        public void SetController(ProjectileController controller) => this.controller = controller;

        private void Update()
        {
            if (ProjectileOutOfBounds())
                controller.ResetProjectile();
            
            controller?.UpdateProjectileMotion();
        }

        private bool ProjectileOutOfBounds() => !spriteRenderer.isVisible;

        public void SetSprite(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<BloonView>() != null)
                controller.OnHitBloon(collision.GetComponent<BloonView>().Controller);
        }
    }
}