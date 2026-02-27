using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Player player; 
    public Transform orbitCenter;
    public float orbitRadius = 1.5f; 
    
    public float rotationSpeed = 300f; 
    private float currentAngle = 0f; 

    void Start()
    {
        player = FindFirstObjectByType<Player>(); 
        if (player == null)
        {
            Debug.LogError("SwordController requires a Player script in the scene.");
        }

        if (orbitCenter == null)
        {
            orbitCenter = player.transform;
        }
    }

    void Update()
    {
        // Continuous Spin Logic
        currentAngle += rotationSpeed * Time.deltaTime;
        if (currentAngle > 360f) currentAngle -= 360f;

        float angleInRadians = currentAngle * Mathf.Deg2Rad;
        
        Vector2 orbitDirection = new Vector2(
            Mathf.Cos(angleInRadians),
            Mathf.Sin(angleInRadians)
        );

        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle - 90f);
        transform.position = orbitCenter.position + (Vector3)orbitDirection * orbitRadius;
    }

    // --- SWORD WEAPON: If the sword hits a monster, the monster dies ---
    // Note: This requires the sword's Collider2D to be set to "Is Trigger"
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object we hit has the "Monster" tag
        if (other.gameObject.tag == "Monster")
        {
            // Call the public method on the Player script to handle scoring and destruction
            if (player != null)
            {
                player.MonsterHit(other.gameObject);
            }
        }
    }
}