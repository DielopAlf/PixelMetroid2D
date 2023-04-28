using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPowerUp : MonoBehaviour
{
    public int  cantidad;
    
    
    private void OntriggerEnter2D(Collider2D collision)

    {

        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ControlJugador>().IncrementarPuntos(cantidad);
            Destroy(gameObject);

        }

    }

}
