using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // The monster Prefab you want to spawn
    public GameObject monsterPrefab;

    // A reference to the Player's Transform (to find a good spawn location)
    public Transform playerTransform;

    // Time between spawns
    public float spawnInterval = 6f;

    // Distance from the player where monsters should appear
    public float spawnDistance = 10f;

    private float timer;

    void Start()
    {
        // Safety check to ensure the player reference is set
        if (playerTransform == null)
        {
            playerTransform = FindAnyObjectByType<Player>().transform;
        }

        // Initialize the timer so the first monster spawns quickly
        timer = spawnInterval; 
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnMonster();
            timer = spawnInterval; // Reset the timer
        }
    }

    void SpawnMonster()
    {
        if (monsterPrefab == null || playerTransform == null) return;

        // 1. Calculate a random angle for the spawn position
        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        
        // 2. Determine the spawn position in a circle around the player
        Vector3 spawnOffset = new Vector3(
            Mathf.Cos(angle),
            Mathf.Sin(angle),
            0
        ) * spawnDistance;

        Vector3 spawnPosition = playerTransform.position + spawnOffset;

        // 3. Instantiate the monster
        GameObject newMonster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

        // Optional: If the MonsterAI script is on the prefab, it will automatically start moving.
    }
}