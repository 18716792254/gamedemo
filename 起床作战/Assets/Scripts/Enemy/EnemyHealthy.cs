using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyHealthy : MonoBehaviour
{
    public bool EnemyDeath = false;
    public AudioClip DeathAS;
    public AudioClip HurtAS;
    public float Blood=100;
    public ObjectPool<GameObject> Pool;
    public float blood=100;

    private AudioSource AS;
    private ParticleSystem PS;
    private Animator Ani;
    private CapsuleCollider CC;
    private NavMeshAgent NMA;
    private SphereCollider SC;
    private bool IsSinking=false;
    private float DeathTime=2f;
    private float timer=0;
    private EnemyDrops enemyDrops;

    // Start is called before the first frame update
    void Start()
    {
        Blood=blood;
        AS=GetComponent<AudioSource>();
        AS.clip = HurtAS;
        PS =GetComponentInChildren<ParticleSystem>();
        Ani=GetComponent<Animator>();
        CC=GetComponent<CapsuleCollider>();
        NMA=GetComponent<NavMeshAgent>();
        SC=GetComponent<SphereCollider>();
        enemyDrops = GetComponent<EnemyDrops>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSinking)
        {
            transform.Translate(-transform.up*Time.deltaTime);
            timer += Time.deltaTime;
            if (EnemyDeath && timer >= DeathTime)
            {
                Pool.Release(gameObject);
                timer = 0;
                EnemyDeath = false;
            }
        }
    }

    public void Damage(float damage,Vector3 Vec1)
    {
        AS.Play();
        PS.transform.position=Vec1;
        PS.Play();
        Blood -= damage;
        if (IsDeath())
        {
            Death();
            //SC.isTrigger=false;
        }
    }

    private bool IsDeath()
    {
        if(Blood<=0)return true;
        else return false;
    }

    private void Death()
    {
        EnemyDeath=true;
        //玩家得分
        PlayerScore.Score+=10;

        NMA.enabled = false;
        //死亡动画
        Ani.SetTrigger("Death");
        CC.enabled = false;
        SC.enabled=false;
        GetComponent<Rigidbody>().isKinematic=true;
        //死亡音效
        AS.clip=DeathAS;
        AS.Play();
        //掉落物品
        enemyDrops.DropItems();
    }

    public void StartSinking()
    {
        IsSinking = true;    
    }

    public float NowBlood()
    {
        return Blood;
    }

    public void ResetEnemy()
    {
        Blood = blood;
        EnemyDeath = false;
        IsSinking = false;
        timer = 0;

        // 重置组件状态
        if (CC != null)
            CC.enabled = true;
        if (SC != null)
            SC.enabled = true;
        if (NMA != null)
            NMA.enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;

        // 重置动画器
        if (Ani != null)
        {
            Ani.Rebind();
            Ani.Update(0f);
        }
        // 重置音效
        if (AS != null)
        {
            AS.Stop();
            if (HurtAS != null)
            {
                AS.clip = HurtAS;
            }
        }
    }
}
