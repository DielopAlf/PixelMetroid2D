using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPowerUp : MonoBehaviour
{
    public int  cantidad;
    public AudioClip recolectarSfx;
    
    private void OntriggerEnter2D(Collider2D collision)

    {

        if (collision.CompareTag("Player"))
        {
            
            collision.GetComponent<AudioSource>().PlayOneShot(recolectarSfx);
            Destroy(gameObject);

        }

    }

}
