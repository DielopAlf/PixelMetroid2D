using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDatosJuego : MonoBehaviour
{
    private int maxpuntuacion;
    private int puntuacion;
    private bool ganado;
    
   public int Puntuacion { get => puntuacion; set => puntuacion = value; }

   public bool Ganado { get => ganado; set => ganado = value; }
   public int MaxPuntuacion { get => maxpuntuacion; set => maxpuntuacion = value; }

   private void Awake()
   {
    int numInstancias = FindObjectsOfType<ControlDatosJuego>().Length;

    if(numInstancias != 1)
    {
        Destroy(this.gameObject);
    }
    else
    {
            DontDestroyOnLoad(this.gameObject);

    }
     ControlPowerUp[] powerups = FindObjectsOfType<ControlPowerUp>();
     if(powerups.Length>0)
     {
          foreach (ControlPowerUp powerup in powerups)
     {
          maxpuntuacion += powerup.cantidad;     
     }
     }



     

   }  

    

}
