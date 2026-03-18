using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public GameObject blockPrefab;
    public GameObject gearPrefab;
    public GameObject winUI;
    public GameObject loseUI;
    public List<LevelLayout> allLevels;

    public LevelLayout activeLevel;
    public bool isWin = false;

    public int currentLevelIndex = 0;
    private int block = 0;
    public int currentStep = 0;

    public Block[,] grid;

    void Awake() => Instance = this;

    void Start()
    {
        LoadLevel(0);
    }

    public Vector3 GetWorldPos(int x, int y)
    {
        float startX = -(activeLevel.width - 1) / 2f;
        float startY = -(activeLevel.height - 1) / 2f;
        return transform.position + new Vector3(startX + x, startY + y, 0);
    }

    public void LoadLevel(int index)
    {
        if (index < 0 || index >= allLevels.Count) return;

        currentLevelIndex = index;
        GenerateLevel(allLevels[index]);
    }

    public void GenerateLevel(LevelLayout level)
    {
        activeLevel = level;
        block = 0;
        currentStep = activeLevel.Count;
        foreach (Transform child in transform)
            Destroy(child.gameObject);

        grid = new Block[activeLevel.width, activeLevel.height];

        for (int x = 0; x < activeLevel.width; x++)
        {
            for (int y = 0; y < activeLevel.height; y++)
            {
                int index = x + y * activeLevel.width;

                if (activeLevel.cellTypes == null || index >= activeLevel.cellTypes.Length)
                    continue;

                CellType type = activeLevel.cellTypes[index];

                if (type == CellType.Block)
                {
                    SpawnBlock(x, y, index);
                    block++;
                }
                else if (type == CellType.Gear)
                {
                    SpawnGear(x, y);
                }
            }
        }
    }

    void SpawnBlock(int x, int y, int index)
    {
        GameObject obj = Instantiate(blockPrefab, GetWorldPos(x, y), Quaternion.identity, transform);
        Block b = obj.GetComponent<Block>();

        b.gridX = x;
        b.gridY = y;

        if (activeLevel.directions != null && index < activeLevel.directions.Length)
            b.direction = activeLevel.directions[index];
        else
            b.direction = Direction.Right;

        b.SetupArrowRotation();

        grid[x, y] = b;
    }

    void SpawnGear(int x, int y)
    {
        Instantiate(gearPrefab, GetWorldPos(x, y), Quaternion.identity, transform);
    }

    public void RegisterBlockRemoved()
    {
        block--;
        if (block <= 0)
        {
            isWin = true;
            
            CheckWinCondition();
        }
    }
    public void CheckWinCondition()
    {
        foreach (var b in grid)
            if (b != null) return;
        winUI.SetActive(true);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.win);
        //Invoke(nameof(LoadNextLevel), 1f);
    }

    public void LoadNextLevel()
    {
        winUI.SetActive(false);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.moveBlock);
        LoadLevel(currentLevelIndex + 1);
    }
    public void RegisterMoveUsed()
    {
        currentStep--;
        CheckLoseCondition();
    }

    public void CheckLoseCondition()
    {
        if (currentStep <= 0 && block > 0)
        {
            loseUI.SetActive(true);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.lose);
        }
    }

    public void MoreMove()
    {
        currentStep++;
    }
    public void RemoveRandomBlock()
    {
        List<Block> list = new List<Block>();

        for (int x = 0; x < activeLevel.width; x++)
        {
            for (int y = 0; y < activeLevel.height; y++)
            {
                if (grid[x, y] != null)
                    list.Add(grid[x, y]);
            }
        }

        if (list.Count == 0) return;

        Block randomBlock = list[Random.Range(0, list.Count)];

        grid[randomBlock.gridX, randomBlock.gridY] = null;

        Destroy(randomBlock.gameObject);

        RegisterBlockRemoved();
    }
}