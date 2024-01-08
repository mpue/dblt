using UnityEngine;

public class LevelController : MonoBehaviour
{
    //Defeinitions specific for each Level
    //Is this the hive Level?
    public bool hiveLevel = false; 
    public int howManyBeesInRound;

    float simpleTimer = 0;  
    GameController GameController;

    private void Awake()
    {
        GameController = FindFirstObjectByType<GameController>();
    }

    void Start()
    {         
        howManyBeesInRound = Random.Range(3, GameController.howManyBeesMaxPerRound) * GameController.actualRound;         
    }  
    void Update()
    {
 
        //Round won ->all bees go home
        if (GameController.beesGoHome == howManyBeesInRound)
        {
            Debug.Log("RoundWon - beesGoHome: " + howManyBeesInRound);
            GameController.RoundWon(hiveLevel);
        }  
    }

    private void FixedUpdate()
    {
        simpleTimer += Time.deltaTime;
        //Debug.Log("simpleTimer: " + Mathf.Round(simpleTimer));
        GameController.actualTime = (int)Mathf.Round(simpleTimer);
    }
}
