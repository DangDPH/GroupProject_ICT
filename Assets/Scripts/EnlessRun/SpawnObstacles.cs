using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    public GameObject obstacle;
    public float minY;
    public float maxY;
    public float timeBetweenSpawn = 1.2f;

    private float spawnTime;

    void Update()
    {
        if (Time.time > spawnTime)
        {
            Spawn();
            spawnTime = Time.time + timeBetweenSpawn;
        }
    }

    void Spawn()
    {
        float randomY = Random.Range(minY, maxY);

        Vector3 spawnPos = new Vector3(
            transform.position.x,
            randomY,
            0
        );

        Instantiate(obstacle, spawnPos, Quaternion.identity);
    }
}
