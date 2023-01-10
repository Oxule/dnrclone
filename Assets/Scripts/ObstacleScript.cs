using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITrigger
{
    void OnEnter(int pawn);
}

public class ObstacleScript : MonoBehaviour , ITrigger
{
    public void OnEnter(int pawn)
    {
        GameObject.Find("Player").GetComponent<PawnsGroup>().KillPawn(pawn);
    }
}
