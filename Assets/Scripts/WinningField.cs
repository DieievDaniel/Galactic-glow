using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class WinningField : MonoBehaviour
{
    public TextMeshProUGUI winningsText;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI creditsText;
    public TextMeshProUGUI bonusText;
    public Button playButton;
    public Button increaseButton;
    public Button decreaseButton;
    public static WinningField Instance;

    public float creditsAmount = 10000;
    private bool isGeneratingGrid;
    private DateTime lastBonusTime;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        lastBonusTime = DateTime.Now;
        LoadCredits();
        UpdateText();
    }

    public void PlayButtonClicked()
    {
        if (creditsAmount >= BetManager.Instance.betAmount && !isGeneratingGrid)
        {
            creditsAmount -= BetManager.Instance.betAmount;
            UpdateText();
            SaveCredits();
        }
    }

    public void UpdateText()
    {
        winningsText.text = " " + Grid.Instance.winnings.ToString();
        betText.text = "" + BetManager.Instance.betAmount.ToString();
        creditsText.text = " " + creditsAmount.ToString();
    }

    private void Update()
    {
        ButtonOff();
        
    }

    public void ButtonOff()
    {
        var buttons = FindObjectsOfType<Button>();
        if (Grid.isGridLogicInProgress)
        {
            
            foreach (var button in buttons)
            {
                    button.interactable = false;
            }
        }
        else if(BetManager.Instance.betAmount <= 0)
        {
            
            foreach(var button in buttons)
            {
                if(button !=increaseButton)
                {
                    button.interactable = false;
                }
            }
        }
        else
        {
          
            foreach (var button in buttons)
            {
                button.interactable = true;
            }

            playButton.interactable = (creditsAmount >= BetManager.Instance.betAmount);
        }
    }

   

    public void EndGame()
    {
        SaveCredits();
    }

    public void SaveCredits()
    {
        PlayerPrefs.SetFloat("Credits", creditsAmount);
        PlayerPrefs.Save();
    }

    private void LoadCredits()
    {
        if (PlayerPrefs.HasKey("Credits"))
        {
            creditsAmount = PlayerPrefs.GetFloat("Credits");
        }
    }

   
    public void ResetCreditsOnce()
    {
        creditsAmount = 100000;
        SaveCredits();
        UpdateText();
    }
}
