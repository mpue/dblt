using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlySpawner : MonoBehaviour
{
    public GameObject fly;
    public float spawnInterval;
    private float spawnTime;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime > spawnInterval)
        {
            spawnTime = 0;
            Vector3 spawnPos = GetRandomPositionInView();
            Instantiate(fly, spawnPos, Quaternion.identity);
        }

    }

    Vector3 GetRandomPositionInView()
    {
        Camera mainCamera = Camera.main;
        Vector2 screenPosition = new Vector2(
            Random.Range(0, Screen.width),
            Random.Range(0, Screen.height)
        );

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, mainCamera.nearClipPlane));
        worldPosition.z = 0; // For a 2D game, we set Z to 0

        return worldPosition;
    }
}