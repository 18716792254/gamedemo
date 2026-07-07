using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Speed=6f;

    private Rigidbody rb;
    private Vector3 vel;
    private float currentMovement = 0f;
    private Animator ani;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
        ani=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Turning();
    }

    private void FixedUpdate()
    {
        rb.velocity = vel;
    }
    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = (transform.forward * v + transform.right * h).normalized;
        vel = dir * Speed;
        bool IsWalk = false;
        if (h != 0 || v != 0)
        {
            IsWalk = true;
        }
        ani.SetBool("Move",IsWalk);
    }

    void Turning()
    {
        Ray CameraRay=Camera.main.ScreenPointToRay(Input.mousePosition);
        //…‰œﬂºÏ≤‚
        int Mask=LayerMask.GetMask("Ground");
        RaycastHit FloorHit;
        bool IsTouch=Physics.Raycast(CameraRay,out FloorHit,100,Mask);
        if (IsTouch)
        {
            Vector3 V3=FloorHit.point-transform.position;
            V3.y=0;
            Quaternion quaternion=Quaternion.LookRotation(V3);
            rb.MoveRotation(quaternion);
        }
    }
}