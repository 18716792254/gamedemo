using CompleteProject;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public AudioClip AC;
    //受伤间隔
    public float timer = 1f;
    public float time = 0;
    //玩家血量
    public float Blood=100f;
    //玩家是否死亡
    public bool IsDeath=false;
    //玩家血量UI
    public TextMeshProUGUI PlayerHealthUI;
    //受伤提示
    public Image Damage;
    public Color FlashColor = new Color(1f,0f,0f,0.1f);

    private Animator ani;
    private AudioSource AS;
    private PlayerMovement PM;
    private PlayerShooting PS;
    private bool GetDamage=false;
    private float CurrentBlood;

    // Start is called before the first frame update
    void Start()
    {
        ani=GetComponent<Animator>();
        AS=GetComponent<AudioSource>();
        PM=GetComponent<PlayerMovement>();
        PS=GetComponentInChildren<PlayerShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetDamage)
        {
            Damage.color = FlashColor;
        }
        else
        {
            Damage.color=Color.Lerp(Damage.color, Color.clear,5f*Time.deltaTime);
        }
        GetDamage=false;
        if (CurrentBlood != Blood)
        {
            //更新UI
            PlayerHealthUI.text = Blood.ToString();
        }
        CurrentBlood = Blood;
    }
    
    public void GetHurt(float Hurt)
    {
        if (IsDeath) return;
        GetDamage=true;
        time += Time.deltaTime;
        if (time > timer)
        {
            time = 0;
            Blood -= 10;
            AS.Play();
            if (Blood <= 0)
            {
                Death();
            }
            //Debug.Log(Blood);
        }
    }

    void Death()
    {
        IsDeath = true;
        //死亡音效
        AS.clip = AC;
        AS.Play();
        //死亡动画
        ani.SetTrigger("Death");
        PM.enabled=false;
        PS.enabled=false;
    }

    public void RestartLevel()
    {
        PlayerScore.Score = 0;
        SceneManager.LoadScene(0);
    }
}
