﻿//Source: https://noobtuts.com/unity/2d-tetris-game
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    public static int w = 10;
    public static int h = 20;
    public static Transform[,] grid = new Transform[w,h];
    public static bool isFinished = false;

    public static Vector2 roundVec2(Vector2 v) {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool isInsideBorder(Vector2 pos) {
        return ((int) pos.x >= 0 &&
                (int) pos.x < w &&
                (int) pos.y >= 0 &&
                (int) pos.y <= h);
    }

    public static void deleteRow(int y) {
        for (int x = 0; x < w; ++x) {
            Destroy(grid[x,y].gameObject);
            grid[x,y] = null;
        }
    }

    public static void decreaseRow(int y) {
        for (int x = 0; x < w; ++x) {
            if (grid[x,y] != null) {
                grid[x, y-1] = grid[x,y];
                grid[x,y] = null;

                grid[x, y-1].position += new Vector3(0, -1, 0);
            }
        }
    }

    //Deviation from tutorial: I think this makes more sense if y is the first row
    //that get moved down.
    public static void decreaseRowsAbove(int y) {
        for (int i = y + 1; i < h; ++i) {decreaseRow(i);}
    }

    public static bool isRowFull(int y) {
        for (int x = 0; x < w; ++x) {
            if (grid[x,y] == null) {
                return false;
            }
        }

        return true;
    }

    public static void deleteFullRows() {
        for (int y = 0; y < h; ++y) {
            if (isRowFull(y)) {
                deleteRow(y);
                decreaseRowsAbove(y);

                y--;
            }
        }
    }
}
