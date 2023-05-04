using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPowerUp : MonoBehaviour
{
    public int  cantidad;
    public AudioClip recolectarSfx;
    
    private void OnTriggerEnter2D(Collider2D collision)

    {
        Debug.Log("recogido");
        if (collision.CompareTag("Player"))
        {
            
            collision.gameObject.GetComponent<ControlJugador>().IncrementrarPuntos(cantidad);//.
            collision.GetComponent<AudioSource>().PlayOneShot(recolectarSfx);                                                                            //GetComponent<AudioSource>().PlayOneShot(recolectarSfx);
            Destroy(gameObject);
            
        }

    }

}
