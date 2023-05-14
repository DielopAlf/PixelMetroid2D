using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlEnemigo : MonoBehaviour
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

    void Update()
    {
        MoverEnemigo();
    }

    private void MoverEnemigo()
    {
        Vector3 posicionDestino = (moviendoAFin) ? posicionFin : posicionInicio;

        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);

        if (transform.position == posicionFin) moviendoAFin = false;
        if (transform.position == posicionInicio) moviendoAFin = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ControlJugador>().QuitarVida();
        }
        else if (other.gameObject.CompareTag("Proyectil"))
        {
            Desaparecer();
        }
    }

    private void Desaparecer()
    {
        // Destruye el objeto del enemigo
        Destroy(gameObject);
    }
}