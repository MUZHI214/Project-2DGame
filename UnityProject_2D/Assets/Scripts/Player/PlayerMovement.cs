using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(dirX * moveSpeed, rb2d.velocity.y);

        // jump
        if (Input.GetButtonDown("Jump"))
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        UpdateAnimation();


    }

    private void UpdateAnimation()
    {
        // run left
        if (dirX > 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = false;
        }
        // run right
        else if (dirX < 0f)
        {
            anim.SetBool("running", true);
            sprite.flipX = true;
        }
        else
        {
            anim.SetBool("running", false);
        }
    }
}
