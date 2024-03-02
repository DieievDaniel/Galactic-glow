using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BetManager : MonoBehaviour
{
    private static BetManager instance;
    public static BetManager Instance { get { return instance; } }
    

    public int betAmount;
    
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void IncreaseCredits()
    {
        if(Grid.Instance.winnings > 0)
        {
            WinningField.Instance.creditsAmount += Grid.Instance.winnings;        
        }
        Grid.Instance.winnings = 0;
    }

    
}