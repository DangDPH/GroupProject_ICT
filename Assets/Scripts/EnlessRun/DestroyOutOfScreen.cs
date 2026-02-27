using UnityEngine;

public class DestroyOutOfScreen : MonoBehaviour
{
    void Update()
    {
        if (transform.position.x < -15f)
        {
            Destroy(gameObject);
        }
    }
}
