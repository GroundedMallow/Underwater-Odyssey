using System;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    [Header("Stats")]
    private Rigidbody2D rb;
    public float speed;
    public bool isFacingRight;
    private float moveInput;

    [Header("Jump")]
    public float jumpForce;
    public float jumpTime;
    public float checkRadius;

    private bool isGrounded;
    public Transform groundCheck;
    private float jumpTimeCounter;
    private bool isJumping;

    public LayerMask _layerGround;

    [Header("ATK")]
    public Transform atkPoint;
    public float atkRange;
    public int atkDMG;

    public LayerMask _layerEnemy;

    [Header("Wall Slide")]
    private bool isWallSliding;
    public float wallSlideSpeed;
    public Transform wallCheck;

    public LayerMask _layerWall;

    [Header("Wall Dash")]
    private Vector2 lastPos;
    private bool isMoving;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isWallSliding)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

            if(moveInput > 0 && isFacingRight)
            {
                Flip();
            }
            else if(moveInput < 0 && !isFacingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, _layerGround);

        //isGrounded = true if circle collides w/ground
        if(isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;

            rb.velocity = Vector2.up * jumpForce;
        }
        //press up longer to jump higher
        if (Input.GetKey(KeyCode.W))
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime; //decreases by the sec
            }
            else
            {
                isJumping = false; //no extra jump mid-air
            }

        }

        /* getkey: indefenitly
           getkeydown: once */

        if (!isWallSliding && Input.GetMouseButtonDown(0))
        {
            AttackAction();
        }

        WallSlide();
        WallDash();
    }

    private void AttackAction()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(atkPoint.position, atkRange, _layerEnemy);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("Enemy hit");
            enemy.GetComponent<Enemy_DMG>().TakeDMG(atkDMG);
        }
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, .2f, _layerWall);
    }

    private void WallSlide()
    {
        if(IsWalled() && !isGrounded)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallDash()
    {
        if(isWallSliding && Input.GetMouseButtonUp(1)) //right click to move
        {
            lastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isMoving = true;
        }

        if(isMoving && (Vector2)transform.position != lastPos)
        {
            rb.gravityScale = 0f;
            speed = 20f;
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, lastPos, step);
        }
        else
        {
            speed = 7f;
            rb.gravityScale = 3;
            isMoving = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (atkPoint == null)
            return;

        Gizmos.DrawWireSphere(atkPoint.position, atkRange);
    }
}
