using UnityEngine;
using ServiceLocator.Wave.Bloon;

namespace ServiceLocator.Player.Projectile
{
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] private float impactRadius = 3f;
        [SerializeField] private LayerMask projectileLayer;
        
        private ProjectileController controller;
        private SpriteRenderer spriteRenderer;
        private ProjectileType projectileType;

        private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();

        public void SetController(ProjectileController controller) => this.controller = controller;

        private void Update()
        {
            if (ProjectileOutOfBounds())
                controller.ResetProjectile();
            
            controller?.UpdateProjectileMotion();
        }

        private bool ProjectileOutOfBounds() => !spriteRenderer.isVisible;

        public void InitProjectileView(Sprite spriteToSet) => spriteRenderer.sprite = spriteToSet;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<BloonView>() != null)
            {
                controller.OnHitBloon(collision.GetComponent<BloonView>().Controller);
            }
        }

        public void BlastImpact()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, impactRadius,projectileLayer);

            foreach (Collider2D hit in hits)
            {
                controller.OnHitBloon(hit.GetComponent<BloonView>().Controller); 
            }
        }
    }
}