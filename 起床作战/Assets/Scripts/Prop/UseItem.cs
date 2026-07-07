using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseItem : MonoBehaviour
{
    public Image itemImage;
    private TextMeshProUGUI numText;
    private TextMeshProUGUI keyCodeText;

    public int slotIndex; // 道具栏索引，对应 InventoryManager 中的位置
    public int bagSlotCount;
    public Transform OriginalParent { get; private set; }
    public int Num;
    void Start()
    {
        itemImage = GetComponentInChildren<Image>();
        numText = transform.Find("num")?.GetComponent<TextMeshProUGUI>();
        ////获取道具栏索引（根据父级位置）
        Transform parentSlot = transform.parent;
        if (parentSlot != null)
        {
            slotIndex = parentSlot.GetSiblingIndex();
        }

        // 初始化时刷新显示
        RefreshUI();
    }

    public void RefreshUI()
    {
        // 实时获取当前的索引
        List<ItemInfo> itemList = InventoryManager.Instance.GetItemList();
        bagSlotCount = InventoryManager.Instance.GetBagCount();
        int currentSlotIndex = slotIndex + bagSlotCount;
        // 检查索引是否有效
        if (currentSlotIndex < itemList.Count && itemList[currentSlotIndex] != null)
        {
            ItemInfo item = itemList[currentSlotIndex];

            // 显示图标
            if (itemImage != null)
            {
                Sprite sp = Resources.Load<Sprite>("UI/" + item.itemname);
                if (sp != null)
                {
                    itemImage.sprite = sp;
                    itemImage.color = Color.white;
                }
            }

            // 显示数量
            if (numText != null)
            {
                numText.text = item.itemcount.ToString();
                numText.gameObject.SetActive(true);
            }
        }
        else
        {
            // 没有物品，清空显示
            ClearSlot();
        }
    }


    private void ClearSlot()
    {
        if (itemImage != null)
        {
            itemImage.sprite = null;
            itemImage.color = Color.clear;
        }
        if (numText != null)
        {
            numText.text = "";
            numText.gameObject.SetActive(false);
        }
    }

    public void ItemUse()
    {
        Transform parentSlot = transform.parent; // PropSlot GameObject
        List<ItemInfo> itemList = InventoryManager.Instance.GetItemList();
        int currentSlotIndex = slotIndex + bagSlotCount;
        // 检查索引是否有效
        if (currentSlotIndex >= itemList.Count || itemList[currentSlotIndex] == null)
        {
            Debug.Log($"道具栏 {slotIndex + 1} 为空，无法使用");
            return;
        }
        ItemInfo currentItem = itemList[currentSlotIndex];
        // 根据物品类型使用
        if (currentItem.itemsort == ItemSortEnum.药物)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerHealth health = player.GetComponent<PlayerHealth>();

                if (health != null && !health.IsDeath)
                {
                    // 恢复血量
                    health.Blood += currentItem.hp;
                    if (health.Blood > 100) health.Blood = 100;

                    // 减少数量
                    currentItem.itemcount--;


                    // 如果数量为0，从列表中移除
                    if (currentItem.itemcount <= 0)
                    {
                        itemList[currentSlotIndex] = null;
                        ClearSlot();
                    }
                    else
                    {
                        // 更新数量显示
                        if (numText != null)
                        {
                            numText.text = currentItem.itemcount.ToString();
                        }
                    }

                }
                else
                {
                    Debug.Log("玩家已死亡或无法恢复血量");
                }
            }
        }
        else
        {
            Debug.Log($"物品 {currentItem.itemname} 无法使用");
        }
    }
    void Update()
    {
        RefreshUI();
        // 检测快捷键
        if (Input.GetKeyDown(KeyCode.Alpha1) && this.transform.parent.name == "PropSlot1") 
        {
            ItemUse();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && this.transform.parent.name == "PropSlot2")
        {
            ItemUse();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && this.transform.parent.name == "PropSlot3")
        {
            ItemUse();
        }
    }
}