using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ControlEnemigo2 : MonoBehaviour
{

    public float velocidad;
    public Vector3 posicionFin;
    private Vector3 posicionInicio;
    private bool moviendoAFin;

    void Start()
    {
        posicionInicio = transform.position;
        moviendoAFin = true;
    }

    // Update is called once per frame
    void Update()
    {

        MoverEnemigo();
    }
    private void MoverEnemigo()
    {

        Vector3 posicionDestino = (moviendoAFin) ? posicionInicio : posicionFin;

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(posicionDestino.x, transform.position.y, transform.position.z), velocidad * Time.deltaTime);

        if (transform.position == posicionFin) moviendoAFin = false;
        if (transform.position == posicionInicio) moviendoAFin = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ControlJugador>().QuitarVida();
        }
    }

}

