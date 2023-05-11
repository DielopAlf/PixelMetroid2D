using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour
{
    
    public Canvas canvas;

    private InterfazController hud;

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

    private AudioSource audiosource;

    public AudioClip saltoSfx;
    public AudioClip vidasSfx;




    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    



    private ControlDatosJuego datosjuegos;

    private void Start()
    {
        tiempoInicio = Time.time;
        vulnerable = true;
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animacion = GetComponent<Animator>();
        hud = canvas.GetComponent<InterfazController>();
        datosjuegos = GameObject.Find("DatosJuegos").GetComponent<ControlDatosJuego>();
    }

   private void FixedUpdate()
    {
        float entradax = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(entradax * velocidad, fisica.velocity.y);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&& TocarSuelo())
        {

            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            audiosource.PlayOneShot(saltoSfx);
          }
        if (fisica.velocity.x > 0) sprite.flipX = false;

        else if (fisica.velocity.x < 0) sprite.flipX = true;

      animarJugador();

        hud.SetPowerUpsTxt(GameObject.FindGameObjectsWithTag("PowerUp").Length);
        if (GameObject.FindGameObjectsWithTag("PowerUp").Length== 0)

            GanarJuego();

       tiempoEmpleado = Mathf.RoundToInt(Time.time - tiempoInicio);
       hud.SetTiempoTxt(Mathf.RoundToInt(tiempoNivel - tiempoEmpleado));
        if (tiempoNivel - tiempoEmpleado < 0) FinJuego();
        
           
        
        

    }
    private void GanarJuego()
    {
        float tiempoEmpleado = Time.time - tiempoInicio;
        puntuacion = (numVidas * 100) + Mathf.RoundToInt(tiempoNivel - tiempoEmpleado);

        datosjuegos.Puntuacion = Mathf.FloorToInt(puntuacion);
        datosjuegos.Ganado = true;   
        SceneManager.LoadScene("FinNivel");
    }

    private void animarJugador()
    {
        if(!TocarSuelo()) animacion.Play("jugadorSaltando");
        else if ((fisica.velocity.x>1 || fisica.velocity.x < -1)&& fisica.velocity.y == 0)
        animacion.Play("jugadorCorriendo");
        else if ((fisica.velocity.x<1 || fisica.velocity.x > -1)&& fisica.velocity.y == 0)
        animacion.Play("jugadorParado");
    }
    private bool TocarSuelo()
    {
        RaycastHit2D toca = Physics2D.Raycast
            (transform.position + new Vector3(0, -2f,0) , 
            Vector2.down, 0.2f);
        return toca.collider != null;
    }
    public void FinJuego()
    {
        datosjuegos.Ganado = false;
        SceneManager.LoadScene("FinNivel");
    }
    public void IncrementrarPuntos(int cantidad)
    {
        puntuacion += cantidad;

    }
    public void QuitarVida()
    {
        if (vulnerable)
        {
            audiosource.PlayOneShot(vidasSfx);
            vulnerable = false;
            numVidas--;
            hud.SetVidasTxt(numVidas);
            if (numVidas == 0)
            {
                // Reiniciar la escena
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Invoke("HacerVulnerable", 1f);
                sprite.color = Color.red;
                audiosource.PlayOneShot(vidasSfx);
            }
        }
    }
    private  void HacerVulnerable()
    {
        vulnerable= true;
        sprite.color = Color.white;
    }
}
