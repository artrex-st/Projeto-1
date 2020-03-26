using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
	public Transform Obj, PlayerTransform; // Camera Transform
	public bool TESTE;
	public LayerMask Layer;
	public float Range = 4;
	private Vector2 Base;

	void Start()
	{
		PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
		Obj = GetComponent<Transform>();
		Base = transform.position;

	}

	// Update is called once per frame
	void Update()
	{
		if(Physics2D.OverlapCircle(Obj.position, Range,Layer))
			Obj.position = Vector3.Lerp(Obj.position,new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, Obj.position.z), Time.deltaTime);
		else
			Obj.position = Vector3.Lerp(Obj.position, Base, Time.deltaTime);


	}
	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(Obj.position, Range);

	}

}
