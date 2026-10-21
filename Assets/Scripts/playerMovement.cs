using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
public class playerMovement : MonoBehaviour
{
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f,10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    float gravityScaleAtStart;
    Vector2 moveInput;
    Rigidbody2D myrb2D;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    bool isAlive = true;
    void Start()
    {
      myrb2D = GetComponent<Rigidbody2D>();
      myAnimator = GetComponent<Animator>();
      myBodyCollider  = GetComponent<CapsuleCollider2D>();
      myFeetCollider = GetComponent<BoxCollider2D>();
      gravityScaleAtStart = myrb2D.gravityScale;
    }

    void Update()
    {
        if(!isAlive){return;}
        run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed,myrb2D.velocity.y);
        myrb2D.velocity = playerVelocity;


        bool playerHasHorizontalMov = Mathf.Abs(myrb2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning",playerHasHorizontalMov);
    }
    void OnFire(InputValue value)
    {
        if(!isAlive){return;}
        Instantiate(bullet,gun.position,transform.rotation);
    }
    void OnJump(InputValue value)
    {
        if(!isAlive){return;}
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        if (value.isPressed)
        {
            myrb2D.velocity += new Vector2(0f,jumpSpeed); 
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalMov = Mathf.Abs(myrb2D.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalMov){
        transform.localScale = new Vector2(Mathf.Sign(myrb2D.velocity.x),1f);
        }
    }
    void OnMove(InputValue value)
    {
        if(!isAlive){return;}
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myrb2D.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing",false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myrb2D.velocity.x, moveInput.y * climbSpeed);
        myrb2D.velocity = climbVelocity;
        myrb2D.gravityScale = 0f;

        bool playerHasVerticalVel = Mathf.Abs(myrb2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing",playerHasVerticalVel);
    }
    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Hazard")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myrb2D.velocity = deathKick;
            FindObjectOfType<gameSession>().ProcessPlayerDeath();
        }
    }
}
