using UnityEngine;

public class LevelController : MonoBehaviour
{

    public int howManyBees = 50; 
    public int beesAtHome = 0;     
    public int beesGoHome = 0;
    public int beesHit = 0;
    public int beesFail = 0;
    public int flyHit = 0;

    public bool hiveLevel = false;

    void Start()
    {
        
    }
  
    void Update()
    {
        if (beesAtHome == howManyBees) 
        {
            Debug.Log("Won - beesAtHome: "+ howManyBees);
            Won();
        }
        if (beesGoHome == howManyBees)
        {
            Debug.Log("Won - beesGoHome: " + howManyBees);
            Won();
        }
        if (beesHit == 10)
        {
            Debug.Log("Game Over - beesHit: " + beesHit);
            GameOver();
        }
        if (beesFail == 10)
        {
            Debug.Log("Game Over - beesHit: " + beesHit);
            GameOver();
        }
    }

    public void GameOver()
    {

    }

    public void Won()
    {

    }
}
