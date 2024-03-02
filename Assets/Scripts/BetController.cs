using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BetController : MonoBehaviour
{
    public TextMeshProUGUI betText;
    public WinningField winningField;

    public Button increaseButton;
    public Button decreaseButton;
    public Button playButton;

    private void Start()
    {
        UpdateText();
        CheckBetAmount();
    }

    public void IncreaseBet()
    {
        BetManager.Instance.betAmount += 25;
        UpdateText();
        CheckBetAmount();
    }

    public void Decrease()
    {
        if (BetManager.Instance.betAmount >= 25)
        {
            BetManager.Instance.betAmount -= 25;
            UpdateText();
            CheckBetAmount();
        }
    }

    public void UpdateText()
    {
        betText.text = "" + BetManager.Instance.betAmount.ToString();
    }

    private void CheckBetAmount()
    {
        if (BetManager.Instance.betAmount <= 0)
        {
            winningField.ButtonOff();
        }
        
    }
}
