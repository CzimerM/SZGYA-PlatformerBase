using UnityEngine;

public class HedgehogAI : MonoBehaviour
{
    [SerializeField] float runSpeed = 7f;
    [SerializeField] GameObject graphics;

    private Animator animator;
    private Rigidbody2D rigidbody2d;

    private bool facintgRight = false;
    private bool canFlip = true;
    private float flipTime = 3f;
    private float flipChance = 0f;
    private float chargeTime = 2f;

    private bool isCharging = false;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Time.time > flipChance)
        {
            if (Random.Range(0, 10) >= 5) FlipFacing();
            flipChance = Time.time + flipTime;
        }
    }

    private void FlipFacing()
    {
        if (!canFlip) return;
        graphics.transform.localScale =
            new Vector3(
                graphics.transform.localScale.x * -1,
                graphics.transform.localScale.y,
                graphics.transform.localScale.z);
    }
}