using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndPanel : MonoBehaviour
{
    public Color winColor;
    public Color loseColor;
    
    public void SetResult(bool win)
    {
        transform.GetChild(0).gameObject.active = true;
        var txt = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        txt.text = win ? "You Win!" : "You Lose!";
        txt.color = win ? winColor : loseColor;
    }
}
