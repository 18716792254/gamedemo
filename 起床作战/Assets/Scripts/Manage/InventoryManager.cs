using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryManager : SingleTon<InventoryManager>
{

    public List<ItemInfo> itemList = new List<ItemInfo>();
    public Dictionary<int, ItemInfo> ItemInfos = new Dictionary<int, ItemInfo>();
    public int maxSlotCount = 15; // 最大背包格子数
    public int propSlotCount = 3;//道具格;
    public int Count = 0;
    private bool isInitialized = false;
    private ItemInfo dataManage;

    public InventoryManager()
    {
        Initial(); // 只要有人 new 或者获取 Instance，就会自动执行
    }
    public void Initial()
    {
        Count = 0;
        if (isInitialized) return;
        for (int i = 0; i < maxSlotCount + propSlotCount; i++)
        {
            itemList.Add(null);
        }
        isInitialized = true;
    }

    public void RemoveItem(ItemInfo item)
    {
        ItemInfos.Remove(item.itemid);
    }
    public int GetBagCount()
    {
        return maxSlotCount;
    }

    //找到物体在列表中的位置
    public int GetItemIndex(int id)
    {
        for (int i = 0; i <= maxSlotCount; i++)
        {
            if (id == itemList[i].itemid)
            {
                return i;
            }
        }
        return 0;
    }

    //将新物体的基本信息写入字典，而不可堆叠物品进入判断会被返回false，间接说明了他是不是不可堆叠物品
    public bool DisInBag(ItemInfo newItem)
    {
        if (!ItemInfos.ContainsKey(newItem.itemid))
        {
            ItemInfos.Add(newItem.itemid, newItem);
            return true;
        }
        return false;
    }
    public bool PickUpItem(ItemInfo newItem)
    {
        //检查是否有空格
        if (Count > maxSlotCount)
        {
            Debug.LogWarning("背包已满！");
            return false;
        }
        //IsStackable(newItem)可以删除，但是还是做个保障
        if (DisInBag(newItem) || !IsStackable(newItem))
        {
            Debug.Log("不可堆叠物或没有的堆叠物");
            for (int i = 0; i < maxSlotCount; i++)
            {
                if (itemList[i] == null)
                {
                    Count++;
                    itemList[i] = newItem;
                    return true;
                }
            }
        }
        else if (!DisInBag(newItem) && IsStackable(newItem))
        {
            Debug.Log("堆叠物");
            for (int i = 0; i < maxSlotCount; i++)
            {
                if (itemList[i] != null)
                {
                    if (itemList[i].itemid == newItem.itemid)
                    {
                        itemList[i].itemcount += 1;
                        return true;
                    }
                }
            }
        }
        for (int i = 0; i < maxSlotCount; i++)
        {
            if (itemList[i] == null)
            {
                Count++;
                itemList[i] = newItem;
                return true;
            }
        }
        return true;
    }



    public bool IsStackable(ItemInfo item)
    {
        return item.itemsort == ItemSortEnum.药物;
    }

    /// 获取背包物品列表（供UI显示）
    public List<ItemInfo> GetItemList()
    {
        return itemList;
    }

    public void Clear()
    {
        itemList.Clear();
    }

    public List<ItemInfo> GetSaveData()
    {
        List<ItemInfo> saveData = new List<ItemInfo>();
        for (int i = 0; i < maxSlotCount + propSlotCount; i++)
        {
            saveData.Add(null);
        }
        for (int i = 0; i < itemList.Count; i++)
        {
            saveData[i] = itemList[i];
        }

        return saveData;
    }

    internal void LoadFromSaveData(List<ItemInfo> saveData)
    {
        // 清空当前背包
        Clear();

        // 重新初始化
        int totalSlots = maxSlotCount + propSlotCount;
        for (int i = 0; i < totalSlots; i++)
        {
            itemList.Add(null);
        }
        ItemInfos.Clear();
        Count = 0;

        // 加载物品
        for (int i = 0; i < saveData.Count && i < totalSlots; i++)
        {
            if (saveData[i] != null && saveData[i].itemsortindex == (int)ItemSortEnum.药物)
            {
                // 创建副本，避免引用问题
                ItemInfo newItem = saveData[i].CloneProp(); // 使用你已有的克隆方法
                itemList[i] = newItem;
                ItemInfos.Add(newItem.itemid, newItem);
                Count++;
            }
        }
    }
}