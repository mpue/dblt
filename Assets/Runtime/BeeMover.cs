using System.Collections;
using UnityEngine;

public class BeeMover : MonoBehaviour
{
    public float speed = 1.0f;
    public float waitTime = 1.0f;
    Vector3 destinationPosition;
    Vector3 OutsidePosition;
    GameObject[] Target;
    GameObject[] OutsidePos;
    GameObject Beehive;
    bool destinationReached = false;
    bool posReached = false;
    GameController GameController;

    private void Start()
    {
        GameController = FindFirstObjectByType<GameController>();

        if (!GameController.hiveLevel)
        {             
            Target = GameObject.FindGameObjectsWithTag("Target");
            destinationPosition = Target[Random.Range(0, Target.Length)].transform.position;
            OutsidePos = GameObject.FindGameObjectsWithTag("OutsidePos");
            OutsidePosition = OutsidePos[Random.Range(0, OutsidePos.Length)].transform.position;
        }
        else
        {
            Beehive = GameObject.FindGameObjectWithTag("Beehive");
            destinationPosition = Beehive.transform.position;
        }
    }

    void Update()
    {
        if (!posReached)
        {
            if (!destinationReached)
            {
                MoveBee(destinationPosition);
            }
            else
            {
                if (!GameController.hiveLevel)
                {
                    StartCoroutine(MoveOut());
                }
                else
                {                   
                    GameController.beesAtHome = GameController.beesAtHome + 1;
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            GameController.beesGoHome = GameController.beesGoHome + 1;
            Destroy(gameObject);
        }
    }

    void MoveBee(Vector3 newTarget)
    {
        LookAtPathDirection(newTarget);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, newTarget, step);

        if (transform.position == newTarget)
        {
            destinationReached = true;
        }
    }

    void LookAtPathDirection(Vector3 nextPoint)
    {
        Vector3 direction = nextPoint - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    IEnumerator MoveOut()
    {
        yield return new WaitForSeconds(waitTime);
        GoOutside(OutsidePosition);
    }

    void GoOutside(Vector3 newTarget)
    {
        LookAtPathDirection(newTarget);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, newTarget, step);

        if (transform.position == newTarget)
        {
            posReached = true;
        }
    }
}
