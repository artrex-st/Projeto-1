using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	public Transform Obj, PlayerTransform;
	public LayerMask Layer;
	public float 
		Range = 4,
		FailRange= 2;
	public float teste;
	public Rigidbody2D BodyEnemy;
	
	private Vector3 Base;

	void Start()
	{
		PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
		Obj = GetComponent<Transform>();
		Base = transform.position;
		BodyEnemy = GetComponent<Rigidbody2D>();

	}

	// Update is called once per frame
	void Update()
	{
		if(Physics2D.OverlapCircle(Obj.position, Range, Layer))
		{
			BodyEnemy.velocity = (PlayerTransform.position - transform.position);
			//Obj.position = Vector3.Lerp(Obj.position,new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, Obj.position.z), Time.deltaTime);
		}
		else
		{
			BodyEnemy.velocity = (Base - transform.position);
			//Obj.position = Vector3.Lerp(Obj.position, Base, Time.deltaTime);
		}

		BodyEnemy.AddForce(new Vector2(0, Random.Range(FailRange*-1, FailRange)), ForceMode2D.Impulse);


		//BodyEnemy.AddForceAtPosition(new Vector2(1,1), PlayerTransform.position, ForceMode2D.Impulse);

		// move do player é com
		//rb.velocity = new Vector2(Moving, rb.velocity.y);

		//pulo do player é com
		//rb.AddForce(new Vector2(0, JumpForce * 1.2f), ForceMode2D.Impulse);


	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(Obj.position, Range);

	}

}
