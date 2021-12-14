using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class movement : MonoBehaviour
{
    public float Speed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    private float moveDirection = 0;

    public Animator animator;
    public score scoreControll;


    private bool isfacingRight = true;
    private bool isGrounded = false;

    private Rigidbody2D rigidbody2d;
    private CapsuleCollider2D mainCollider;
    private Transform t;

    public void pickUpKey()
    {
        scoreControll.increaseScore(10);
    }
    public void KillPLayer()
    {
        animator.SetBool("Death", true);
        ReloadLevel();
    }
    private void ReloadLevel()
    {
        Scene sur = SceneManager.GetActiveScene();
        SceneManager.LoadScene(sur.buildIndex);
    }
    private void movementcontrols()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (isGrounded || Mathf.Abs(rigidbody2d.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded)
            {
                moveDirection = 0;
            }
        }
    }
    private void changeFacingDirection()
    {
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !isfacingRight)
            {
                isfacingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
            if (moveDirection < 0 && isfacingRight)
            {
                isfacingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }
    }
    private void Jumping()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && isGrounded)
        {
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, jumpHeight);
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);
        }
    }
    private void Crouching()
    {
        if ((Input.GetKeyDown(KeyCode.S) || (Input.GetKeyDown(KeyCode.RightControl))) && isGrounded)
        {
            animator.SetBool("crouch", true);
        }
        else
        {
            animator.SetBool("crouch", false);
        }
    }
    void Start()
    {
        t = transform;
        rigidbody2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        rigidbody2d.freezeRotation = true;
        rigidbody2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rigidbody2d.gravityScale = gravityScale;
        isfacingRight = t.localScale.x > 0;
    }
    void Update()
    {
        movementcontrols();
        changeFacingDirection();
        Jumping();
        Crouching();
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));
        rigidbody2d.velocity = new Vector2((moveDirection) * Speed, rigidbody2d.velocity.y);
    }
    private void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }
    }
}