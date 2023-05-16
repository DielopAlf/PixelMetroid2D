using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInvencibilidad : MonoBehaviour
{
    public float duracionInvencibilidad = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PowerUpInvencibilidad"))
        {
            ControlJugador jugador = collision.GetComponent<ControlJugador>();

            // Activar la invencibilidad en el jugador
            jugador.ActivarInvencibilidad(duracionInvencibilidad);

            // Desactivar la pérdida de vidas durante el tiempo de invencibilidad
            jugador.DesactivarPerderVidas();

            // Destruir el power-up
            Destroy(gameObject);
        }
    }
}
