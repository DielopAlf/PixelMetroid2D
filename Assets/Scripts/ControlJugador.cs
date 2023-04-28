using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlJugador : MonoBehaviour
{

    public int velocidad;

    public int fuerzaSalto;

    private Rigidbody2D fisica;

    private void Start()
    {
        fisica = GetComponent<Rigidbody2D>();
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

        }
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
}
