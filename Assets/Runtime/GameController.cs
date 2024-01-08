using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject HiveLevel;
    public GameObject[] FlowerLevels;

    public int difficulty = 1;

    //Setting for difficulty 1
    //how many bees should spawn
    public int howManyBees = 10;
    //Haw many bees can be hit
    public int howManyBeesHit = 100;
    //Haw many bees can fail
    public int howManyBeesFail = 100;
    //Haw many hits can the bumblebee take
    public int howManyBumbleBeeHits = 100;
    public int howManyBeesMaxPerRound = 5;

    public GameObject GameOverObj; 
    public Text GameOverText;
    public GameObject WonObj;
    public Text WonText;
    public Text BeesHomeText;
    public Text TimerText;
    public Text RoundText;

    //Do not change
    public int beesAtHome = 0;
    public int beesGoHome = 0;
    public int beesHits = 0;
    public int beesFail = 0;
    public int flyHit = 0;
    public int bumbleBeeHits = 0;

    public int actualTime = 0;
    public int actualRound = 0;

    GameObject ActiveFlowerScene;

    private void Start()
    {
        howManyBees = howManyBees * difficulty;
        howManyBeesHit = howManyBeesHit / difficulty;
        howManyBeesFail = howManyBeesFail / difficulty;
        howManyBumbleBeeHits = howManyBumbleBeeHits / difficulty;
        howManyBeesMaxPerRound = howManyBeesMaxPerRound * actualRound;

        ActiveFlowerScene = FlowerLevels[Random.Range(0, FlowerLevels.Length)];
        ActiveFlowerScene.SetActive(true);
        actualRound = 1;
    }

    private void Update()
    {
        BeesHomeText.text = beesAtHome.ToString();
        TimerText.text = actualTime.ToString();
        RoundText.text = actualRound.ToString();

        if (beesAtHome == howManyBees)
        {
            WonGame();
            Debug.Log("Won Game - beesAtHome: " + beesAtHome);
        }
        //Game Over -> Too many bees hit
        if (beesHits == howManyBeesHit)
        {
            Debug.Log("Game Over - beesHit: " + beesHits);
            GameOver("Game Over - beesHit: " + beesHits);            
        }
        //Game Over -> Too many bees fail
        if (beesFail == howManyBeesFail)
        {
            Debug.Log("Game Over - beesHit: " + beesFail);
            GameOver("Game Over - beesFail: " + beesFail);
        }
        //Game Over -> bumblebee hit
        if (bumbleBeeHits >= howManyBumbleBeeHits)
        {
            Debug.Log("Game Over -  BumbleBee Hit");
            GameOver("Game Over -  BumbleBee Hit");
        }
    }

    public void GameOver(string reason)
    {
        Debug.Log(reason);
        GameOverObj.SetActive(true);
        GameOverText.text = reason;
    }

    public void RoundWon(bool hiveLevel)
    {
        if (hiveLevel)
        {
            HiveLevel.SetActive(false);
            ActiveFlowerScene = FlowerLevels[Random.Range(0, FlowerLevels.Length)];
            ActiveFlowerScene.SetActive(true);
        }
        else
        {
            ActiveFlowerScene.SetActive(false);
            HiveLevel.SetActive(true);            
        }
    }
    public void WonGame()
    {
        Debug.Log("Won - beesAtHome: " + howManyBees);
        WonObj.SetActive(true);
        WonText.text = "ALL BEES HOME";
    }
}
