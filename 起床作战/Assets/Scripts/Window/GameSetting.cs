using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSetting : BaseWindow
{
    private Slider bgSlider;
    public override void Initial()
    {
        bgSlider = GameObject.Find("Panel/main/Slider").GetComponent<Slider>();
        if (BackgroundMusicManager.instance != null)
        {
            bgSlider.value = BackgroundMusicManager.instance.Volume * 10;
            bgSlider.onValueChanged.AddListener(AudioChange);
        }

    }

    public void AudioChange(float value)
    {
        BackgroundMusicManager.instance.Volume = value * 0.1f;
    }
}
