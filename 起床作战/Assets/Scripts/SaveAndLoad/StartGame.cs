using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject canvas;
    public GameObject menu;
    public GameObject loadMenu;
    private void Start()
    {
        canvas = GameObject.FindWithTag("canvas");
        menu = canvas.transform.Find("GameSetting").gameObject;
        loadMenu = canvas.transform.Find("LoadSetting").gameObject;
    }
    public void LoadNewGame()
    {
        PlayerScore.Score = 0;
        GameManage.vec = new Vector3(0,0,0);
        InventoryManager.Instance.Clear();
        SceneManager.LoadScene(1);
    }
    public void LoadGame()
    {
        if (SaveManage.Instance.HasSaveData())
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            GameObject warningObj = loadMenu.transform.Find("Warning").gameObject;
            warningObj.SetActive(true);
            StartCoroutine(DelayedDeactivate(warningObj, 1.0f));
            return;
        }
    }

    IEnumerator DelayedDeactivate(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj != null)
        {
            obj.SetActive(false);
        }
    }

    public void OpenSetting()
    {
        canvas = GameObject.FindWithTag("canvas");
        menu = canvas.transform.Find("GameSetting").gameObject;
        menu.SetActive(true);
    }

    public void OpenLoadMenu()
    {
        canvas = GameObject.FindWithTag("canvas");
        loadMenu = canvas.transform.Find("LoadSetting").gameObject;
        loadMenu.SetActive(true);
    }

    public void CloseLoadMenu()
    {
        canvas = GameObject.FindWithTag("canvas");
        loadMenu = canvas.transform.Find("LoadSetting").gameObject;
        loadMenu.SetActive(false);
    }
}
