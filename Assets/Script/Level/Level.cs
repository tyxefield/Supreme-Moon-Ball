using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public static int TOTAL_ZONES;
    public static int STAGE; //0 = Islands, 1 = Sky, 2 = Space. 
    public static int CURRENT_LEVEL = 0;

    protected GameObject area;

    public Player player;

    public Level()
    {
        CreateLevel();
    }

    public void DestroyLevel()
    {
        if (area != null)
            Object.Destroy(area);
    }

    public void DestroyGoals()
    {
        Goal.GoalFlag[] g = Object.FindObjectsOfType<Goal.GoalFlag>();

        for (int i = 0; i < g.Length; i++)
        {
            Object.Destroy(g[i]);
            continue;
        }

        return;
    }

    public void CreateLevel()
    {
        switch (CURRENT_LEVEL)
        {
            case 0:
                area = Object.Instantiate(LevelData.LEVEL_1);
                break;
            case 1:
                area = Object.Instantiate(LevelData.LEVEL_2);
                break;
            case 2:
                area = Object.Instantiate(LevelData.LEVEL_3);
                break;
            case 3:
                area = Object.Instantiate(LevelData.LEVEL_4);
                break;
        }


    }
}