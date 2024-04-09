using UnityEngine;

public class HedgehogAI : MonoBehaviour
{
    [SerializeField] float runSpeed = 7f;
    [SerializeField] GameObject graphics;
    [SerializeField] private float chargeDelay = 0.5f;
    [SerializeField] private float flipTime = 2f;

    private Animator animator;
    private Rigidbody2D rigidbody2d;

    private bool facingRight = false;
    private bool canFlip = true;
    
    private float nextFlip = 0f;
    private float startChargeTime = 0f;
    

    private bool isCharging = false;


    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canFlip && Time.time > nextFlip)
        {
            if (Random.Range(0, 10) >= 5) FlipFacing();
            nextFlip = Time.time + flipTime;
        }

        animator.SetBool("isCharging", isCharging);
    }

    private void FlipFacing()
    {
        facingRight = !facingRight;
        graphics.transform.localScale =
            new Vector3(
                graphics.transform.localScale.x * -1,
                graphics.transform.localScale.y,
                graphics.transform.localScale.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (facingRight != other.transform.position.x > transform.position.x)
                FlipFacing();
            canFlip = false;
            isCharging = true;
            startChargeTime = Time.time + chargeDelay;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canFlip = true;
            isCharging = false;
            rigidbody2d.velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isCharging && other.CompareTag("Player") && Time.time > startChargeTime)
        {
            rigidbody2d.AddForce((facingRight ? Vector3.right : Vector3.left) * runSpeed);

        }
    }
}