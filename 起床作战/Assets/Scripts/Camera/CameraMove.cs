using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        offset = transform.position - new Vector3(0,0,0);
    }
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, 0.1f);
    }
}
