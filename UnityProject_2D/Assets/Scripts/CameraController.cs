using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField] private Transform playerLoc;

    private void Update()
    {
        transform.position = new Vector3(playerLoc.position.x, playerLoc.position.y, transform.position.z);
    }
}
