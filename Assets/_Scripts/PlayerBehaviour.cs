using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Joystick joystick;
    public float joystickHorizontalSensitivity;
    public float joystickVerticalSensitivity;
    public bool IsGrounded;
    public bool IsJumping;
    public Transform spawnPoint;

    public float horizontalForce;
    public float verticalForce;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
    }

    void _Move()
    {
        if(IsGrounded)
        {
            if (joystick.Horizontal > joystickHorizontalSensitivity)
            {
                //move right
                _rigidbody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                _spriteRenderer.flipX = false;
                _animator.SetInteger("AnimState", 1);
            }
            else if (joystick.Horizontal < -joystickHorizontalSensitivity)
            {
                //move left
                _rigidbody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                _spriteRenderer.flipX = true;
                _animator.SetInteger("AnimState", 1);


            }
            
            else if (!IsJumping)
            {
                //idle
                _animator.SetInteger("AnimState", 0);

            }

            if (joystick.Vertical > joystickVerticalSensitivity)
            {
                //Jump
                _rigidbody2D.AddForce(Vector2.up * verticalForce * Time.deltaTime);
                _animator.SetInteger("AnimState", 2);
                IsJumping = true;

            }
            else
            {
                IsJumping = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGrounded = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //respawn
        if(collision.gameObject.CompareTag("DeathPlane"))
        {
            transform.position = spawnPoint.position;
        }
    }
}
