using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private bool inCombat = false;
    public float runSpeed = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        CameraScript.Instance.SetPlayer(transform);
    }

    void FixedUpdate()
    {
        if (!inCombat)
        {
            rb.linearVelocity = new Vector2(runSpeed, rb.linearVelocity.y);
        }
    }
}
