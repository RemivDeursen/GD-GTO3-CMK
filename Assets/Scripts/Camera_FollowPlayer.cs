using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_FollowPlayer : MonoBehaviour {
	public Camera mainCamera;
	public GameObject player;
	public Vector3 CameraOffset;
	// Use this for initialization
	void Start () {
	}

	private void FixedUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, player.transform.position - CameraOffset, 4 * Time.deltaTime);
	}
}
