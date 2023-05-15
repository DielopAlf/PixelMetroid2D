using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlVictoria : MonoBehaviour
{
    public static ControlVictoria Instance;
    int powerupsRestantes;
    public ControlJugador jugador;


    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void Start()
    {
        powerupsRestantes = GameObject.FindGameObjectsWithTag("PowerUps").Length + 1;
    }

}
