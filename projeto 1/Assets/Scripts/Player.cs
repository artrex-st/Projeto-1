using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 10.0f, JumpForce = 10.0f,sizeMorph=1.5f, testeVelocidade;
    public bool canMove = true, isCrouch = false;
    public LayerMask GroundLayer;
    private Rigidbody2D rb;
    public CapsuleCollider2D BodySize;
    private SpriteRenderer SpritePlayer;
    public Animator AnimatorPlayer;

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
        if (fIsGround())
            canMove = true;

        float translation;

        if (Input.GetButton("Fire3") && !isCrouch)
            translation = Input.GetAxis("Horizontal") * speed * 2;
        else
        if (Input.GetButtonDown("Fire1"))
        {
            BodySize.size /= sizeMorph;
            translation = Input.GetAxis("Horizontal") * speed / 4;
            isCrouch = true;
        }
        else
        if (Input.GetButtonUp("Fire1") && isCrouch)
        {
            BodySize.size *= sizeMorph;
            translation = Input.GetAxis("Horizontal") * speed;
            isCrouch = false;
        }
        else
        if(isCrouch)
            translation = Input.GetAxis("Horizontal") * speed/2;
                else
                translation = Input.GetAxis("Horizontal") * speed;

        //flip
        if (translation < 0)
            SpritePlayer.flipX = true;
        else
            if (translation > 0)
            SpritePlayer.flipX = false;
        // animator move
        AnimatorPlayer.SetFloat("IsMove", Mathf.Abs(translation));
        






        if (fIsWall() == "Left" && translation < 0 && canMove && !isCrouch)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            if (fIsWall() == "Right" && translation > 0 && canMove && !isCrouch)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            if (canMove)
            rb.velocity = new Vector2(translation, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && !isCrouch)
            Jump();
        //****************        TESTES          *********************\\
        teste = fIsWall();
        testeVelocidade = translation;
    }
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x - (Time.deltaTime * 1.2f), rb.velocity.y);
        if (fIsWall() == "" && fIsGround())
        {
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            canMove = true;
        }
        else
            canMove = false;
        if (fIsWall() == "Left")
            rb.AddForce(new Vector2(JumpForce / 2, JumpForce), ForceMode2D.Impulse);
        else
            if (fIsWall() == "Right")
            rb.AddForce(new Vector2(JumpForce / -2, JumpForce), ForceMode2D.Impulse);

    }
    private bool fIsGround()
    {
        return Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(BodySize.size.x * 0.4f, BodySize.size.y * 0.9f), 0, Vector2.down, 0.1f, GroundLayer);
    }
    private string fIsWall()
    {
        if (Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(BodySize.size.x * 0.9f, BodySize.size.y * 0.4f), 0, Vector2.left, 0.1f, GroundLayer))
            return "Left";
        else
            if (Physics2D.BoxCast(new Vector2(transform.localPosition.x, transform.localPosition.y), new Vector2(BodySize.size.x * 0.9f, BodySize.size.y * 0.4f), 0, Vector2.right, 0.1f, GroundLayer))
            return "Right";
        else
            return "";
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.localPosition.x, transform.localPosition.y, 0), new Vector3(BodySize.size.x * 0.4f, BodySize.size.y * 0.9f));
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(transform.localPosition.x, transform.localPosition.y, 0), new Vector3(BodySize.size.x * 0.9f, transform.localScale.y * 0.4f));
    }
}