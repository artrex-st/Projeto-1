using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 10.0f, JumpForce = 10.0f,sizeMorph=1.5f, testeVelocidade, Moving;
    public bool canMove = true, isCrouch = false;
    public LayerMask GroundLayer;
    private Rigidbody2D rb;
    public CapsuleCollider2D BodySize;
    private SpriteRenderer SpritePlayer;
    public Animator AnimatorPlayer;
    public float CD;

    //public BoxCollider2D BcPlayer;
    public string teste;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BodySize = GetComponent<CapsuleCollider2D>();
        SpritePlayer = GetComponent<SpriteRenderer>();
        AnimatorPlayer = GetComponent<Animator>();
        //BcPlayer = GetComponentInChildren<BoxCollider2D>();
    }
    //  ##  ##  ##  ##  ##  ##  ## //
    void Update()
    {
        CD = Data.recarga;
        if (fIsGround())
            canMove = true;
        if (Input.GetButton("Fire3") && !isCrouch)
            Moving = Input.GetAxis("Horizontal") * speed * 2;
        else
        if (Input.GetButtonDown("Fire1"))
        {
            BodySize.size /= sizeMorph;
            Moving = Input.GetAxis("Horizontal") * speed / 4;
            isCrouch = true;
        }
        else
        if (Input.GetButtonUp("Fire1") && isCrouch)
        {
            BodySize.size *= sizeMorph;
            Moving = Input.GetAxis("Horizontal") * speed;
            isCrouch = false;
        }
        else
        if(isCrouch)
            Moving = Input.GetAxis("Horizontal") * speed/2;
                else
                Moving = Input.GetAxis("Horizontal") * speed;

        //flip
        if (Moving < 0)
            SpritePlayer.flipX = true;
        else
            if (Moving > 0)
            SpritePlayer.flipX = false;
        // animator move
        AnimatorPlayer.SetFloat("IsMove", Mathf.Abs(Moving));






        if(fIsGround())
            AnimatorPlayer.SetBool("onAir",false);
        else
            AnimatorPlayer.SetBool("onAir", true);
        if (fIsWall() == "Left" && Moving < 0 && canMove && !isCrouch)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            if (fIsWall() == "Right" && Moving > 0 && canMove && !isCrouch)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            if (canMove)
            rb.velocity = new Vector2(Moving, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && !isCrouch &&(Data.recarga>=0.1f))
            Jump();
        //****************        TESTES          *********************\\
        teste = fIsWall();
        testeVelocidade = Moving;
    }
    void Jump()
    {
        Data.recarga = 0;
        rb.velocity = new Vector2(rb.velocity.x - (Time.deltaTime * 1.2f), rb.velocity.y);
        if (fIsWall() == "" && fIsGround())
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            canMove = true;
            AnimatorPlayer.SetTrigger("Jump");
        }
        else
            canMove = false;
        if (fIsWall() == "Left")
        {
            rb.AddForce(new Vector2(Moving *2, JumpForce), ForceMode2D.Impulse);
            AnimatorPlayer.SetTrigger("WallJump");
        }
        else
            if (fIsWall() == "Right")
            {
                rb.AddForce(new Vector2(Moving *2, JumpForce), ForceMode2D.Impulse);
                AnimatorPlayer.SetTrigger("WallJump");
            }


    }
    private bool fIsGround()
    {
        return Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(BodySize.size.x * 0.4f, BodySize.size.y * 0.9f), 0, Vector2.down, 0.1f, GroundLayer);
    }
    private string fIsWall()
    {
        if (Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(BodySize.size.x * 0.9f, BodySize.size.y * 0.8f), 0, Vector2.left, 0.1f, GroundLayer))
            return "Left";
        else
            if (Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(BodySize.size.x * 0.9f, BodySize.size.y * 0.8f), 0, Vector2.right, 0.1f, GroundLayer))
            return "Right";
        else
            return "";

        /*
         * BoxCast
         * if (Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(BodySize.size.x * 0.9f, BodySize.size.y * 0.8f), 0, Vector2.left, 0.1f, GroundLayer))
            return "Left";
        else
            if (Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(BodySize.size.x * 0.9f, BodySize.size.y * 0.8f), 0, Vector2.right, 0.1f, GroundLayer))
            return "Right";
        else
            return "";
         * 
         * 
         * 
         */
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.localPosition.x, transform.localPosition.y, 0), new Vector3(BodySize.size.x * 0.4f, BodySize.size.y * 0.9f));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(transform.localPosition.x, transform.localPosition.y, 0), new Vector3(BodySize.size.x * 0.9f, transform.localScale.y * 0.4f));
    }
}