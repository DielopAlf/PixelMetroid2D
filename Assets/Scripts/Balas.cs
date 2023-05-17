using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balas : MonoBehaviour
{
    public GameObject prefabProyectil;
    public Transform puntoDisparo;
    public float fuerzaDisparo;
    public AudioClip sonidoDisparo;

    public float tiempoEntreProyectiles = 0.75f;
    private bool isFiring = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            IniciarDisparoContinuo();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            DetenerDisparoContinuo();
        }
    }

    public void IniciarDisparoContinuo()
    {
        if (!isFiring)
        {
            isFiring = true;
            StartCoroutine(FireContinuous());
        }
    }

    public void DetenerDisparoContinuo()
    {
        if (isFiring)
        {
            isFiring = false;
            StopCoroutine(FireContinuous());
        }
    }

    private IEnumerator FireContinuous()
    {
        while (isFiring)
        {
            Disparar();
            yield return new WaitForSeconds(tiempoEntreProyectiles);
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
}
