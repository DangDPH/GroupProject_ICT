using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    public float moveSpeed = 1.5f;

    private Transform playerTarget;

    void Start()
    {
        Player player = FindFirstObjectByType<Player>();
        if (player != null)
        {
            playerTarget = player.transform;
        }
        else
        {
            Debug.LogError("MonsterAI cannot find Player in scene!");
        }
    }

    void Update()
    {
        if (playerTarget == null) return;

        Vector3 direction = (playerTarget.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}
