using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 10.0f, JumpForce = 10.0f;
    public LayerMask GroundLayer;
    private Rigidbody2D rb;
    //public BoxCollider2D BcPlayer;
    public bool teste;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //BcPlayer = GetComponentInChildren<BoxCollider2D>();
    }
    //  ##  ##  ##  ##  ##  ##  ## //
    void Update()
    {
        float translation = Input.GetAxis("Horizontal") * speed;
        rb.velocity = new Vector2(translation, rb.velocity.y);
        if (Input.GetKeyDown("space") && fIsground())
            Jump();
        teste = fIsground();


    }
    void Jump()
    {
        rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
    }
    private bool fIsground()
    {
        return Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(transform.localScale.x * 0.5f, transform.localScale.y*0.9f), 0, Vector2.down,0.1f, GroundLayer);  
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.localPosition.x, transform.localPosition.y, 0), new Vector3(transform.localScale.x*0.5f, transform.localScale.y* 1.01f));
    }
}