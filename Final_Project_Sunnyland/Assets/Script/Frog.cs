using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private float leftCap;
    [SerializeField] private float rightCap;

    [SerializeField] private float jumpLenght;
    [SerializeField] private float jumpHeight;

    [SerializeField] private LayerMask ground;

    private enum State { idle, jumping, falling }
    private State state_Frog = State.idle;

    public bool facingLeft = true;

    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FrogState();
        anim.SetInteger("state", (int)state_Frog);
    }

    private void Move()
    {
        if (facingLeft)
        {
            if (transform.position.x > leftCap)
            {
                if (transform.localScale.x == -1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLenght, jumpHeight);
                    state_Frog = State.jumping;
                }
            }
            else
            {
                facingLeft = false;
            }
        }
        else
        {

            if (transform.position.x < rightCap)
            {
                if (transform.localScale.x == 1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLenght, jumpHeight);
                    state_Frog = State.jumping;
                }
            }
            else
            {
                facingLeft = true;
            }
        }
    }

    private void FrogState()
    {
        if (state_Frog == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state_Frog = State.falling;
            }
        }
        else if (state_Frog == State.falling)
        {
            if (coll.IsTouchingLayers(ground))
            {
                state_Frog = State.idle;
            }
        }
        else
        {
            state_Frog = State.idle;
        }
    }

    public void JumpON()
    {
        anim.SetTrigger("Death");
    }

    private void Death()
    {
        Destroy(this.gameObject);
    }
}
