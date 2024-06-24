using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = .25f;
    private Vector3 vel = Vector3.zero;

    [SerializeField] private Transform player;

    private void Update()
    {
        Vector3 playerPos = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, playerPos, ref vel, smoothTime);
    }
}
