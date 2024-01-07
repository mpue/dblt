using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject HiveLevel;
    public GameObject FlowerLevel1;
    public GameObject FlowerLevel2;
    public GameObject FlowerLevel3;
    
    public void GameOver(string reason)
    {
        Debug.Log(reason);
    }

    public void Won(bool hiveLevel)
    {
        Debug.Log("Won - hiveLevel: " + hiveLevel);
    }
}
