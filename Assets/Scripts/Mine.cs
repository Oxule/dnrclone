using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour , ITrigger
{
    private bool used = false;
    
    public void OnEnter(int pawn)
    {
        if (used) return;
        used = true;
        transform.GetChild(0).gameObject.active = true;
        transform.GetChild(1).gameObject.active = true;
        GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, 1);
    }
}
