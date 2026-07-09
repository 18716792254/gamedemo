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
        bg = GameObject.FindWithTag("background");
        text = GetComponentInChildren<TextMeshProUGUI>();
        button.onClick.AddListener(Load);
        if (bg!=null)
        {
            StartGame bgLoad = bg.GetComponent<StartGame>();
            button.onClick.AddListener(bgLoad.LoadGame);
        }
        SaveManage.Instance.GetFilePath(filePath);
        SaveManage.Instance.Initial();
        ReflashUI();
    }

    public void ReflashUI()
    {
        SaveManage.Instance.GetFilePath(filePath);
        SaveManage.Instance.Initial();
        Debug.Log("ReflashUI()" + fileName);
        Debug.Log(SaveManage.Instance.HasSaveData());
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
    }

    public void Save()
    {
        SaveManage.Instance.GetFilePath(filePath);
        SaveManage.Instance.Initial();
        SaveManage.Instance.SaveGame();
        ReflashUI();
    }

}
