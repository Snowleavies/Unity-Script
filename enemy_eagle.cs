using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_eagle : Enemy
{
    private Rigidbody2D rb;
    public float speed;
    private Collider2D coll;
    public float topy, bottomy;
    public Transform top, bottom;
    private bool isuo = true;

    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
        transform.DetachChildren();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        topy = top.position.y;
        bottomy = bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        if (isuo)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (rb.transform.position.y > topy)
            {           
                isuo = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (rb.transform.position.y < bottomy)
            {
                isuo = true;
            }
        }
        
    }

}
