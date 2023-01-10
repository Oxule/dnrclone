using System.Linq;
using UnityEngine;

public static class GameManager
{
    public static int Gems = 0;
    
    public static void EndGame(bool won)
    {
        GameObject.FindWithTag("Player").GetComponent<PawnsGroup>().GroupSpeed = 0;
        var endPanel = GameObject.FindWithTag("End");
        endPanel.SetActive(true);
        endPanel.GetComponent<EndPanel>().SetResult(won);
        if (won)
        {
            Debug.Log("You won!");
        }
        else
        {
            Debug.Log("You lost!");
        }
        
        PlayerPrefs.SetInt("Gems", Gems);
    }

    public static void Start()
    {
        Gems = PlayerPrefs.GetInt("Gems");
    }
}