using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider2D))]

public class Player : MonoBehaviour
{

    public float
        MaxHp = 100,
        speed = 10.0f, 
        JumpForce = 10.0f,
        sizeMorph=1.5f, 
        testeVelocidade, 
        Moving;
    private float
        CurrHP;
    public bool 
        canMove = true, 
        isCrouch = false;

    public LayerMask GroundLayer;

    private Rigidbody2D rb;
    public CapsuleCollider2D BodySize;
    private SpriteRenderer SpritePlayer;
    public Animator AnimatorPlayer;
    public float CD;
    public HealthBar HealthUI;

    //public BoxCollider2D BcPlayer;
    public string teste;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BodySize = GetComponent<CapsuleCollider2D>();
        SpritePlayer = GetComponent<SpriteRenderer>();
        AnimatorPlayer = GetComponent<Animator>();
        //BcPlayer = GetComponentInChildren<BoxCollider2D>();
        HealthUI.SetMaxHealth(MaxHp);
        CurrHP = MaxHp;

    }
    //  ##  ##  ##  ##  ##  ##  ## //
    void Update()
    {
        CD = Data.recarga;
        if (fIsGround())
        {
            canMove = true;
            AnimatorPlayer.SetTrigger("IsGround");
        }

        //
        if(fIsGround())
            AnimatorPlayer.SetBool("onAir",false);
        else
            AnimatorPlayer.SetBool("onAir", true);  
        //
        if (Input.GetButton("Fire3") && !isCrouch) //run
            Moving = Input.GetAxis("Horizontal") * speed * 2;
        else
            Moving = Input.GetAxis("Horizontal") * speed; //Walk Base

        //flip Sprite
        if (Moving < 0)
            SpritePlayer.flipX = true;
        else
            if (Moving > 0)
            SpritePlayer.flipX = false;
        // animator move
        AnimatorPlayer.SetFloat("IsMove", Mathf.Abs(Moving));
                                   
        // Move OK
        if (canMove)
            rb.velocity = new Vector2(Moving, rb.velocity.y);

        // Jump
        if (Input.GetButtonDown("Jump") &&(Data.recarga>=0.1f))
            Jump(!isCrouch);



        //****************        TESTES          *********************\\
        teste = "0";
        testeVelocidade = Moving;
    }
    void Jump(bool PowerJump)
    {
        Data.recarga = 0;
        rb.velocity = new Vector2(rb.velocity.x - (Time.deltaTime * 1.2f), rb.velocity.y);
        if (fIsGround() && !PowerJump)
        {
            rb.AddForce(new Vector2(0, JumpForce * 1.2f), ForceMode2D.Impulse);
            canMove = true;
            AnimatorPlayer.SetTrigger("Jump");
        }
        else
            if (fIsGround() && PowerJump)
            {
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                canMove = true;
                AnimatorPlayer.SetTrigger("Jump");
            }
            else
                canMove = false;
        CurrHP -= 20;
        HealthUI.SetHealth(CurrHP);

    }
    private bool fIsGround()
    {
        return Physics2D.OverlapCapsule(PositionPlayer(), new Vector2(BodySize.size.x-0.3f,BodySize.size.y),BodySize.direction,0,GroundLayer,0,0.1f);
        //return Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(BodySize.size.x * 0.4f, BodySize.size.y * 0.9f), 0, Vector2.down, 0.1f, GroundLayer);
    }

    private Vector2 PositionPlayer()
    {
        return new Vector2(transform.localPosition.x, transform.localPosition.y);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(PositionPlayer(), new Vector3(BodySize.size.x * 0.9f, transform.localScale.y * 0.4f));
        
    }
}

/*
 * crouch
 * if (Input.GetButtonDown("Fire1") && !isCrouch) // Crouch: Start
        {
            BodySize.size /= sizeMorph;
            Moving = 0;
            isCrouch = true;
        }
        else
        if (Input.GetButtonUp("Fire1") && isCrouch) // Crouch: Stop
        {
            BodySize.size *= sizeMorph;
            Moving = Input.GetAxis("Horizontal") * speed;
            isCrouch = false;
        }
        else 
        
     
*/
