using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    public DropPrefab[] dropPrefabs;
    public float noDropWeight;


    public void DropItems()
    {
        // 计算总权重
        float totalWeight = noDropWeight;
        foreach (var drop in dropPrefabs)
        {
            totalWeight += drop.dropPercentage;
        }
        float randomValue = Random.Range(0f, totalWeight);
        float currentWeight = noDropWeight;
        if (randomValue <= noDropWeight)
        {
            return;  // 不掉落
        }
        // 根据权重选择
        foreach (var drop in dropPrefabs)
        {
            currentWeight += drop.dropPercentage;
            if (randomValue <= currentWeight)
            {
                Instantiate(drop.prefab, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}

[System.Serializable]
public class DropPrefab
{
    public GameObject prefab;
    [Range(0f,100f)] public float dropPercentage;

}