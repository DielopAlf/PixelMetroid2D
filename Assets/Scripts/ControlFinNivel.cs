using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ControlFinNivel : MonoBehaviour
{
    public TextMeshProUGUI mensajeFinalTexto;
    private ControlDatosJuego datosjuego;


    void Start()
    {
        datosjuego = GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();
        string mensajeFinal = (datosjuego.Ganado) ? "HA GANADO!!" :"HA PERDIDO";
        if (datosjuego.Ganado) mensajeFinal += "Puntuación:" + datosjuego.Puntuacion;

        mensajeFinalTexto.text = mensajeFinal;
    }
   
}
