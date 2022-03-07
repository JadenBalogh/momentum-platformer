using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float respawnHeight = 6;
    [SerializeField] private float fallThresholdY = -6;
    [SerializeField] private Transform footRef;
    [SerializeField] private float groundedRadius = 0.2f;
    [SerializeField] private float groundedCooldown = 0.05f;
    [SerializeField] private LayerMask groundedMask;

    private bool canGroundCheck = true;
    private bool isGrounded = true;

    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody2D;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(moveInput * moveSpeed, rigidbody2D.velocity.y);
        spriteRenderer.flipX = rigidbody2D.velocity.x < 0;

        animator.SetBool("IsMoving", Mathf.Abs(moveInput) > 0.01f);

        if (transform.position.y < fallThresholdY)
        {
            transform.position = new Vector3(transform.position.x, respawnHeight);
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody2D.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            StartCoroutine(GroundCheckWait());
            isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        if (canGroundCheck && Physics2D.OverlapCircle(footRef.position, groundedRadius, groundedMask))
        {
            isGrounded = true;
        }
    }

    public void FlipVelocity()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -rigidbody2D.velocity.y);
    }

    private IEnumerator GroundCheckWait()
    {
        canGroundCheck = false;
        yield return new WaitForSeconds(groundedCooldown);
        canGroundCheck = true;
    }
}
