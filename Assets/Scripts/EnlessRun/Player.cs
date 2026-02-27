using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public FixedJoystick joystick;
    private Rigidbody2D rb;
    private Vector2 playerDirection;
    private void Start()
    {
        // Existing Start logic
    }
    private void Update()
    {
        // Existing Update logic
    }
    private void FixedUpdate()
    {
        // Existing FixedUpdate logic
    }
    public void MonsterHit(GameObject monster)
    {
        // Destroy the monster and handle scoring
        Destroy(monster);
    }
}