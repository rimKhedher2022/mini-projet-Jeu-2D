using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour

{
  private  Rigidbody2D rb;
  private BoxCollider2D coll;
  private SpriteRenderer sprite ;
  private Animator anim;
   [SerializeField] private LayerMask jumpableGround; 

  private float dirX = 0f;

  [SerializeField] private float moveSpeed = 7f;
  [SerializeField] private float jumpForce = 7f;
   
   private enum MouvementState {idle,running,jumping,falling}

   private MouvementState state = MouvementState.idle;
   [SerializeField] private AudioSource jumpSoundEffect;
   
    // Start is called before the first frame update
   private void Start()
    {
       rb= GetComponent<Rigidbody2D>();
       coll= GetComponent<BoxCollider2D>();
       sprite = GetComponent<SpriteRenderer>();
       anim = GetComponent<Animator>();
    }

    // Update is called once per frame
   private void Update()
    {
         dirX =Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX*moveSpeed , rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSoundEffect.Play();
           rb.velocity= new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationUpdate ();

    }


    private void UpdateAnimationUpdate ()
    {
         MouvementState state; 

        if (dirX > 0f)
        {
            //anim.SetBool("running",true);
            state = MouvementState.running;
            sprite.flipX = false ;


        }

        else if (dirX < 0f)
        {
           //anim.SetBool("running",true); 
           state = MouvementState.running;
           sprite.flipX = true ;
        }

        else 
        {
            //anim.SetBool("running",false); 
            state = MouvementState.idle;
        }

        if(rb.velocity.y > .1f)
        {
            state = MouvementState.jumping;
        }

        else if (rb.velocity.y < -.1f)
        {
            state = MouvementState.falling;
        }


        anim.SetInteger("state",(int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down, .1f,jumpableGround);

    }
}
