using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    [SerializeField] private Collider2D coll;
    [SerializeField] private float hurtForce = 5f;

    [SerializeField] private LayerMask ground;

    private enum State {idle, running, jumping, falling, hurt }
    private State state =  State.idle;


    // Update is called once per frame
    void Update()
    {
        if(state != State.hurt)
        {
            Movement();
        }
        AnimationState();
        anim.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Cherry")
        {
            Destroy(collision.gameObject);
            PermanentUI.perm.cherryNum++;
            PermanentUI.perm.cherryText.text = PermanentUI.perm.cherryNum.ToString();
        }
        if (collision.tag == "scene1")
        {
            SceneManager.LoadScene("score1");
        }
        if (collision.tag == "scene2")
        {
            SceneManager.LoadScene("score2");
        }
        if (collision.tag == "scene3")
        {
            SceneManager.LoadScene("score3");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Frog")
        {
            Frog frog = collision.gameObject.GetComponent<Frog>();

            if(state == State.falling)
            {
                frog.JumpON();
                Jump();
            }
            else
            {
                state = State.hurt;
                if(collision.gameObject.transform.position.x > transform.position.x)
                {
                    rb.velocity = new Vector2(-hurtForce, rb.velocity.y); 
                }
                else
                {
                    rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                }
            }
        }
        else if(collision.gameObject.tag == "Eagle")
            {
                Eagle eagle = collision.gameObject.GetComponent<Eagle>();

                if (state == State.falling)
                {
                    eagle.JumpON();
                    Jump();
                }
                else
                {
                    state = State.hurt;
                    if (collision.gameObject.transform.position.x > transform.position.x)
                    {
                        rb.velocity = new Vector2(-hurtForce, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(hurtForce, rb.velocity.y);
                    }
                }
            }
       
    }
    
    void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        Debug.Log(hDirection);
   
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1); 
        }
        else if (hDirection > 0)
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);  
        }
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 7f);
        state = State.jumping;
    }

    void AnimationState()
    {
        if(state == State.jumping)
        {
            if(rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }

        else if(state == State.falling)
        {
            if(coll.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }

        else if(state == State.hurt)
        {
            if(Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                state = State.idle;
            }
        }

        else if(Mathf.Abs(rb.velocity.x) > 0.1f)
        {
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
       
    }

}
