using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadMenuWindow : BaseWindow
{
    private Button CloseBag;
    public override void Initial()
    {
        CloseBag = GameObject.Find("Close").GetComponent<Button>();
        CloseBag.onClick.AddListener(CloseSaveWindow);
    }

    private void CloseSaveWindow()
    {
        CloseWindow();
    }

}
