using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    private PlayerHealth isDeath;
    private NavMeshAgent Nav;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindWithTag("Player");
        isDeath=player.GetComponent<PlayerHealth>();
        Nav =GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float Blood=GetComponent<EnemyHealthy>().NowBlood();
        if (Blood > 0 && !isDeath.IsDeath) 
        {
            Nav.SetDestination(player.transform.position);
        }
        
    }
}
