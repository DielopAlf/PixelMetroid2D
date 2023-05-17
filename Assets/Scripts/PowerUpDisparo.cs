using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDisparo : MonoBehaviour
{
   
    public float[] spawnPositions; // Rango de posiciones pre-seleccionadas
    public float minSpawnTime = 20f; // Tiempo mínimo entre apariciones
    public float maxSpawnTime = 40f; // Tiempo máximo entre apariciones
    public float powerUpDuration = 40f; // Duración del power-up
    public float shootingTimeReduction = 0.325f; // Reducción del tiempo de disparo
    public GameObject player; // Referencia al jugador (la nave)
    public GameObject powerUpEffect; // Efecto visual o partículas del power-up

    private void Start()
    {
        Invoke("SpawnPowerUp", Random.Range(minSpawnTime, maxSpawnTime));
    }

    private void SpawnPowerUp()
    {
        float spawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];

        GameObject newPowerUp = Instantiate(powerUpEffect, new Vector3(spawnPosition, 0f, 0f), Quaternion.identity);
        newPowerUp.GetComponent<PowerUpEffect>().player = player;
        newPowerUp.GetComponent<PowerUpEffect>().duration = powerUpDuration;
        newPowerUp.GetComponent<PowerUpEffect>().shootingTimeReduction = shootingTimeReduction;

        Invoke("SpawnPowerUp", Random.Range(minSpawnTime, maxSpawnTime));
    }

    public class PowerUpEffect : MonoBehaviour
    {
        public float duration; // Duración del power-up
        public float shootingTimeReduction; // Reducción del tiempo de disparo
        public GameObject player; // Referencia al jugador (la nave)

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                player.GetComponent<Balas>().fuerzaDisparo *= shootingTimeReduction;

                gameObject.SetActive(false);

                Invoke("ResetShootingTime", duration);
            }
        }

        private void ResetShootingTime()
        {
            player.GetComponent<Balas>().fuerzaDisparo /= shootingTimeReduction;
            gameObject.SetActive(true);
        }
    }
}

