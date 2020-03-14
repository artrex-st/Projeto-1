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
        teste=fIsground();
        if (Input.GetKeyDown("space") && fIsground())
            Jump();
        else
            rb.gravityScale += 2*Time.deltaTime;
    }
    void Jump()
    {
        rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        rb.gravityScale = 1;

    }
    private bool fIsground()
    {
        return Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y - 0.4f), new Vector2(0.5f, 0.2f), 0, Vector2.down,0.2f, GroundLayer);  
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.localPosition.x, transform.localPosition.y-0.4f, 0), new Vector3(0.5f,0.2f));
    }
}