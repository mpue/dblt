using UnityEngine;

public class LevelController : MonoBehaviour
{
    //Defeinitions specific for each Level
    //Is this the hive Level?
    public bool hiveLevel = false;
    //How many bees should be helped
    public int howManyBees = 50;
    //Haw many bees can be hit
    public int howManyBeesHit = 10;
    //Haw many bees can fail
    public int howManyBeesFail = 10;
    //Do not change
    public int beesAtHome = 0;     
    public int beesGoHome = 0;
    public int beesHit = 0;
    public int beesFail = 0;
    public int flyHit = 0;
    public bool bumbleBeeHit = true;     
    GameController GameController;

    void Start()
    {
        GameController = FindFirstObjectByType<GameController>();
    }
  
    void Update()
    {
        if (beesAtHome == howManyBees) 
        {
            Debug.Log("Won - beesAtHome: "+ howManyBees);
            GameController.Won(hiveLevel);
        }
        if (beesGoHome == howManyBees)
        {
            Debug.Log("Won - beesGoHome: " + howManyBees);
            GameController.Won(hiveLevel);
        }
        if (beesHit == howManyBeesHit)
        {
            Debug.Log("Game Over - beesHit: " + beesHit);
            GameController.GameOver("Game Over - beesHit: " + beesHit);
        }
        if (beesFail == howManyBeesFail)
        {
            Debug.Log("Game Over - beesHit: " + beesFail);
            GameController.GameOver("Game Over - beesFail: " + beesFail);
        }
        if (bumbleBeeHit)
        {
            GameController.GameOver("Game Over -  BumbleBee Hit");
        }
    }
}
