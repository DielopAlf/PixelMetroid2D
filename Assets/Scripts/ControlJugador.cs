using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(Input.GetKeyDown(KeyCode.Space))
        {

            fisica.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

        }
    }
}
