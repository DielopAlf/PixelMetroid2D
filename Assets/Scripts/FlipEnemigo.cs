using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipEnemigo : MonoBehaviour
{

    private SpriteRenderer sprite;
    private float posicionXAnterior;
   
   private void Start()
   {
        posicionXAnterior = transform.parent.position.x;
        sprite = GetComponent<SpriteRenderer>();
   }

    
    private void Update()
    {
       
        sprite.flipX =posicionXAnterior < transform.position.x;
        
        posicionXAnterior = transform.position.x;    
    }
}
