using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemManage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //public GameObject parentCanvas;
    public UseItem useItem;
    public Transform OriginalParent;
    private GameObject tmpObj;
    private Sprite itemSprite;
    private Image tmpImage;
    private int index;
    private int t;


    void Awake()
    {
        t = this.transform.parent.transform.GetSiblingIndex();
        index = t;
    }

    //物体移动
    public void OnBeginDrag(PointerEventData eventData)
    {
        tmpObj = this.transform.parent.parent.parent.parent.Find("TmpImage").gameObject;
        index = t;
        List<ItemInfo> itemList = InventoryManager.Instance.GetItemList();
        if (this.transform.parent.tag == "propslot")
        {
            index = InventoryManager.Instance.GetBagCount() + index;
        }
        // 获取图片资源
        if (index < 0 || index >= itemList.Count)
        {
            return;
        }
        Debug.Log(itemList[index]==null);
        itemSprite = Resources.Load<Sprite>("UI/" + itemList[index].itemname);
        tmpObj.SetActive(true);
        tmpObj.transform.SetAsLastSibling();
        // 添加Image组件并设置图片和数量
        tmpImage = tmpObj.GetComponent<Image>();
        tmpImage.sprite = itemSprite;
        tmpImage.GetComponentInChildren<TextMeshProUGUI>().text = itemList[index].itemcount.ToString();
        // 设置位置（可选）
        tmpObj.transform.position = eventData.position;
        tmpObj.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        tmpObj.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            if (eventData.pointerCurrentRaycast.gameObject.name != "tp")
            {
                tmpObj.SetActive(false);
                return;
            }
            //背包物体之间拖动
            if (this.transform.parent.tag=="bagslot" && eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.tag == "bagslot")
            {
                int tmpindex= eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.transform.GetSiblingIndex();
                List<ItemInfo> itemList = InventoryManager.Instance.GetItemList();
                if (index < 0 || index >= itemList.Count || tmpindex < 0 || tmpindex >= itemList.Count)
                {
                    return;
                }
                if (itemList[tmpindex] != null && itemList[index].itemid == itemList[tmpindex].itemid)
                {
                    itemList[tmpindex].itemcount += itemList[index].itemcount;
                    tmpObj.SetActive(false);
                    itemList[index] = null;
                    tmpObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    return;
                }
                ItemInfo temp = itemList[index];
                itemList[index] = itemList[tmpindex];
                itemList[tmpindex] = temp;
                tmpObj.SetActive(false);
                tmpObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            //背包物体拖到物品槽
            else if(this.transform.parent.tag == "bagslot" && eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.tag == "propslot")
            {
                List<ItemInfo> itemList = InventoryManager.Instance.GetItemList();
                //防止装备进入道具栏，但目前无装备物体，没有用
                if (!InventoryManager.Instance.IsStackable(itemList[index]))
                {
                    return;
                }
                int propIndex = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetSiblingIndex();
                int tmpindex = InventoryManager.Instance.GetBagCount() + propIndex;
                if (index < 0 || index >= itemList.Count || tmpindex < 0 || tmpindex >= itemList.Count)
                {
                    return;
                }
                if (itemList[tmpindex]!=null && itemList[index].itemid == itemList[tmpindex].itemid)
                {
                    itemList[tmpindex].itemcount += itemList[index].itemcount;
                    tmpObj.SetActive(false);
                    itemList[index] = null;
                    eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<UseItem>().RefreshUI();
                    tmpObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    return;
                }
                ItemInfo temp = itemList[index];
                itemList[index] = itemList[tmpindex];
                itemList[tmpindex] = temp;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<UseItem>().RefreshUI();
                InventoryManager.Instance.RemoveItem(temp);
                tmpObj.SetActive(false);
                tmpObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            //道具栏位置交换
            else if(this.transform.parent.tag == "propslot" && eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.tag == "propslot")
            {
                List<ItemInfo> itemList = InventoryManager.Instance.GetItemList();
                int propIndex = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.GetSiblingIndex();
                int tmpindex = InventoryManager.Instance.GetBagCount() + propIndex;
                if (index < 0 || index >= itemList.Count || tmpindex < 0 || tmpindex >= itemList.Count)
                {
                    return;
                }
                if (itemList[tmpindex] != null && itemList[index].itemid == itemList[tmpindex].itemid)
                {
                    itemList[tmpindex].itemcount += itemList[index].itemcount;
                    tmpObj.SetActive(false);
                    itemList[index] = null;
                    this.GetComponent<UseItem>().RefreshUI();
                    tmpObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    return;
                }
                ItemInfo temp = itemList[index];
                itemList[index] = itemList[tmpindex];
                itemList[tmpindex] = temp;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<UseItem>().RefreshUI();
                this.GetComponent<UseItem>().RefreshUI();
                tmpObj.SetActive(false);
                tmpObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
            else if(this.transform.parent.tag == "propslot" && eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.tag == "bagslot")
            {
                List<ItemInfo> itemList = InventoryManager.Instance.GetItemList();
                int tmpindex = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.transform.GetSiblingIndex();
                if (index < 0 || index >= itemList.Count || tmpindex < 0 || tmpindex >= itemList.Count)
                {
                    return;
                }
                if (itemList[tmpindex] != null && itemList[index].itemid == itemList[tmpindex].itemid)
                {
                    itemList[tmpindex].itemcount += itemList[index].itemcount;
                    tmpObj.SetActive(false);
                    itemList[index] = null;
                    GetComponent<UseItem>().RefreshUI();
                    tmpObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
                    return;
                }
                ItemInfo temp = itemList[index];
                itemList[index] = itemList[tmpindex];
                itemList[tmpindex] = temp;
                GetComponent<UseItem>().RefreshUI();
                this.GetComponent<UseItem>().RefreshUI();
                tmpObj.SetActive(false);
                tmpObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
        

        //    transform.SetParent(OriginalParent);
        //    transform.position = OriginalParent.transform.position;
        //    if (eventData.pointerCurrentRaycast.gameObject == null || eventData.pointerCurrentRaycast.gameObject.name != "tp")
        //    {
        //        transform.SetParent(OriginalParent);
        //        transform.position = OriginalParent.transform.position;
        //        GetComponent<CanvasGroup>().blocksRaycasts = true;
        //    }
        //    else if (eventData.pointerCurrentRaycast.gameObject.name == "tp")
        //    {

            //        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform.parent.parent);//将原物体转移到鼠标下的格子的slot中
            //        transform.position = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent.position;//让被拖拽物体移到鼠标所指的格子里
            //        eventData.pointerCurrentRaycast.gameObject.transform.parent.position = OriginalParent.position;//把格子里有的物体移到原物体位置
            //        eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(OriginalParent);//给被更改物体移到原物体的slot里
            //        if (transform.parent.name=="PropSlot1" || transform.parent.name == "PropSlot2" || transform.parent.name == "PropSlot3")
            //        {
            //            GetComponent<UseItem>().enabled=true;
            //        }
            //        else
            //        {
            //            GetComponent<UseItem>().enabled = false;
            //        }
            //            GetComponent<CanvasGroup>().blocksRaycasts = true;
            //    }
            //    else
            //    {
            //        transform.SetParent(eventData.pointerCurrentRaycast.gameObject.transform);
            //        transform.position = eventData.pointerCurrentRaycast.gameObject.transform.position;
            //        GetComponent<CanvasGroup>().blocksRaycasts = true;
            //    }
    }

}