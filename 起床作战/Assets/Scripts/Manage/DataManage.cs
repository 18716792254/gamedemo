using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public enum ItemSortEnum
{
    Ò©Îï
}

[System.Serializable]
public class ItemInfo
{
    public string itemname;
    public int itemid;
    public int itemsortindex;
    public string itemdesc;
    public string itemspname;
    public int itemcount;
    public int hp;
    public int ack;

    public ItemInfo CloneProp()
    {
        return new ItemInfo
        {
            itemname = this.itemname,
            itemcount = this.itemcount,
            itemid = this.itemid,
            itemsortindex = this.itemsortindex,
            itemdesc = this.itemdesc,
            itemspname = this.itemspname,
            hp = this.hp,
            ack = this.ack
        };
    }
    public ItemInfo()
    {

    }
    public ItemInfo(ItemInfo item)
    {
        itemcount=1;
        this.itemname=item.itemname;
        this.itemid=item.itemid;
        this.itemsortindex=item.itemsortindex;
        this.itemdesc=item.itemdesc;
        this.itemspname=item.itemspname;
        if ((ItemSortEnum)this.itemsortindex == ItemSortEnum.Ò©Îï)
        {
            this.hp=item.hp;
            this.ack=item.ack;
        }
    }

    public ItemSortEnum itemsort
    {
        get
        {
            return (ItemSortEnum)itemsortindex;
        }
    }
}

public class DataManage : SingleTon<DataManage>
{
    private Dictionary<int,ItemInfo> ItemInfos=new Dictionary<int, ItemInfo>();
    List<ItemInfo> ItemInfoList;

    public void Initial()
    {
        string filepath=Application.streamingAssetsPath + "/Bag.json";
        string jsondata=File.ReadAllText(filepath);
        ItemInfoList=JsonConvert.DeserializeObject<List<ItemInfo>>(jsondata);
        for (int i = 0; i < ItemInfoList.Count; i++)
        {
            ItemInfos.Add(ItemInfoList[i].itemid, ItemInfoList[i]);
        }
    }

    public ItemInfo GetItem(int id)
    {
        if (ItemInfos.ContainsKey(id))
        {
            if(ItemInfos[id].itemsortindex == (int)ItemSortEnum.Ò©Îï)
            {
                return ItemInfos[id].CloneProp();
            }
        }
        return null;
    }

}
