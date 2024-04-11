using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    [SerializeField] private float healAmount = 3;
    private bool pickedUp = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !pickedUp)
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            ph.Heal(healAmount);
            Destroy(gameObject);
            pickedUp = true;
        }
    }
}
