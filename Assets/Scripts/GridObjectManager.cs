using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GridObjectManager : MonoBehaviour
{

    public IEnumerator DestroyObjectsSmoothly(Sprite key, List<Vector3> emptyPositions, Dictionary<Sprite, int> elementCounts, float spriteSize)
    {
        List<Transform> objectsToDestroy = new List<Transform>();
        List<float> destroyTimes = new List<float>();

        foreach (Transform child in transform)
        {
            if (child.GetComponent<SpriteRenderer>().sprite == key)
            {
                objectsToDestroy.Add(child);
                destroyTimes.Add(Random.Range(0.0f, 0.3f));
            }
        }

        float maxDestroyTime = 0f;
        foreach (float time in destroyTimes)
        {
            if (time > maxDestroyTime)
            {
                maxDestroyTime = time;
            }
        }

        for (int i = 0; i < objectsToDestroy.Count; i++)
        {
            Transform obj = objectsToDestroy[i];
            float duration = destroyTimes[i];

            StartCoroutine(LowerObjectSmoothly(obj, duration));
            StartCoroutine(DestroyObjectSmoothly(obj, duration));

            emptyPositions.Add(obj.position);
        }

        elementCounts[key] = 0;

        yield return new WaitForSeconds(maxDestroyTime);
    }

    private IEnumerator LowerObjectSmoothly(Transform obj, float duration)
    {
        float timer = 0f;
        Vector3 initialPos = obj.position;
        Vector3 targetPos = new Vector3(initialPos.x, initialPos.y - Grid.Instance.spacing, initialPos.z);

        while (timer < duration)
        {
            obj.position = Vector3.Lerp(initialPos, targetPos, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        obj.position = targetPos;
    }

    private IEnumerator DestroyObjectSmoothly(Transform obj, float duration)
    {
        float timer = 0f;
        Vector3 initialScale = obj.localScale;

        while (timer < duration)
        {
            float scale = Mathf.Lerp(1f, 0f, timer / duration);
            obj.localScale = initialScale * scale;
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(obj.gameObject);
    }

    public IEnumerator SpawnObjectsSmoothly(List<Vector3> emptyPositions, Sprite[] gridSprites, Dictionary<Sprite, int> elementCounts, float spriteSize)
    {
        // Перемешиваем список пустых позиций перед началом создания новых объектов
        ShuffleList(emptyPositions);

        foreach (Vector3 position in emptyPositions)
        {
            float xPos = position.x;
            float yPos = position.y;
            Sprite gridSprite = gridSprites[Random.Range(0, gridSprites.Length)]; // Выбор случайного спрайта

            if (elementCounts.ContainsKey(gridSprite))
            {
                elementCounts[gridSprite]++;
            }
            else
            {
                elementCounts.Add(gridSprite, 1);
            }

            GameObject gridObject = new GameObject("GridObject");
            gridObject.transform.position = new Vector3(xPos, yPos, 0f);
            gridObject.transform.localScale = new Vector3(spriteSize, spriteSize, 1.0f);
            gridObject.transform.SetParent(Grid.Instance.transform);

            SpriteRenderer spriteRenderer = gridObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = gridSprite;

            float timePassed = 0;
            float animTime = 0.1f;
            Vector3 targetPos = new Vector3(xPos, yPos, 0f);
            while (timePassed < animTime)
            {
                gridObject.transform.position = Vector3.Lerp(gridObject.transform.position, targetPos, timePassed / animTime);
                timePassed += Time.deltaTime;
                yield return null;
            }
            gridObject.transform.position = targetPos;
        }

        emptyPositions.Clear();
    }

    // Метод для перемешивания списка
    private void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }



}
