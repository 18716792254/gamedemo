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
