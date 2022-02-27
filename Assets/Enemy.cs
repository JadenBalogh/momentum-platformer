using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float leftPatrolPoint;
    [SerializeField] private float rightPatrolPoint;

    private bool isMovingRight = true;

    private SpriteRenderer spriteRenderer;
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float dirX = isMovingRight ? 1 : -1;
        rigidbody2D.velocity = new Vector2(dirX * moveSpeed, rigidbody2D.velocity.y);
        if (isMovingRight && transform.position.x > rightPatrolPoint)
        {
            isMovingRight = false;
        }
        else if (!isMovingRight && transform.position.x < leftPatrolPoint)
        {
            isMovingRight = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.TryGetComponent<Player>(out Player player))
        {
            GameManager.EndGame(false);
        }
    }
}
