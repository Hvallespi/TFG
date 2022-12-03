using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovJugador : MonoBehaviour
{

    public float velMov = 4f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movimiento;
    Vector2 ultimoMovimiento;

    void Update()
    {
        movimiento.x = Input.GetAxisRaw("Horizontal");
        movimiento.y = Input.GetAxisRaw("Vertical");

        if (movimiento.x == 0 && movimiento.y == 0)
        { }
        else { 
            ultimoMovimiento.x = movimiento.x;
            ultimoMovimiento.y = movimiento.y;
        }

        animator.SetFloat("UltMovX", ultimoMovimiento.x);
        animator.SetFloat("UltMovY", ultimoMovimiento.y);
        animator.SetFloat("Horizontal", movimiento.x);
        animator.SetFloat("Vertical", movimiento.y);
        animator.SetFloat("Velocidad", movimiento.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movimiento.normalized * velMov * Time.fixedDeltaTime);
    }
}
