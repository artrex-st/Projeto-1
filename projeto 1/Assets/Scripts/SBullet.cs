using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBullet : MonoBehaviour
{
    public float speed=20f,Dmg=15f;
    public Rigidbody2D BulletBody;
    public bool StartSelfDestruction;
    void Start()
    {
        BulletBody = GetComponent<Rigidbody2D>();
        BulletBody.AddForce(new Vector2(transform.right.x * speed,0),ForceMode2D.Impulse); 
    }

    //void OnTriggerEnter2D(Collider2D hit)
    //{
    //    Debug.Log("Hit");
    //    //if (hit.gameObject.tag.Equals("Enemy"))
    //    Destroy(gameObject);    
    //}
    private void Update()
    {
        if(StartSelfDestruction)
            transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * 1.2f, Time.deltaTime * 2);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartSelfDestruction = true;
        Destroy(gameObject,0.1f);
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, GetComponent<CircleCollider2D>().radius*1.1f);
        if(collision.gameObject.name=="Enemy")
            collision.gameObject.GetComponent<FollowPlayerFly>().TakeDmg(Dmg);
    }
}
