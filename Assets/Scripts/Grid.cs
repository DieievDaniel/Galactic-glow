using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Grid : MonoBehaviour
{
    public Sprite[] gridSprites;
    public int rows;
    public int columns;
    public float spacing;
    public float spriteSize;
    public static bool isGridLogicInProgress = false;
    public float winnings;

    public Dictionary<Sprite, int> elementCounts = new Dictionary<Sprite, int>();
    private List<Vector3> emptyPositions = new List<Vector3>();

    private GridObjectManager objectManager;
    private ObjectAnimation objectAnimation;
    private static Grid instance;
    public static Grid Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        objectManager = GetComponent<GridObjectManager>();
        objectAnimation = GetComponent<ObjectAnimation>();
    }

    public void GenerateGrid()
    {
        Grid.isGridLogicInProgress = true;
        ClearGrid();

        Vector3 center = transform.position - new Vector3((columns - 1) * spacing / 2f, (rows - 1) * spacing / 2f, 0f);

        StartCoroutine(AnimateAppear(center));
    }

    private IEnumerator AnimateAppear(Vector3 center)
    {
        for (int x = 0; x < columns; x++)
        {
            for (int y = rows - 1; y >= 0; y--)
            {
                float xPos = center.x + x * spacing;
                float yPos = center.y + y * spacing;

                Sprite gridSprite = gridSprites[Random.Range(0, gridSprites.Length)];

                if (elementCounts.ContainsKey(gridSprite))
                {
                    elementCounts[gridSprite]++;
                }
                else
                {
                    elementCounts.Add(gridSprite, 1);
                }

                GameObject gridObject = new GameObject("GridObject");
                gridObject.transform.position = new Vector3(xPos, center.y + rows * spacing, 0f);
                gridObject.transform.localScale = new Vector3(spriteSize, spriteSize, 1.0f);
                gridObject.transform.SetParent(transform);

                SpriteRenderer spriteRenderer = gridObject.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = gridSprite;

                yield return StartCoroutine(objectAnimation.AnimateAppear(gridObject, xPos, yPos, 0.1f));
            }
        }

        StartCoroutine(CheckForWin());
    }

    public void ClearGrid()
    {
        foreach (Transform child in transform)
        {
            if (child.tag != "Background")
            {
                Destroy(child.gameObject);
            }
        }

        elementCounts.Clear();
    }


    public int GetElementIndex(Sprite sprite)
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

    private IEnumerator CheckForWin()
    {
        yield return new WaitForSeconds(1f);

        while (true) 
        {
            List<Sprite> keys = new List<Sprite>(elementCounts.Keys);

            foreach (var key in keys)
            {
                int elementIndex = GetElementIndex(key);
                int count = elementCounts[key];

                float coefficient = Coefficients.GetCoefficient(elementIndex, count);

                if (coefficient > 0 && count >= 8)
                {
                    winnings = BetManager.Instance.betAmount * coefficient;
                    Debug.Log("Element: " + key.name + ", Counts: " + count + ", Coefficient: " + coefficient + ", Winnings: " + winnings);
                    WinningField.Instance.UpdateText();
                    BetManager.Instance.IncreaseCredits();

                    yield return StartCoroutine(DestroyObjectsSmoothlyCoroutine(key));
                    yield return new WaitForSeconds(1f);
                    StartCoroutine(SpawnObjectsSmoothly());

                    yield return new WaitForSeconds(2f);
                }
            }

            if (!WinChecker.CheckForWin(elementCounts, gridSprites))
            {
                break; 
            }
        }

        Grid.isGridLogicInProgress = false;
    }

    private IEnumerator DestroyObjectsSmoothlyCoroutine(Sprite key)
    {
        yield return StartCoroutine(objectManager.DestroyObjectsSmoothly(key, emptyPositions, elementCounts, spriteSize));
    }

    private IEnumerator SpawnObjectsSmoothly()
    {
        yield return StartCoroutine(objectManager.SpawnObjectsSmoothly(emptyPositions, gridSprites, elementCounts, spriteSize));
    }
}
