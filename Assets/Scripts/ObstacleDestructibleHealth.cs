using UnityEngine;

public class ObstacleDestructibleHealth : MonoBehaviour
{
    public float health;

    private void Update()
    {
        if(health < 1)
        {
            Destroy(gameObject);
        }
    }
}
