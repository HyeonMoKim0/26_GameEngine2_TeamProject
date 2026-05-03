using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tetris : MonoBehaviour
{
    [Header("Sprites")]
    public Sprite blockSprite;
    public Sprite emptySprite;

    [Header("Grid Settings")]
    public int width = 6;
    public int height = 10;

    private Image[,] cellImages;
    private bool[,] grid;
    
    void Start()
    {
        grid = new bool[width, height];
        cellImages = new Image[width, height];

        InitializeUI();
        GenerateInitialBlock();
        RefreshBoard();
    }

    void InitializeUI()
    {
        for (int y = height - 1; y >= 0; y--)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject newCell = new GameObject($"Cell_{x}_{y}");
                newCell.transform.SetParent(this.transform, false);

                Image img = newCell.AddComponent<Image>();
                img.sprite = emptySprite;

                cellImages[x, y] = img;
            }
        }
    }

    void GenerateInitialBlock()
    {
        for (int y = 0; y < 4; y++)
        {
            int holeX = Random.Range(0, width);
            for (int x = 0; x < width; x++)
            {
                if (x != holeX)
                    grid[x, y] = true;
            }
        }
    }

    void SpawnBlock()
    {
        
    }

    public void RefreshBoard()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y])
                    cellImages[x, y].sprite = blockSprite;
                else
                    cellImages[x, y].sprite = emptySprite;
            }
        }
    }

    void Update()
    {
        RefreshBoard();
    }
}
