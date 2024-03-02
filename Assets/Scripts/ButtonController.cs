using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Grid grid;
    [SerializeField] private WinningField winningField;

    public void GenerateGridByBuuton()
    {
        grid.GenerateGrid();
        winningField.UpdateText();
        winningField.PlayButtonClicked();
    }
}

