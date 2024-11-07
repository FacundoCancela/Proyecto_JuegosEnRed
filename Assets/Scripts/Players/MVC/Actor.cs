using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IActor
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] PlayerStats playerStats;

    private bool isGrounded = true;
    private bool canJump = true;

    private Vector3 spawnPoint;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        spawnPoint = transform.position;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            canJump = true;
        }
    }

    public void Move(Vector2 dir)
    {
        Vector2 velocity = new Vector2(dir.x * playerStats.speed, _rb.velocity.y);
        _rb.velocity = velocity;
    }

    public void Jump()
    {
        if (canJump)
        {
            if (!isGrounded)
            {
                canJump = false;
            }
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.AddForce(Vector2.up * playerStats.jumpPower, ForceMode2D.Impulse);
        }
    }

    public void Respawn()
    {
        transform.position = spawnPoint;
        _rb.velocity = Vector2.zero;
    }
}
