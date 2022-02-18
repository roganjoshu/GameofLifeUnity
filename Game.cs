using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private static int height = 68; //1080/16 (size of sprite/unit)
    private static int width = 120; //1920/16
    Cell[,] game_board = new Cell[width, height];

    // Start is called before the first frame update
    public void Start()
    {
        generateBoard();
        StartCoroutine(waiter());
    }

    void getAliveNeighbours(Cell cell, int row, int col)
    {
        int liveCellCount = 0;
        // through offsets/neighbours 
        for (int i_offset = -1; i_offset <= 1; i_offset++)
        {
            for (int j_offset = -1; j_offset <= 1; j_offset++)
            {
                //if not origin cell
                if (!(i_offset == 0 && j_offset == 0))
                {
                    //if cell to check is in bounds, i.e. not outside of width/height of board
                    if (isInBounds(row + i_offset, col + j_offset, height, width))
                    {
                        //if the value is true
                        if (game_board[row + i_offset, col + j_offset].state == true)
                        {
                            liveCellCount += 1;
                        }
                    }
                }
            }
        }
        cell.liveNeighbours = liveCellCount;
    }

    bool isInBounds(int targetRow, int targetCol, int boardWidth, int boardHeight)
    {
        //if target cell is within height and width boundary return true
        if (targetRow >= 0 && targetRow < boardHeight)
        {
            if (targetCol >= 0 && targetCol < boardWidth)
            {
                return true;
            }
        }
        return false;
    }

    void generateBoard()
    {
        //for every cell coordinate generate new cell object and load sprite
        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < height; col++)
            {
                Cell cell = Instantiate(Resources.Load("Prefabs/white block", typeof(Cell)), new Vector2(row, col), Quaternion.identity) as Cell;
                game_board[row, col] = cell;
                game_board[row, col].SetCellState(); 
            }
        }
    }

    void iterate()
    {   
        //get live neighbours for every cell
        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < height; col++)
            {
                getAliveNeighbours(game_board[row, col], row, col);
            }
        }
        
        //calculate next state for cell
        for (int row = 0; row < width; row++)
        {
            for (int col = 0; col < height; col++)
            {
                if (game_board[row, col].state == true)
                {
                    if (game_board[row, col].liveNeighbours < 2 || game_board[row, col].liveNeighbours > 3)
                    {
                        game_board[row, col].state = false;
                        game_board[row, col].setSprite();
                    }
                }
                else if (game_board[row, col].state == false)
                {
                    if (game_board[row, col].liveNeighbours == 3)
                    {
                        game_board[row, col].state = true;
                        game_board[row, col].setSprite();
                    }
                }
            }
        }
        
    }

    IEnumerator waiter()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            iterate();
        }
    }
}
