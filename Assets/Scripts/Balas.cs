using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour
{
    public GameObject prefabProyectil;
    public Transform puntoDisparo;
    public float fuerzaDisparo;
    public AudioClip sonidoDisparo;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Disparar();
        }
    }

    public void Disparar()
    {
        GameObject disparo = Instantiate(prefabProyectil, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody2D rbDisparo = disparo.GetComponent<Rigidbody2D>();

        Vector2 direccion = (transform.localScale.x > 0) ? Vector2.right : Vector2.left;

       
        rbDisparo.AddForce(direccion * fuerzaDisparo, ForceMode2D.Impulse);

        AudioSource.PlayClipAtPoint(sonidoDisparo, puntoDisparo.position);

        Destroy(disparo, 0.3f); 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemigo"))
        {
            Destroy(gameObject);
        }
    }
}