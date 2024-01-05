using UnityEngine;

/// <summary>
/// Spawns Bumblebees in a given interval.
/// </summary>
public class BumblebeeSpawner : MonoBehaviour
{
    // Actual interval in which the bb's are being spawned
    public float randomAppearTime;
    private float currentTime = 0;

    [Header("Minimum wait time (s)")]
    public float minWaitTime = 60.0f;
    [Header("Maximum wait time (s)")]
    public float maxWaitTime = 600.0f;

    [Header("The prefab to be spawned")]
    public GameObject bumbleBeePrefab;

    [Header("The bumblebee passes only once")]
    public bool flyOnce = true;


    void Start()
    {
        // random value between one and 10 minutes
        randomAppearTime = Random.Range(minWaitTime, maxWaitTime);
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        // time slot exhausted, let's go.
        if (currentTime >= randomAppearTime)
        {
            currentTime = 0;
            GameObject bb = Instantiate(bumbleBeePrefab);

            Destroy(bb, 10.0f);

            if (flyOnce)
            {
                enabled = false;
            }
        }

    }
}
