using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playercontoller : MonoBehaviour
{

    [SerializeField]private Rigidbody2D rb;
    public Collider2D coll;
    public float speed;
    public float jumpspeed;
    [SerializeField]private Animator anim;
    public LayerMask Ground;
    public int cherry = 0;
    public Text Cherrynum;
    private bool isHurt = false;
    public AudioSource JumpAudio;
    public AudioSource HurtAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
             movement();
        }
       
        switchanim();
    }

    //move
    void movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedirection = Input.GetAxisRaw("Horizontal");

        //左右移動
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }
        //人物面向
        if (facedirection != 0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

        //跳躍
        if (Input.GetKeyDown(KeyCode.Space) && coll.IsTouchingLayers(Ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpspeed * Time.deltaTime);
            anim.SetBool("jumping", true);
            JumpAudio.Play();
        }
    }

    //變換動畫
    void switchanim()
    {
        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(Ground))
        {
            anim.SetBool("falling", true);
        }
        if (anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }else if (isHurt)
        {
            anim.SetBool("hurt", true);
            if(Mathf.Abs(rb.velocity.x) < 0.3f)
            {
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
                anim.SetFloat("running", 0);
            }
        }else if (coll.IsTouchingLayers(Ground))
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }

    //吃櫻桃
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "collection")
        {
            Destroy(collision.gameObject);
            cherry += 1;
            Cherrynum.text = cherry.ToString();
       

        }

    }

    //消滅敵人
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                enemy.Jumpon();               
                rb.velocity = new Vector2(rb.velocity.x, jumpspeed * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            else if(transform.position.x < collision.gameObject.transform.position.x)
            {
                HurtAudio.Play();
                rb.velocity = new Vector2(-3, rb.velocity.y);
                isHurt = true;                
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                HurtAudio.Play();
                rb.velocity = new Vector2(3, rb.velocity.y);
                isHurt = true;
            }
        }
         
    }
}
