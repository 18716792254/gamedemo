using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float damage=50f;
    public float TimerBetween = 0.15f;
    float Timer = 0;
    float EffectTime=0.2f;
    private AudioSource AS;
    private Light LT;
    private LineRenderer LR;
    private ParticleSystem PS;
    //…‰œﬂ
    private Ray ShootRay;
    private RaycastHit ShootHit;
    
    // Start is called before the first frame update
    void Start()
    {
        AS=GetComponent<AudioSource>();
        LT=GetComponent<Light>();
        LR=GetComponent<LineRenderer>();
        PS=GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0f) return;
        Timer +=Time.deltaTime;
        if (Input.GetMouseButton(0)&&Timer>=TimerBetween)
        {
            Timer = 0;
            Shoot();
        }
        if (Timer >= TimerBetween * EffectTime)
        {
            LT.enabled = false;
            LR.enabled = false;
        }
    }

    void Shoot()
    {
        LT.enabled = true;
        LR.SetPosition(0,transform.position);
        LR.enabled = true;
        PS.Play();
        AS.Play();
        //…‰œﬂºÏ≤‚
        ShootRay.origin = transform.position;
        ShootRay.direction = transform.forward;
        if(Physics.Raycast(ShootRay ,out ShootHit, 100, -1))
        {
            LR.SetPosition(1,ShootHit.point);
            EnemyHealthy EH = ShootHit.collider.GetComponent<EnemyHealthy>();
            if(EH == null)return;
            if(!EH.EnemyDeath)
            {
                EH.Damage(damage, ShootHit.point);
            }
        }
        else
        {
            LR.SetPosition(1, transform.position + transform.forward * 100);
        }
    }
    
}
