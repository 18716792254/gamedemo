using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent Nav;
    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindWithTag("Player");
        Nav=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float Blood=GetComponent<EnemyHealthy>().NowBlood();
        if (Blood > 0) 
        {
            Nav.SetDestination(player.transform.position);
        }
        
    }
}
