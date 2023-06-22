using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    //move
    public float moveSpeed = 5f;
    public float dashSpeed = 10f;
    public float dashTime = 0.5f;
    public float dashCooldown = 2f;
    public float dashInvulnerabilityTime = 0.3f;
    public float trailDuration = 0.5f; // Duration of the trail in seconds
    public KeyCode dashKey = KeyCode.Space;

    private Rigidbody2D rb;

    //Dash 
    private bool isDashing = false;
    private float currentDashTime = 0f;
    private float currentDashCooldown = 0f;
    private bool isInvulnerable = false;
    private float currentInvulnerabilityTime = 0f;
    private Vector2 dashDirection;

    //animator reference
    private Animator anim;

    //Follow mouse position
    private Vector3 mousePos;
    private Camera mainCam;
    private bool facingRight = true;

    // Trail Renderer
    private TrailRenderer trailRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        transform.localScale = new Vector3(-1f, 1f, 1f);

        // Get the TrailRenderer component
        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector2 movement = new(horizontalInput, verticalInput);

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        anim.SetFloat("MoveX", movement.x);
        anim.SetFloat("MoveY", movement.y);

        if (mousePos.x > transform.position.x && facingRight)
        {
            Flip();
        }
        if (mousePos.x < transform.position.x && !facingRight)
        {
            Flip();
        }

        if (!isDashing && currentDashCooldown <= 0f && Input.GetKeyDown(dashKey))
        {
            StartCoroutine(StartDash(movement));
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
                StartCoroutine(EndDash());
            }
            else
            {
                rb.velocity = dashSpeed * dashDirection;

                if (!trailRenderer.enabled)
                {
                    trailRenderer.enabled = true;
                    StartCoroutine(DisableTrailAfterDelay());
                }
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

        void Flip()
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;

            facingRight = !facingRight;
        }
    }

    IEnumerator StartDash(Vector2 movement)
    {
        isDashing = true;
        currentDashTime = dashTime;
        currentDashCooldown = dashCooldown;
        rb.velocity = Vector2.zero;
        dashDirection = movement.normalized != Vector2.zero ? movement.normalized : rb.velocity.normalized;

        yield return null;
    }

    IEnumerator EndDash()
    {
        isDashing = false;
        isInvulnerable = false;
        yield return null;
    }

    IEnumerator DisableTrailAfterDelay()
    {
        yield return new WaitForSeconds(trailDuration);
        trailRenderer.enabled = false;
    }
}
