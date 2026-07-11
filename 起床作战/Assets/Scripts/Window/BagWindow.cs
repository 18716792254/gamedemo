using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagWindow : BaseWindow
{
    
    private List<BagSlot> AllSlots;//背包格子
    private Transform Info;
    private Button CloseBag;//关闭按钮
    private Button SelectButton;

    public override void Initial()
    {
        GameObject.Find("Info").SetActive(false);
        CloseBag = GameObject.Find("CloseBag").GetComponent<Button>();
        CloseBag.onClick.AddListener(CloseBagWindow);
        AllSlots = new List<BagSlot>();
        Transform SlotRoot = SelfTransform.Find("Slots");
        for (int i = 0; i < SlotRoot.childCount; i++)
        {
            GameObject TmpSlot = SlotRoot.GetChild(i).gameObject;
            BagSlot tmpslot = TmpSlot.AddComponent<BagSlot>();
            tmpslot.Initial();
            AllSlots.Add(tmpslot);
        }
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (var slot in AllSlots)
        {
            slot.Clear();
        }

        //从 InventoryManager 获取数据并显示
        List<ItemInfo> itemList = InventoryManager.Instance.GetItemList();
        for (int i = 0; i < itemList.Count && i < AllSlots.Count; i++)
        {
            AllSlots[i].SelfIndex = i;
            AllSlots[i].SetItem(itemList[i]);
        }
    }


    private void CloseBagWindow()
    {
        if (GameObject.Find("Info") != null)
            GameObject.Find("Info").SetActive(false);
        Time.timeScale = 1f;
        CloseWindow();
    }

}
