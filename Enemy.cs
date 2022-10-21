using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Enemy : MonoBehaviour
{
     [SerializeField]protected Animator anim;
    protected AudioSource DeathAudio;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        DeathAudio = GetComponent<AudioSource>();
    }

    public void death()
    {
       
        Destroy(gameObject);
    }

    public void Jumpon()
    {
        anim.SetTrigger("death");
        DeathAudio.Play();
    }
}
