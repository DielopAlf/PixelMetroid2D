using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour
{
    private Vector3 posicionInicial;

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

    private float velocidadOriginal;


    public AudioClip saltoSfx;
    public AudioClip vidasSfx;
    private ControlDatosJuego datosjuegos;

    private Balas balas;

    private bool invencible;
    private float duracionInvencibilidad;

    private bool perderVidasActivo = true;

    private bool invulnerable = false;



    public void ActivarPerderVidas()
    {
        perderVidasActivo = true;
    }
    public void DesactivarPerderVidas()
    {
        perderVidasActivo = false;
    }


    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
    }
    

    private void Start()
    {
        posicionInicial = transform.position;
        tiempoInicio = Time.time;
        vulnerable = true;
        fisica = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animacion = GetComponent<Animator>();
        hud = canvas.GetComponent<InterfazController>();
        datosjuegos = GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();
        // datosjuegos.Puntuacion=0;
        invencible = false;
        duracionInvencibilidad = 0f;
        velocidadOriginal = velocidad;

       balas = GetComponent<Balas>();
    }
   

    private void FixedUpdate()
    {
        float entradax = Input.GetAxis("Horizontal");
        fisica.velocity = new Vector2(entradax * velocidad, fisica.velocity.y);
    }
    private void Update()
    {
        GameObject[] powerUpsArray = GameObject.FindGameObjectsWithTag("PowerUps");
        if (Input.GetKeyDown(KeyCode.Space)&& TocarSuelo())
        {

            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            audiosource.PlayOneShot(saltoSfx);
          }
        if (fisica.velocity.x > 0) sprite.flipX = false;

        else if (fisica.velocity.x < 0) sprite.flipX = true;

        else if (Input.GetMouseButtonDown(0))
        {
            balas.Disparar();
        }

        /* else if (powerUpsArray.Length == 0)
         {
             GanarJuego();
         }*/

        animarJugador();  

      GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUps");
      hud.SetPowerUpsTxt(powerUps.Length);

      /*if (powerUps.Length ==0)
      
        GanarJuego();*/

        tiempoEmpleado = (int)(Time.time - tiempoInicio);
       hud.SetTiempoTxt(Mathf.RoundToInt(tiempoNivel - tiempoEmpleado));
        if (tiempoNivel - tiempoEmpleado < 1)
        {
            FinJuego();
        }
        /*  if (Input.GetMouseButtonDown(0))
         {
             DispararProyectil();
         }*/

       else if (invencible)
        {
            // Realizar acciones espec�ficas durante la invencibilidad

            duracionInvencibilidad -= Time.deltaTime;
            if (duracionInvencibilidad <= 0f)
            {
                invencible = false;

                // Restaurar propiedades normales despu�s de la invencibilidad
            }
        }
    }
    public void ActivarInvencibilidad(float duracion)
    {
        invencible = true;
        duracionInvencibilidad = duracion;

        // Realizar acciones espec�ficas al activar la invencibilidad
    }
    private void GanarJuego()
    {
      
       

        datosjuegos.Puntuacion = Mathf.FloorToInt(puntuacion);
        datosjuegos.Ganado = true;   
        SceneManager.LoadScene("FinNivel");
    }
    private void ActivarPowerUpVelocidad(float duracion)
    {
        velocidad *= 2; // Por ejemplo, duplicar la velocidad actual

        // Restaurar la velocidad original despu�s de la duraci�n del power-up
        StartCoroutine(RestaurarVelocidadOriginal(duracion));
    }

    private IEnumerator RestaurarVelocidadOriginal(float duracion)
    {
        yield return new WaitForSeconds(duracion);

        // Restaurar la velocidad original
        velocidad = Mathf.RoundToInt(velocidadOriginal);
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

        datosjuegos.Puntuacion = Mathf.FloorToInt(puntuacion);

        SceneManager.LoadScene("FinNivel");
    }
    public void IncrementrarPuntos(int cantidad)
    {
        puntuacion += cantidad;

    }
    public void QuitarVida()
    {
        if (vulnerable )
        {
            invulnerable = true;
            audiosource.PlayOneShot(vidasSfx);
            vulnerable = false;
            numVidas--;
            hud.SetVidasTxt(numVidas);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);


            if (numVidas == 0)
            {
                // Reiniciar la escena
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                // TODO: Cargar nueva escena
                FinJuego();
            }
            else
            {
                Invoke("HacerVulnerable", 1f);
                sprite.color = Color.red;
                audiosource.PlayOneShot(vidasSfx);
            }
        }
        else if (!vulnerable)
        {
            audiosource.PlayOneShot(vidasSfx);
            sprite.color = Color.green; // Otra forma de indicar que el jugador es invencible
        }
    }
    private  void HacerVulnerable()
    {
        vulnerable= true;
        sprite.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)

    {
        
        if (collision.CompareTag("meta"))
        {
            if(puntuacion<=0)
            {
             FinJuego();
            }
            else 
            {

                GanarJuego();
            }
        }

        else if (collision.CompareTag("Muerto"))
        {
          
            transform.position = posicionInicial;

            QuitarVida();
        }
        else if (collision.CompareTag("PowerUpVelocidad"))
        {
            ActivarPowerUpVelocidad(10f);

            
            Destroy(collision.gameObject);
        }


    }
    public bool EsInvulnerable()
    {
        return invulnerable;
    }



}

