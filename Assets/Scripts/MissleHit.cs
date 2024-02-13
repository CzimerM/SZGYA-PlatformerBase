using UnityEngine;

public class MissleHit : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private GameObject explosionEffect;

    private ProjectileController controller;

    private void Awake()
    {
        controller = GetComponentInParent<ProjectileController>();
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.layer == LayerMask.NameToLayer("Hitable"))
        {
            controller.Stop();
            Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(gameObject);

            if(target.tag == "Enemy")
            {
                EnemyHealth enemyHealth = target.gameObject.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}
