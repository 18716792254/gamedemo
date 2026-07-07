using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackgroundMusicManager : MonoBehaviour
{
    //实现单例
    public static BackgroundMusicManager instance { get; private set; }
    private AudioSource audioSource;
    public AudioClip currentClip => audioSource.clip;
    public bool isPlaying => audioSource.isPlaying;
    public float Volume
    {
        get => audioSource.volume;
        set => audioSource.volume = Mathf.Clamp01(value);
    }
    void Awake()
    {
        //如果实例已存在，则销毁新生成的这个对象
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 保存实例，并让此对象在场景加载时不被销毁
        instance = this;
        DontDestroyOnLoad(gameObject);

        //获取AudioSource组件
        audioSource = GetComponent<AudioSource>();
    }

}