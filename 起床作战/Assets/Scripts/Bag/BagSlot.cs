using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagSlot : MonoBehaviour
{
    public int SelfIndex;
    private Image SelfImage;
    private TextMeshProUGUI SelfNum;
    private BagWindow parentWindow;
    private Button Desc;//ÃèÊö´°¿Ú
    private GameObject InfoPanel;
    public ItemInfo SelfItem { get;private set; }


    public void Initial()
    {
        InfoPanel = this.transform.parent.parent.Find("Info").gameObject;
        Desc = GetComponentInChildren<Button>();
        Desc.onClick.AddListener(OpenDescWindow);
        SelfImage = transform.Find("Item/tp").GetComponent<Image>();
        SelfNum = transform.Find("Item/num").GetComponent<TextMeshProUGUI>();
        parentWindow = WindowManage.Instance.GetWindow<BagWindow>();
        Clear();
    }

    public void Clear()
    {
        SelfImage.color=Color.clear;
        SelfNum.text = "";
        SelfItem = null;
    }

    public bool HasItem()
    {
        if (SelfItem != null) return true;
        else return false;
    }

    public bool ItemCanAdd()
    {
        if (SelfItem.itemsort == ItemSortEnum.Ò©Îï)
        {
            return true;
        }
        else return false;
    }

    public void SetItem(ItemInfo itemInfo)
    {
        if(itemInfo==null)return;
        SelfItem=itemInfo;
        SelfNum.gameObject.SetActive(true);
        Sprite sp=Resources.Load<Sprite>("UI/"+itemInfo.itemname);
        SelfImage.sprite=sp;
        SelfImage.color = Color.white;
        if (itemInfo.itemsortindex == (int)ItemSortEnum.Ò©Îï)
        {
            SelfNum.text=itemInfo.itemcount.ToString();
        }
        else
        {
            SelfNum.text = "";
        }
    }

    public void AddItem(int count)
    {
        SelfItem.itemcount+=count;
        SelfNum.text=SelfItem.itemcount.ToString();
    }

    public void OpenDescWindow()
    {
        List<ItemInfo> itemList = InventoryManager.Instance.GetItemList();
        if (itemList[SelfIndex] != null)
        {
            InfoPanel.SetActive(true);
            InfoPanel.transform.Find("desc").GetComponent<TextMeshProUGUI>().text = itemList[SelfIndex].itemdesc;
            InfoPanel.transform.Find("title").GetComponent<TextMeshProUGUI>().text = itemList[SelfIndex].itemname;
        }
       
    }
}
