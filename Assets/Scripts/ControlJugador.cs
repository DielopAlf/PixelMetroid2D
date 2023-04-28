using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour
{

    public int velocidad;
    public int fuerzaSalto;
    public int puntuacion;
    public int numVidas;
    private Rigidbody2D fisica;
    private SpriteRenderer sprite;
    private Animator animacion;
    private bool vulnerable;
    private float tiempoInicio;
    public float tiempoNivel;
    private int tiempoEmpleado;


    private void Start()
    {
        tiempoInicio = Time.time;
        vulnerable = true;
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animacion = GetComponent<Animator>();
    }

   private void FixedUpdate()
    {
        float entradax = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(entradax * velocidad, fisica.velocity.y);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& TocarSuelo())
        

            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        if (fisica.velocity.x > 0) sprite.flipX = false;

        else if (fisica.velocity.x < 0) sprite.flipX = true;

      animarJugador();  

      if (GameObject.FindGameObjectWithTag("PowerUp").Lenght ==0)
      
        GanarJuego();

        tiempoEmpleado = (int)(Time.time - tiempoInicio);
        if (tiempoNivel - tiempoEmpleado < 0)FinJuego();
      

    }
    private void GanarJuego()
    {
        tiempoEmpleado=(int)(Time- tiempoInicio); 
        puntuacion=(numVidas * 100) + (tiempoNivel - tiempoEmpleado);
        

    }


    private void animarJugador()
    {
        if(!TocarSuelo()) animacion.Play("jugadorSaltando");
        else if ((fisica.velocity.x>1 || fisica .velocity.x < -1)&& fisica.velocity.y == 0)
        animacion.Play("jugadorCorriendo");
        else if ((fisica.velocity.x<1 || fisica .velocity.x > -1)&& fisica.velocity.y == 0)
        animacion.Play("jugadorParado");
    }
    private bool TocarSuelo()
    {
        RaycastHit2D toca = Physics2D.Raycast
            (transform.position, Vector2.down, 0.2f);
        return toca.collider != null;
    }
    public void FinJuego()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void IncrementrarPuntos(int cantidad)
    {
        puntuacion += cantidad;

    }
    public void QuitarVida()
    {
        if (vulnerable)
        {
            vulnerable= false;
            numVidas --;
            if(numVidas ==0)FinJuego();
            Invoke("HacerVulnerable",1f);
            sprite.color = Color.red;
        }
    }
    private  void HacerVulnerable()
    {
        vulnerable= true;
        sprite.color = Color.white;
    }
}
