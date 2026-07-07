using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManage : SingleTon<WindowManage>
{
    private Dictionary<string,BaseWindow> AllWindows;
    private Transform TranCanvas;

    public void Initial(Transform Canvas)
    {
        AllWindows = new Dictionary<string, BaseWindow>();
        TranCanvas = Canvas;
    }

    public void OpenWindow<T>() where T : BaseWindow, new()
    {
        string WindowName=typeof(T).Name;
        if (AllWindows.ContainsKey(WindowName))
        {
            AllWindows[WindowName].OpenWindow();
        }
        else
        {
            T Window=new T();
            Window.CreateWindow(WindowName,TranCanvas);
            Window.Initial();//抽象方法，在子类中具体实现
            AllWindows.Add(WindowName,Window);
        }
    }

    public void CloseWindow<T>() where T : BaseWindow, new()
    {
        string WindowName = typeof(T).Name;
        if (AllWindows.ContainsKey(WindowName))
        {
            AllWindows[WindowName].CloseWindow();
        }
    }

    public T GetWindow<T>() where T : BaseWindow
    {
        string WindowName = typeof(T).Name;
        if (AllWindows.ContainsKey(WindowName))
        {
            return AllWindows[WindowName] as T;
        }
        return null;
    }



}
