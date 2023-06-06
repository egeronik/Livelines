using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    public GameManager gameManager;    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            gameManager.IncreaseScore(10);
        }

        if (other.gameObject.CompareTag("Heal"))
        {
            Destroy(other.gameObject);
        }
    }
}
