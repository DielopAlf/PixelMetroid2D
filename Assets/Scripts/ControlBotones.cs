using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ControlBotones : MonoBehaviour
{
	public void OnBotonJugar()
	{
		SceneManager.LoadScene ("Nivel1");
	}
	public void OnBotonCreditos()
	{
		SceneManager.LoadScene("Creditos");
	}
	
	public void OnBotonMenu()
	{
		SceneManager.LoadScene("Menu");
	}
	public void OnBotonSalir()
	{
		Application.Quit();
	}

}
