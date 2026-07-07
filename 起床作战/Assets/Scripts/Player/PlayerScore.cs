using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    //·ÖÊý
    public static int Score=0;
    public TextMeshProUGUI ScoreUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUI.text=$"Score£º{Score}";
    }
}
