using System.Collections;
using UnityEngine;

public class BeeMover : MonoBehaviour
{
    public float speed = 1.0f;
    public float waitTime = 1.0f;
    Vector3 destinationPosition;
    Vector3 GoHomePosition;
    Vector3 FailPosition;
    GameObject[] FlowerPos;
    GameObject[] GoHomePos;
    GameObject[] Beehives;
    public GameObject[] FailPos;
    bool destinationReached = false;
    bool homePosReached = false;
    bool failPosReached = false;
    public bool fail = false;
    public bool success = false;
    LevelController LevelController;
    GameController GameController;

    private void Awake()
    {
        GameController = FindFirstObjectByType<GameController>();
    }

    private void Start()
    {
        LevelController = FindFirstObjectByType<LevelController>();      
        FailPos = GameObject.FindGameObjectsWithTag("FailPos");
        FailPosition = FailPos[Random.Range(0, FailPos.Length)].transform.position;

        if (!LevelController.hiveLevel)
        {
            FlowerPos = GameObject.FindGameObjectsWithTag("FlowerPos");
            destinationPosition = FlowerPos[Random.Range(0, FlowerPos.Length)].transform.position;
            GoHomePos = GameObject.FindGameObjectsWithTag("GoHomePos");
            GoHomePosition = GoHomePos[Random.Range(0, GoHomePos.Length)].transform.position;
        }
        else
        {
            Beehives = GameObject.FindGameObjectsWithTag("Beehive");
            destinationPosition = Beehives[Random.Range(0, Beehives.Length)].transform.position;
        }
    }

    void Update()
    {
        if (!fail)
        {
            if (!homePosReached)
            {
                if (!destinationReached)
                {
                    MoveBee(destinationPosition);
                }
                else
                {
                    success = true; 
                    if (!LevelController.hiveLevel)
                    {
                        StartCoroutine(GoHome());
                    }
                    else
                    {
                        GameController.beesLeft--;
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                GameController.beesGoHome++;
                Destroy(gameObject);
            }
        }
        else
        {
            if (!failPosReached)
            {
                Fail();
            }
            else
            {
                GameController.beesFail++;
                Destroy(gameObject);
            }
        }
    }

    void MoveBee(Vector3 newPos)
    {
        LookAtPathDirection(newPos);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, newPos, step);

        if (transform.position == newPos)
        {
            destinationReached = true;
        }
    }

    IEnumerator GoHome()
    {
        yield return new WaitForSeconds(waitTime);
        LookAtPathDirection(GoHomePosition);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, GoHomePosition, step);

        if (transform.position == GoHomePosition)
        {
            homePosReached = true;
        }
    }

    void Fail()
    {
        LookAtPathDirection(FailPosition);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, FailPosition, step);

        if (transform.position == FailPosition)
        {
            failPosReached = true;
        }
    }

    void LookAtPathDirection(Vector3 nextPoint)
    {
        Vector3 direction = nextPoint - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
