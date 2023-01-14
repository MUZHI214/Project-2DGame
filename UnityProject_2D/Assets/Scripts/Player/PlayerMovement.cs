using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private BoxCollider2D coll2d;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    private enum MovementState { idle, running, jumping, falling }
    // private MovementState state = MovementState.idle;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        coll2d = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(dirX * moveSpeed, rb2d.velocity.y);

        // jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        UpdateAnimation();

    }

    private void UpdateAnimation()
    {
        MovementState state;

        // run left
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        // run right
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb2d.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb2d.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);

    }

    // 检查player下方是否是ground，会在player下方生成一个检测区域
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll2d.bounds.center, coll2d.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

}
