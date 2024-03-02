using UnityEngine;
using System.Collections.Generic;

public class WinChecker : MonoBehaviour
{
    public static bool CheckForWin(Dictionary<Sprite, int> elementCounts, Sprite[] gridSprites)
    {
        foreach (var key in elementCounts.Keys)
        {
            int elementIndex = GetElementIndex(key, gridSprites);
            int count = elementCounts[key];

            float coefficient = Coefficients.GetCoefficient(elementIndex, count);

            if (coefficient > 0 && count >= 8)
            {
                return true;
            }
        }

        return false;
    }

    private static int GetElementIndex(Sprite sprite, Sprite[] gridSprites)
    {
        for (int i = 0; i < gridSprites.Length; i++)
        {
            if (gridSprites[i] == sprite)
            {
                return i;
            }
        }
        return -1;
    }
}
