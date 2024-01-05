using System.Collections;
using UnityEngine;

public class BeeMover : MonoBehaviour
{
    public float speed = 1.0f;
    public float waitTime = 2.0f;
    public Vector3 destinationPosition;
    public Vector3 homePosition;
    public GameObject[] Target;
    public GameObject[] Beehive;
    public bool destinationReached = false;
    public bool homeReached = false; 
    GameController GameController;

    private void Start()
    {
        GameController = FindFirstObjectByType<GameController>();
        Target = GameObject.FindGameObjectsWithTag("Target");
        destinationPosition = Target[Random.Range(0, Target.Length)].transform.position;
        Beehive = GameObject.FindGameObjectsWithTag("Beehive");
        homePosition = Beehive[Random.Range(0, Beehive.Length)].transform.position;
    }

    void Update()
    {
        if (!homeReached)
        {
            if (!destinationReached)
            {
                MoveObject(destinationPosition);
            }
            else
            {
                StartCoroutine(MoveOut());
            }
        }
        else
        {
            GameController.beesAtHome = GameController.beesAtHome +1;
            Destroy(gameObject);
        }
    }

    void MoveObject(Vector3 newTarget)
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
        //Wait until gun has rotated
        yield return new WaitForSeconds(1);
        GoHome(homePosition);
    }

    void GoHome(Vector3 newTarget)
    {
        LookAtPathDirection(newTarget);

        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, newTarget, step);

        if (transform.position == newTarget)
        {
            homeReached = true;
        }
    }
}
