using CompleteProject;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    public int EnemyMount = 3;
    public float BetweenTime = 3f;

    public GameObject Enemy;
    public ObjectPool<GameObject> Pool;
    private float timer=0;
    private int Mount=0;
    // Start is called before the first frame update
    void Start()
    {
        Pool=new ObjectPool<GameObject>(create,get,release,destroy,true,10,1000);
    }
    private void Update()
    {
        if(Mount < EnemyMount)
        CreateEnemy();
    }
    void CreateEnemy()
    {
        timer += Time.deltaTime;
        if (timer >= BetweenTime)
        {
            Mount++;
            timer = 0;
            Pool.Get();
        }
    }

    private GameObject create()
    {
        var enemy=Instantiate(Enemy);
        enemy.GetComponent<EnemyHealthy>().Pool=Pool;
        enemy.SetActive(false);
        return enemy;
    }

    private void get(GameObject enemy)
    {
        enemy.SetActive(true);
        enemy.GetComponent<EnemyHealthy>().ResetEnemy();
        enemy.transform.position = GetRandomPositionInArea();
        float RandomYRotation = UnityEngine.Random.Range(0f, 360f);
        enemy.transform.rotation = Quaternion.Euler(0f, RandomYRotation, 0f);
    }

    private void release(GameObject enemy)
    {
        enemy.SetActive(false);
    }

    private void destroy(GameObject enemy)
    {
        Destroy(enemy);
    }

    Vector3 GetRandomPositionInArea()
    {
        // 获取当前物体（生成器）在场景中的位置作为区域中心
        Vector3 Center = transform.position;
        Vector3 AreaSize = new Vector3(30f, 0f, 30f);

        // 生成一个随机点
        float RandomX = UnityEngine.Random.Range(-AreaSize.x / 2, AreaSize.x / 2);
        float RandomY = UnityEngine.Random.Range(-AreaSize.y / 2, AreaSize.y / 2);
        float RandomZ = UnityEngine.Random.Range(-AreaSize.z / 2, AreaSize.z / 2);
        Vector3 RandomPoint = Center + new Vector3(RandomX, RandomY, RandomZ);

        //检测生成位置
        NavMeshHit hit;
        if (NavMesh.SamplePosition(RandomPoint, out hit, 5f, NavMesh.AllAreas))
        {
            // RandomPoint就是最终位置 = 区域中心 + 随机偏移量
            return hit.position; 
        }
        return Center;
    }
}
