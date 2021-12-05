using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData 
{
    public static string currentLevel;
    public static float lives = 3;
    public static float lastCheckpoint = 0;

    //information about the levels

    //checkpoint tutorial
    public static float levelZeroLength = 380;

    //checkpoints level 1
    public static float levelOneCheckpoint = 165; //location of node 4 on track
    public static float levelOneLength = 445;

    //checkpoints level 3
    public static float levelThreeCheckpoint = 210; //location of node 5
    public static float levelThreeCheckpoint2 = 506;
}
