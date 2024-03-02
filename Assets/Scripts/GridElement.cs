using UnityEngine;

public class GridElement 
{
    [SerializeField] private float[] values = new float[3]; 

    public float[] Values
    {
        get { return values; }
        set { values = value; }
    }
}
