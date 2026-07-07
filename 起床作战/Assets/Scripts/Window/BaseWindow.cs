using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWindow
{
    protected Transform SelfTransform
    {
        get;
        private set;
    }

    public void CreateWindow(string WindowName,Transform Canvas)
    {
        GameObject originWindow=Resources.Load<GameObject>("Prefabs/" + WindowName);
        GameObject CloneWindow=GameObject.Instantiate(originWindow);
        SelfTransform=CloneWindow.transform;
        SelfTransform.SetParent(Canvas,false);
    }

    public abstract void Initial();

    public void OpenWindow()
    {
        SelfTransform.gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        SelfTransform.gameObject.SetActive(false);
    }

}
