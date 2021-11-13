using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class movement : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;
    public float moveDirection = 0;

    // Animator and Scorecontroller 
    public Animator animator;
    public score scorecontroll;
   
   //All Bools 
    bool facingRight = true;
    bool isGrounded = false;
   
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    Transform t;
 
    public void pickUpKey()
    {
        Debug.Log("Key Picked Up By PLayer");
        scorecontroll.increaseScore(10);
    }
    public void KillPLayer()
    {
        animator.SetBool("Death",true);
        Debug.Log("Player Killed By Enenmy");
        Destroy(gameObject);
        ReloadLevel();
       
    }
    
    private void ReloadLevel()
    {
        SceneManager.LoadScene(4);
    }
    // Use this for initialization
    void Start()
    {
        t = transform;
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }


        // Change facing direction
        if (moveDirection != 0)
        {
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }
            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }

        // Jumping
        if ((Input.GetKeyDown(KeyCode.W ) || Input.GetKeyDown(KeyCode.Space))&& isGrounded )
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            animator.SetBool("jump",true);
        }
        else
        {
            animator.SetBool("jump",false);
        }

        // Crouching
        if ((Input.GetKeyDown(KeyCode.S)||(Input.GetKeyDown(KeyCode.RightControl)))&& isGrounded)
        {
            animator.SetBool("crouch",true);
        }
        else
        {
            animator.SetBool("crouch",false);
        }
        // animation 
        animator.SetFloat("Speed",Mathf.Abs( moveDirection));

    }

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
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

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

    
        
    }
}