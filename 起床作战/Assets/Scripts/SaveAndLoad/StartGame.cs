using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadMenu;

    public void LoadNewGame()
    {
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
        menu.SetActive(true);
    }

    public void OpenLoadMenu()
    {
        loadMenu.SetActive(true);
    }

    public void CloseLoadMenu()
    {
        loadMenu.SetActive(false);
    }
}
