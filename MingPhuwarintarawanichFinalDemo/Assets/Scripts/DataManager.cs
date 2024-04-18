using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static int numberOfKilledGhosts = 0;
    public static int currentLevel = 1;
    public const int LEVEL_1_GHOSTS = 7;
    public const int LEVEL_2_GHOSTS = 5;
    public static float time = 180f;
    public static bool isTimeRun = false;
    public static bool isKeyCollected = false;
    public static bool isGameEnd = false;

    public static void ResetValues()
    {
        numberOfKilledGhosts = 0;
        currentLevel = 1;
        time = 180f;
        isTimeRun = false;
        isKeyCollected = false;
        isGameEnd = false;
    }
}
