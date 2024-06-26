using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private GameObject bloodDropsFX;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Image damageFlashImg;
    [SerializeField] private float damageTime = 0.5f;

    [SerializeField] private AudioClip playerGrunt;

    private float currentHealth;
    private bool getDamage;
    private Color dmgColor;
    private AudioSource audioSrc;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        getDamage = false;
        dmgColor = new Color(255, 255, 255, 0.7f);
        currentHealth = maxHealth;
        healthBar.value = maxHealth;
        healthBar.maxValue = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        audioSrc.PlayOneShot(playerGrunt);

        getDamage = true;
        currentHealth -= damage;
        healthBar.value = currentHealth;
        Instantiate(bloodDropsFX, transform.position, transform.rotation);

        if (currentHealth <= 0)
        {
            MakeDead();
        }
    }

    public void Heal(float amount)
    {
        if (currentHealth < maxHealth)
        {
            if (amount + currentHealth <= maxHealth) currentHealth += amount;
            else currentHealth = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    private void MakeDead()
    {
        damageFlashImg.color = dmgColor;
        getDamage = true;
        Destroy(gameObject);
    }

    private void Update()
    {
        if (getDamage)
        {
            damageFlashImg.color = dmgColor;
            getDamage = false;
        }
        else
        {
            damageFlashImg.color = Color.Lerp(damageFlashImg.color, new Color(255f,255f,255f,0f), damageTime * Time.deltaTime);
        }
    }
}
