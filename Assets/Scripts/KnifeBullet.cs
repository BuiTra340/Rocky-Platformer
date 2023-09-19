using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBullet : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.right * 20;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            anim.Play("Knife_Destroy");
            collision.gameObject.GetComponent<Enemy>().takeDamage(PlayerController.damage);
            Destroy(gameObject, 0.1f);
        }
        if (collision.gameObject.tag == "Boss")
        {
            anim.Play("Knife_Destroy");
            collision.gameObject.GetComponent<Boss>().takeDamage(PlayerController.damage);
            Destroy(gameObject, 0.1f);
        }
    }
}
