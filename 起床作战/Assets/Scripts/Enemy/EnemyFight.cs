using CompleteProject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFight : MonoBehaviour
{
    public int Attact;
    private EnemyHealthy health;
    private bool PlayerIsLife=true;
    private bool PlayerIsNext = false;
    private Animator Ani;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<EnemyHealthy>();
        Ani=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HurtPlayer();
    }

    private void HurtPlayer()
    {
        PlayerIsLife = !(GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().IsDeath);
        if (PlayerIsLife)
        {
            if (PlayerIsNext && !health.IsDeath())
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().GetHurt(Attact);
            }
        }
        else
        {
            Ani.SetTrigger("PlayerDeath");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerIsNext = true;  
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerIsNext = false;
        }

    }
}
