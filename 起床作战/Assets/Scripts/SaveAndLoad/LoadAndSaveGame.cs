using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAneSaveGame : MonoBehaviour
{
    public string fileName;
    public string filePath;
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
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

    }

    public void Save()
    {
        SaveManage.Instance.GetFilePath(filePath);
        SaveManage.Instance.Initial();
        SaveManage.Instance.SaveGame();
    }

}
