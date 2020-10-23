using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HeroHP : MonoBehaviour
{
    public Transform Spawn;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.position = Spawn.position;
        }
    }

}


