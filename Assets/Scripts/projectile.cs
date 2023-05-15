using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour

{
    Rigidbody2D rigidbody2d;
    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    
    void Update()
    {
        if(transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnCollisionEnter2D(Collision2D other)
    {
        ControlEnemigo e = other.collider.GetComponent<ControlEnemigo>();
        if (e != null)
        {
             Destroy(gameObject);
        }
    
        Destroy(gameObject);
    }
}