using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    public GameObject player;
    public static Vector3 vec;
    public GameObject Canvas;
    public GameObject setting;
    public LoadMenuWindow loadWindow;
    public BagWindow bagWindow;
    public SaveMenuWindow saveWindow;
    private GameObject tmpObj;
    private void Start()
    {
        WindowManage.Instance.Initial(Canvas.transform);
        InventoryManager.Instance.Initial();
        DataManage.Instance.Initial();
        player = GameObject.FindWithTag("Player");
        player.transform.position = vec;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameObject.Find("Canvas/GameSetting(Clone)") != null)
            {
                tmpObj = GameObject.Find("Canvas/GameSetting(Clone)");
                if (tmpObj.activeSelf)
                {
                    tmpObj.SetActive(false);
                    return;
                }
            }
            if (setting.activeSelf)
            {
                setting.SetActive(false);
            }
            else
            {
                setting.SetActive(true);
            }
        }
    }

    public void OpenLoadSetting()
    {
        WindowManage.Instance.OpenWindow<LoadMenuWindow>();
        GameObject loadWindow = Canvas.transform.Find("LoadMenuWindow(Clone)").gameObject;
        LoadAneSaveGame[] load = loadWindow.GetComponentsInChildren<LoadAneSaveGame>();
        foreach (var item in load)
        {
            item.ReflashUI();
        }
    }

    public void OpenSaveSetting()
    {
        WindowManage.Instance.OpenWindow<SaveMenuWindow>();
        GameObject saveWindow = Canvas.transform.Find("SaveMenuWindow(Clone)").gameObject;
        LoadAneSaveGame[] save = saveWindow.GetComponentsInChildren<LoadAneSaveGame>();
        foreach (var item in save)
        {
            item.ReflashUI();
        }
    }

    public void OpenSetting()
    {
        WindowManage.Instance.OpenWindow<GameSetting>();
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
