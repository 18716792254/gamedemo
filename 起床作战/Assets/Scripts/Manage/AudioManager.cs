using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioSource bgAudio;
    private Slider audioSlider;
    public static AudioManager instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // 5. 获取AudioSource组件
        bgAudio =GameObject.FindGameObjectWithTag("background").transform.gameObject.GetComponent<AudioSource>();
        audioSlider=GetComponent<Slider>();
    }

    private void Update()
    {
        CloseSetting();
        VolumeControl();
    }

    //滑动条控制音量
    public void VolumeControl()
    {
        bgAudio.volume = audioSlider.value * 0.1f;
    }

    public void CloseSetting()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            GameObject menu = GameObject.FindGameObjectWithTag("setting").transform.gameObject;
            menu.SetActive(false);
        }
    }
}