using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject player;
    public Vector3 CameraOffset;
    // Use this for initialization
    void Start()
    {
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position - CameraOffset, 5 * Time.deltaTime);
    }
}
