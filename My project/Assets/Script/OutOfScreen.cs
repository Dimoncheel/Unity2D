using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfScreen : MonoBehaviour
{
    private GameObject SpawnPoint;

    void Start()
    {
        SpawnPoint = GameObject.Find("SpawnPoint");  
    }

    void Update()
    {

    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject.Destroy(other.gameObject);
    }
}
