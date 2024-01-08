using UnityEngine;

public class BeeSpawner : MonoBehaviour
{
    public GameObject Bee;
    public float spawnInterval = 5;
    private float spawnTime;

    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime > spawnInterval)
        {
            spawnTime = 0;
            Vector3 spawnPos = GetRandomPosOffScreen();
            Instantiate(Bee, spawnPos, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosOffScreen()
    {
        float x = Random.Range(-0.02f, 0.02f);
        float y = Random.Range(-0.02f, 0.02f);
        x += Mathf.Sign(x);
        y += Mathf.Sign(y);
        Vector3 randomPoint = new(x, y);
        randomPoint.z = 10f; // set this to whatever you want the distance of the point from the camera to be. Default for a 2D game would be 10.
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(randomPoint);
        return worldPoint;
    }
}
