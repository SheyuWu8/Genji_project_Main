using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackContact : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            other.gameObject.GetComponent<ObstacleDestructibleHealth>().health -= 1;
        }
    }
}
