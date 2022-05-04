using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_View : MonoBehaviour
{
    public TextMeshProUGUI timer;

    public void SetTimer(float time ) 
    {
        timer.text = time.ToString("F0");
    }
}
