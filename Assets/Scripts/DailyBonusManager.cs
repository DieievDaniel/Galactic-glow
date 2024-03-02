using UnityEngine;
using TMPro;
using System;

public class DailyBonusManager : MonoBehaviour
{
    public TextMeshProUGUI creditsText;

    private DateTime lastBonusTime;

    private void Start()
    {
        if (PlayerPrefs.HasKey("LastBonusTime"))
        {
            long ticks = Convert.ToInt64(PlayerPrefs.GetString("LastBonusTime"));
            lastBonusTime = new DateTime(ticks);
        }
        else
        {
            lastBonusTime = DateTime.Now;
            SaveLastBonusTime();
        }
    }

    private void Update()
    {
        TimeSpan timeSinceLastBonus = DateTime.Now - lastBonusTime;
        if (timeSinceLastBonus.TotalDays >= 1)
        {
            Debug.Log(WinningField.Instance.creditsAmount);
            WinningField.Instance.creditsAmount += 10000;
            lastBonusTime = DateTime.Now;
            SaveLastBonusTime(); 
            UpdateCreditsText();
            WinningField.Instance.SaveCredits(); 
            Debug.Log(WinningField.Instance.creditsAmount);
        }
    }

    private void UpdateCreditsText()
    {
        creditsText.text = "" + WinningField.Instance.creditsAmount.ToString();
    }

    private void SaveLastBonusTime()
    {
        PlayerPrefs.SetString("LastBonusTime", lastBonusTime.Ticks.ToString());
        PlayerPrefs.Save();
    }
}
