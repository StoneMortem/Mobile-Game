using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private bool inCombat = false;
    [SerializeField] float runSpeed = 5f;
    private Rigidbody2D rb;
    private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        CameraScript.Instance.SetPlayer(transform);
    }

    void FixedUpdate()
    {
        if (!inCombat)
        {
            rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);
        }
    }

    public void EnterCombat()
    {
       inCombat = true;

        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        animator.SetBool("isRunning", false);
    }

    public void ExitCombat()
    {
        inCombat = false;

        rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);

        animator.SetBool("isRunning", true);
    }
}
