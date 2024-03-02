using UnityEngine;

public class Coefficients
{
    
    public static float[][] elementCoefficients = new float[][] {
        new float[] {20, 50, 100},    // Элемент 1
        new float[] {5, 20, 50},       // Элемент 2
        new float[] {3, 4, 24},        // Элемент 3
        new float[] {4, 10, 30},       // Элемент 4
        new float[] {2, 3, 20},        // Элемент 5
        new float[] {1.6f, 2.40f, 50}, // Элемент 6r
        new float[] {1, 2, 10},        // Элемент 7
        new float[] {0.5f, 1.5f, 4}    // Элемент 8
    };

    
    public static float GetCoefficient(int elementIndex, int count)
    {   
        if (count >= 8 && count < 10)
        {
            return elementCoefficients[elementIndex][0]; 
        }
        else if (count >= 10 && count < 11)
        {
            return elementCoefficients[elementIndex][1]; 
        }
        else if (count >= 12)
        {
            return elementCoefficients[elementIndex][2]; 
        }
        else
        {
            return 0; 
        }
    }
    public static float[] GetElementCoefficients(int elementIndex)
    {
        return elementCoefficients[elementIndex];
    }
}
