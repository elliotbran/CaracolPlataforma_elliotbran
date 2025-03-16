using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D _rb;
    Animator _anim;

    Vector2 _movementDirection;
    int _facingDirection = 1;
    bool _isGrounded;

    [Header("Player Parameters")]
    [Range(1,10)]
    public float jumpVelocity;

    [Range(1, 10)]
    public float playerSpeed;

    [Header("Falling Parameters")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;    
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
  
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        _movementDirection = new Vector2 (x, y);

        _anim.SetFloat("xVelocity", _movementDirection.magnitude);
        _anim.SetFloat("yVelocity", _rb.velocity.y);

        if (_movementDirection.x != 0)
        {
            _facingDirection = _movementDirection.x > 0 ? 1 : -1;
        }
        transform.localScale = new Vector2(_facingDirection, 1);

        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        Jump();
        BetterJumping();
    }
    private void Walk(Vector2 dir)
    {
        _rb.velocity = (new Vector2(dir.x * playerSpeed, _rb.velocity.y));
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpVelocity;
            _isGrounded = false;
            _anim.SetBool("isJumping", !_isGrounded);
        }
    }

    private void BetterJumping()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _anim.SetBool("isJumping", !_isGrounded);
        }
    }
}
