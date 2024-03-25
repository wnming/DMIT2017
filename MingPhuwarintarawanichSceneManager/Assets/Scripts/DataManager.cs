using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DataManager
{
    public static List<int> enemyHealth;

    public static void InitialEnemyHealth()
    {
        enemyHealth = new List<int>();
        for (int index = 0; index < 13; index++)
        {
            enemyHealth.Add(50);
        }
    }
}
