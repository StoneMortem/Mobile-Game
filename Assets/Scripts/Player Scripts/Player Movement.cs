using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private bool inCombat = false;

    [SerializeField] float runSpeed = 5f;
    private float normalRunSpeed;
    [SerializeField] float tappingRunSpeed;
    private Rigidbody2D rb;
    
    
    private Animator animator;
    private float normalSpeed;
    private float tapSpeed;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        CameraScript.Instance.SetPlayer(transform);

        normalSpeed = animator.speed;
        tapSpeed = normalSpeed * 1.5f;

        normalRunSpeed = runSpeed;
        tappingRunSpeed = runSpeed + 1;
    }

    void Update()
    {
        bool tapping = false;

        // Touch screen
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            tapping = true;

        // Mouse
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
            tapping = true;

        if (!inCombat)
        {
            if (tapping)
            {
                animator.speed = tapSpeed;
                runSpeed = tappingRunSpeed;
            }
            else
            {
                animator.speed = normalSpeed;
                runSpeed = normalRunSpeed;
            }
        }
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
