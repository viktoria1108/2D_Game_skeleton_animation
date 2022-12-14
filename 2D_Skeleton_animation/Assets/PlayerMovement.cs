using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float move;
    public float jump;
    public bool isJumping;
    private Rigidbody2D rb;
    public Animator animator;
    public bool facingRight = true;

    //dash
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer trailRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }
        move = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(move));

       
        if (facingRight == false && move > 0)
        {
            Flip();
        }
        else if (facingRight == true && move < 0)
        {
            Flip();
        }
        if (Input.GetButtonDown("Jump") && isJumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
            Debug.Log("jump");
        }
        
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(speed * move, rb.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D item)
    {
        if (item.gameObject.CompareTag("Graund"))
        {
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
    }
    private void OnCollisionExit2D(Collision2D item)
    {
        if (item.gameObject.CompareTag("Graund"))
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    
}
