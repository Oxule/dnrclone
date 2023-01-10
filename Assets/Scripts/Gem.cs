using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour, ITrigger
{
    public enum Type
    {
        KillPawn,
        AddPawn,
        AddGems,
        End
    }

    public Type type;
    public int Count;
    public bool Used;
    
    public void OnEnter(int pawn)
    {
        if (Used) return;
        Used = true;
        Debug.Log("Gem collected");
        var pg = GameObject.FindWithTag("Player").GetComponent<PawnsGroup>();
        switch (type)
        {
            case Type.KillPawn:
                for (int i = 0; i < Count; i++)
                {
                    pg.KillPawn(Random.Range(0, pg.Pawns.Count));
                }
                break;
            case Type.AddPawn:
                for (int i = 0; i < Count; i++)
                {
                    pg.AddPawn();
                }
                break;
            case Type.AddGems:
                GameManager.Gems += Count;
                break;
            case Type.End:
                GameManager.EndGame(true);
                break;
        }
        Destroy(gameObject);
    }
}
