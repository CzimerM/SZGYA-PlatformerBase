using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage = 2f;
    [SerializeField] private float damageRate = 1f;
    [SerializeField] private float pushForce = 20f;

    private float nextDamage;

    void Start()
    {
        nextDamage = 0f;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && nextDamage < Time.time) 
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
            nextDamage = Time.time + damageRate;
            PushBack(collision.transform);

        }
    }

    private void PushBack(Transform target)
    {
        Vector2 pushDirection = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);
        Debug.Log(pushDirection);
        Rigidbody2D targetRB = target.gameObject.GetComponent<Rigidbody2D>();
        targetRB.velocity = Vector2.zero;
        targetRB.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        targetRB.AddForce(new Vector2(20, 0), ForceMode2D.Impulse);
        
    }
}
