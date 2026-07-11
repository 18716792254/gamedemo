using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerControl : MonoBehaviour
{
    private List<UseItem> useItems = new List<UseItem>();
    public BagWindow bagWindow;

    // Start is called before the first frame update
    void Start()
    {
        GameObject propObj = GameObject.Find("Prop");
        if (propObj != null)
        {
            Transform slotsTransform = propObj.transform.Find("Slots");
            if (slotsTransform != null)
            {
                foreach (Transform child in slotsTransform)
                {
                    // PropSlot 下的 Item 子物体
                    Transform itemTransform = child.Find("Item");
                    if (itemTransform != null)
                    {
                        UseItem useItem = itemTransform.GetComponent<UseItem>();
                        if (useItem != null)
                        {
                            useItems.Add(useItem);
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        bagWindow = WindowManage.Instance.GetWindow<BagWindow>();
        List<ItemInfo> itemList = InventoryManager.Instance.GetItemList();
        if (bagWindow != null)
        {
            bagWindow.RefreshUI();
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            Time.timeScale = 0f;
            WindowManage.Instance.OpenWindow<BagWindow>();
        }
    }

    public void PickUp(int itemid, GameObject pickupObject)
    {
        //从数据管理器获取物品信息
        ItemInfo itemInfo = DataManage.Instance.GetItem(itemid);
        if (itemInfo == null)
        {
            return;
        }
        //存入InventoryManager
        bool success = InventoryManager.Instance.PickUpItem(itemInfo);

        if (success)
        {
            Destroy(pickupObject);
        }
        else
        {
            Debug.Log("背包已满，无法拾取！");
        }
    }

}
