using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_frog : Enemy
{
    private Rigidbody2D rb;
    private Collider2D coll;
    public LayerMask ground;
    public Transform leftpoint, rightpoint;
    private bool Faceleft = true;
    public float speed = 2, Jumpforce = 3;
    private float leftx, rightx;
   // public Animator anim;

    // Start is called before the first frame update
     protected override void Start()
    {
        base.Start();
        speed = 2;
        Jumpforce = 3;
       // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        switchanim();
    }

    void movement()
    {
        if (Faceleft)
        {
            if (rb.transform.position.x - speed < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
            else if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed , Jumpforce );
            }

        }
        else
        {
            if (rb.transform.position.x + speed > rightx)
            {

                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
            else if (coll.IsTouchingLayers(ground))
            {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed , Jumpforce );
            }
           
        }
    }

    void switchanim()
    {
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0.1)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        } 
        if(coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
            anim.SetBool("falling", false);
        }
    }

    
}

