using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickCloseLoadButton : MonoBehaviour
{
    public GameObject bg;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        bg = GameObject.FindWithTag("background");
        if (bg != null)
        {
            StartGame bgLoad = bg.GetComponent<StartGame>();
            button.onClick.AddListener(bgLoad.CloseLoadMenu);
        }
    }
}
