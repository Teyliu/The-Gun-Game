using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashTime = 0.5f;
    public float dashCooldown = 2f;
    public float dashInvulnerabilityTime = 0.3f;
    public KeyCode dashKey = KeyCode.Space;

    private Rigidbody2D rb;
    private bool isDashing = false;
    private float currentDashTime = 0f;
    private float currentDashCooldown = 0f;
    private bool isInvulnerable = false;
    private float currentInvulnerabilityTime = 0f;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 movement = new(horizontalInput, verticalInput);

        anim.SetFloat("MoveX", (movement.x));
        anim.SetFloat("MoveY", (movement.y));

        if (!isDashing && currentDashCooldown <= 0f && Input.GetKeyDown(dashKey))
        {
            isDashing = true;
            currentDashTime = dashTime;
            currentDashCooldown = dashCooldown;
            rb.velocity = Vector2.zero;
        }

        if (isDashing)
        {
            if (!isInvulnerable)
            {
                isInvulnerable = true;
                currentInvulnerabilityTime = dashInvulnerabilityTime;
            }

            currentDashTime -= Time.deltaTime;
            if (currentDashTime <= 0f)
            {
                isDashing = false;
                isInvulnerable = false;
            }
            else
            {
                Vector2 dashDirection = movement.normalized != Vector2.zero ? movement.normalized : rb.velocity.normalized;
                rb.velocity = dashSpeed * dashDirection;
            }
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, movement.normalized * moveSpeed, Time.deltaTime * 10f);
        }

        currentDashCooldown -= Time.deltaTime;
        currentInvulnerabilityTime -= Time.deltaTime;
        if (currentInvulnerabilityTime <= 0f)
        {
            isInvulnerable = false;
        }

        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
}