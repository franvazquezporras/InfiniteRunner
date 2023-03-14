using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public float smothing = 5f;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPos = player.position + offset; // Calculate the target camera position
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smothing * Time.deltaTime); // Smoothly move the camera towards the target position

    }
}
