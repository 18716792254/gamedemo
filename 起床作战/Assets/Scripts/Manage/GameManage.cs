using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
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
        //SaveManage.Instance.Initial();
        //SaveManage.Instance.Initial();
        //if (SaveManage.Instance.HasSaveData())
        //{
        //    SaveManage.Instance.LoadGame();
        //}
    }

    void Update()
    {
        // 示例：快捷键保存/加载
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    SaveManage.Instance.SaveGame();
        //}

        //if (Input.GetKeyDown(KeyCode.L))
        //{
        //    SaveManage.Instance.LoadGame();
        //}
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
    }

    public void OpenSaveSetting()
    {
        WindowManage.Instance.OpenWindow<SaveMenuWindow>();
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
