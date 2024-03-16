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

        public void InitProjectileView(Sprite spriteToSet, ProjectileType projectileType)
        {
            spriteRenderer.sprite = spriteToSet;
            this.projectileType = projectileType;
        }
        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<BloonView>() != null)
            {
                if (projectileType == ProjectileType.Canon)
                {
                    BlastImpact();
                }
                else if (projectileType == ProjectileType.EnergyBall)
                {
                    if (collision.GetComponent<BloonView>().GetBloonType() == BloonType.Metal)
                    {
                        return;
                    }
                }
                controller.OnHitBloon(collision.GetComponent<BloonView>().Controller);
            }
        }

        private void BlastImpact()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, impactRadius,projectileLayer);

            foreach (Collider2D hit in hits)
            {
                controller.OnHitBloon(hit.GetComponent<BloonView>().Controller); 
            }
        }
    }
}