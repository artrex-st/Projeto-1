using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	//public Rigidbody2D ; // Player Transform
	public Transform CameraTransform, PlayerTransform; // Camera Transform
	//public GameObject PlayerObject; // Capturar obj Do player


	// Use this for initialization
	void Start()
	{
		PlayerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
		CameraTransform = GetComponent<Transform>();

	}

	// Update is called once per frame
	void Update()
	{
		CameraTransform.position = 
			Vector3.Lerp(CameraTransform.position,
			new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, CameraTransform.position.z),2*Time.deltaTime);


	}
}
