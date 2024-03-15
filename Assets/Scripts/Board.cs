using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board : MonoBehaviour
{
    public Cake cakePrefab;
    public GiftBox giftboxPrefab;
    public Candy candyPrefab;

    public int level = 1;
    Dictionary<int, int[]> myDic = new Dictionary<int, int[]>();
    Dictionary<int, int[]> CandyDic = new Dictionary<int, int[]>();

    private Grid grid;
    private List<Cake> cakes;
    private List<GiftBox> boxs;
    private List<Candy> candies;

    public event Action OnConditionTrue;

    private void Awake()
    {
        grid = GetComponentInChildren<Grid>();
        cakes = new List<Cake>(9);
        boxs = new List<GiftBox>(9);
        candies = new List<Candy>(9);
        int[] lv1 = { 1, 7 };
        myDic.Add(1, lv1);
        int[] lv2 = { 3, 5 };
        myDic.Add(2, lv2);
        int[] lv3 = { 7, 1 };
        myDic.Add(3, lv3);
        int[] cdlv2 = {2,6 };
        CandyDic.Add(2, cdlv2);
        int[] cdlv3 = { 0, 5, 6 };
        CandyDic.Add(3, cdlv3);
    }
    private void Start()
    {

        //CreateCandy(3);
        int[] arr = myDic[level];
        CreateCake(arr[0]);
        CreateGiftBox(arr[1]);

    }
    private void CreateCake(int i)
    {
        Cake cake = Instantiate(cakePrefab, grid.transform);
        cake.Spawn(grid.cells[i]);
        cakes.Add(cake);
    }
    private void CreateGiftBox(int i)
    {
        GiftBox giftBox = Instantiate(giftboxPrefab, grid.transform);
        giftBox.Spawn(grid.cells[i ]);
        boxs.Add(giftBox);
    }
    private void CreateCandy(int i)
    {
        Candy candy = Instantiate(candyPrefab, grid.transform);
        candy.Spawn(grid.cells[i ]);
        candies.Add(candy);
    }

    public void ClearBoard()
    {
        foreach(var cell in grid.cells)
        {
            cell.candy = null;
            cell.giftbox = null;
            cell.cake = null;
        }
        foreach (var cake in cakes)
        {
            Destroy(cake.gameObject);
        }

        cakes.Clear();
        foreach (var box in boxs)
        {
            Destroy(box.gameObject);
        }

        boxs.Clear();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector2Int.up, 0, 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector2Int.left, 1, 1, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector2Int.down, 0, 1, grid.height - 2, -1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector2Int.right, grid.width - 2, -1, 0, 1);
        }
    }
    private void Move(Vector2Int direction, int startX, int incrementX, int startY, int incrementY)
    {
        for (int x = startX; x>= 0 && x< grid.width; x+= incrementX)
        {
            for (int y = startY; y >= 0 && y < grid.height; y+= incrementY) {
                Cell cell = grid.GetCell(x, y);
                if (cell.occupied)
                {
                    {
                        if (cell.cake != null)
                        {
                            MoveCake(cell.cake, direction);
                        }
                        else { Console.Write("cake null"); }
                        if (cell.giftbox != null)
                        {
                            MoveGiftBox(cell.giftbox, direction);
                        }
                        else { Console.Write("gift null"); }
                    }
                    if (CheckWin())
                    {
                        OnConditionTrue?.Invoke();
                    }
                }
            }
        }
        
    }
    private void MoveCake(Cake cake, Vector2Int direction)
    {
        Cell newCell = null;
        Cell adjacent = grid.GetAdjacentCell(cake.cell, direction);

        while (adjacent != null)
        {
            if (adjacent.occupied)
            {
                if(CanMerge(cake, adjacent.giftbox))
                {
                    Merge(cake, adjacent.giftbox);
                }
                break;
            }
            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }
        if (newCell != null)
        {
            cake.MoveTo(newCell);
            Debug.Log("cake: " + newCell.coordinates);
        }
    }
    private void MoveGiftBox(GiftBox giftBox, Vector2Int direction)
    {
        Cell newCell = null;
        Cell adjacent = grid.GetAdjacentCell(giftBox.cell, direction);

        while (adjacent != null)
        {
            if (adjacent.occupied)
            {
                if (CanMerge(adjacent.cake, giftBox))
                {
                    Merge(adjacent.cake,giftBox);
                }
                break;
            }
            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }
        if (newCell != null)
        {

            giftBox.MoveTo(newCell);
            Debug.Log("box: " +newCell.coordinates);
        }
    }
    private bool CanMerge(Cake cake, GiftBox gift)
    {
        //if(cake.cell.coordinates.x == gift.cell.coordinates.x &&
        //    cake.cell.coordinates.y < gift.cell.coordinates.y)
        //{
        //    return true;
        //}
        //return false;
        return cake.cell.coordinates.x == gift.cell.coordinates.x && cake.cell.coordinates.y < gift.cell.coordinates.y;
    }
    private void Merge(Cake cake, GiftBox box)
    {
        cakes.Remove(cake);
        cake.Merge(box.cell);
    }
    public bool CheckWin()
    {
        if (cakes.Count == 0)
        {
            return true;
        }
        else return false;
    }
    public void SetupLevel(int level)
    {
        int[] arr = myDic[level];
        CreateCake(arr[0]);
        CreateGiftBox(arr[1]);
        int[] candyArr = CandyDic[level];
        foreach(int i in candyArr)
        {
            CreateCandy(i);
        }
    }
}
