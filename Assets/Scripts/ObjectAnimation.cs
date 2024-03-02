using UnityEngine;
using System.Collections;

public class ObjectAnimation : MonoBehaviour
{
    public IEnumerator AnimateAppear(GameObject gridObject, float xPos, float yPos, float animTime)
    {
        float timePassed = 0;
        Vector3 targetPos = new Vector3(xPos, yPos, 0f);
        while (timePassed < animTime)
        {
            gridObject.transform.position = Vector3.Lerp(gridObject.transform.position, targetPos, timePassed / animTime);
            timePassed += Time.deltaTime;
            yield return null;
        }
        gridObject.transform.position = targetPos;
    }
}
