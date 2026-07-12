using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;
/*
 bug：将物体放到道具栏，保存后若背包内没物体，则会报错，若有物体，或再次保存后就不会报错
 */

[Serializable]
public class PlayerData
{
    //场上怪物位置信息
    public float x;
    public float y;
    public float z;
    // 角色基本信息
    // 位置信息
    public float posX;
    public float posY;
    public float posZ;
    //得分
    public int score;

    // 背包数据（包括道具栏数据）
    public List<ItemInfo> bagItems = new List<ItemInfo>();
}

public class SaveManage : SingleTon<SaveManage>
{
    private string thisFilePath = "SaveData.json";
    private string saveFilePath;

    public void Initial()
    {
        // 存档保存在持久化路径
        saveFilePath = Application.persistentDataPath + "/" + thisFilePath;
        thisFilePath = "SaveData.json";
    }

    public void GetFilePath(string filePath)
    {
        thisFilePath = filePath;
    }

    // 保存游戏数据
    public void SaveGame()
    {
        try
        {
            PlayerData playerData = new PlayerData();
            //保存角色基本信息
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerData.posX = player.transform.position.x;
                playerData.posY = player.transform.position.y;
                playerData.posZ = player.transform.position.z;
            }
            playerData.score = PlayerScore.Score;
            //保存背包数据
            playerData.bagItems = InventoryManager.Instance.GetSaveData();
            //序列化为JSON
            string jsonData = JsonConvert.SerializeObject(playerData, Formatting.Indented);
            //写入文件
            File.WriteAllText(saveFilePath, jsonData);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"保存失败: {e.Message}\n堆栈跟踪:\n{e.StackTrace}");
            Debug.LogError($"保存失败: {e.Message}");
        }
    }

    // 读取游戏数据
    public void LoadGame()
    {
        try
        {
            if (!File.Exists(saveFilePath))
            {
                Debug.LogWarning("没有找到存档文件！");
                return;
            }

            //读取文件
            string jsonData = File.ReadAllText(saveFilePath);

            //反序列化
            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject[] tmpmonsters = GameObject.FindGameObjectsWithTag("monster");
            foreach (var item in tmpmonsters)
            {
                item.SetActive(false);
            }
            //加载角色基本信息
            GameManage.vec = new Vector3(playerData.posX, playerData.posY, playerData.posZ);
            if(player != null)player.transform.position = GameManage.vec;
            PlayerScore.Score = playerData.score;
            //加载背包数据
            InventoryManager.Instance.LoadFromSaveData(playerData.bagItems);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"加载失败: {e.Message}");
        }
    }

    // 检查是否存在存档
    public bool HasSaveData()
    {
        try
        {
            string jsonData = File.ReadAllText(saveFilePath);
            //是否完全为空字符串
            if (string.IsNullOrWhiteSpace(jsonData))
            {
                return false;
            }
            // 尝试反序列化，看是否能成功读出来
            PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(jsonData);

            //数据读出来了，但里面是不是空的
            if (playerData == null)
            {
                return false;
            }

            return true;
        }
        catch (System.Exception)
        {
            // 读取或反序列化过程出错了，说明文件损坏或无效
            return false;
        }
    }

    // 删除存档
    public void DeleteSave()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("存档已删除");
        }
    }
}