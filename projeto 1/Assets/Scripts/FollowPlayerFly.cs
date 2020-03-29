using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerFly : MonoBehaviour
{
	public Transform PlayerTransform;
	public LayerMask Layer;
	public float 
		Range = 4,
		FailRange= 2,
		MaxHp= 100,
		CurrHealth;
	protected bool dieing = false;
	public float teste;
	public Rigidbody2D BodyEnemy;
	public HealthBar HealthEnemy;
	public SpriteRenderer SpriteR;
	
	private Vector3 Base;

	void Start()
	{
		PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
		Base = transform.position;
		BodyEnemy = GetComponent<Rigidbody2D>();
		HealthEnemy = GetComponentInChildren<HealthBar>();
		HealthEnemy.SetMaxHealth(MaxHp);
		CurrHealth = MaxHp;
		SpriteR = GetComponent<SpriteRenderer>();

	}

	// Update is called once per frame
	void Update()
	{
		if(Physics2D.OverlapCircle(transform.position, Range, Layer) && !dieing)
		{
			BodyEnemy.velocity = (PlayerTransform.position - transform.position);
			SpriteR.color = Color.red;
			//Obj.position = Vector3.Lerp(Obj.position,new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, Obj.position.z), Time.deltaTime);
		}
		else
		{
			BodyEnemy.velocity = (Base - transform.position);
			SpriteR.color = Color.cyan;
			//Obj.position = Vector3.Lerp(Obj.position, Base, Time.deltaTime);
		}
		BodyEnemy.AddForce(new Vector2(0, Random.Range(FailRange*-1, FailRange)), ForceMode2D.Impulse);
		if (CurrHealth <= 0)
		{
			transform.localScale = Vector3.Lerp(transform.localScale, transform.localScale * Random.Range(0.9f, 1.1f), Time.deltaTime);
			death();
			SpriteR.color = new Color(Random.Range(0.4f, 1f), 0, 0);
		}

		//BodyEnemy.AddForceAtPosition(new Vector2(1,1), PlayerTransform.position, ForceMode2D.Impulse);

		// move do player é com
		//rb.velocity = new Vector2(Moving, rb.velocity.y);

		//pulo do player é com
		//rb.AddForce(new Vector2(0, JumpForce * 1.2f), ForceMode2D.Impulse);


	}
	public void TakeDmg(float Dmg)
	{
		CurrHealth -= Dmg;
		HealthEnemy.SetHealth(CurrHealth);
	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, Range);

	}
	void death()
	{
		Destroy(gameObject, 2);
		BodyEnemy.AddForce(new Vector2(0, Random.Range(FailRange * -5, FailRange*5)), ForceMode2D.Impulse);
		dieing = true;
	}

}
