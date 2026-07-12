using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadAneSaveGame : MonoBehaviour
{
    public GameObject bg;
    public string fileName;
    public string filePath;
    private TextMeshProUGUI text;
    private Button button;

    // Start is called before the first frame update
    void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        if (button.onClick.GetPersistentEventCount() == 0)
        {
            button.onClick.AddListener(Load);
            bg = GameObject.FindWithTag("background");
            if (bg != null)
            {
                StartGame bgLoad = bg.GetComponent<StartGame>();
                button.onClick.AddListener(bgLoad.LoadGame);
            }
            SaveManage.Instance.GetFilePath(filePath);
            SaveManage.Instance.Initial();
            ReflashUI();
        }
    }

    public void ReflashUI()
    {
        SaveManage.Instance.GetFilePath(filePath);
        SaveManage.Instance.Initial();
        if (SaveManage.Instance.HasSaveData())
        {
            text.text = fileName;
        }
        else
        {
            text.text = "´Ë´¦´æµµÎª¿Õ";
        }
    }

    public void Load()
    {
        SaveManage.Instance.GetFilePath(filePath);
        SaveManage.Instance.Initial();
        SaveManage.Instance.LoadGame();
        Time.timeScale = 1f;
    }

    public void Save()
    {
        SaveManage.Instance.GetFilePath(filePath);
        SaveManage.Instance.Initial();
        SaveManage.Instance.SaveGame();
        ReflashUI();
    }

}
