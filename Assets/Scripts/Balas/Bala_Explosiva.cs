using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala_Explosiva : Bala
{
    public float radioExplosion;
    public Animator animacion;

    protected override void impactoObjetivo()
    {
        //objetivo.GetComponent<Enemigo>().recibirDaño(daño);

        Collider2D[] colisiones = Physics2D.OverlapCircleAll(transform.position, radioExplosion);
        foreach (Collider2D colision in colisiones)
        {
            if (colision.tag == "Enemigo")
            {
                colision.gameObject.GetComponent<Enemigo>().recibirDaño(daño);
            }
        }

        transform.localScale = new Vector3(radioExplosion * 6, radioExplosion * 6, 1);
        animacion.SetTrigger("Explotar"); //Al disparar se ejecuta la animacion

    }

    public void destruir()
    {
        Destroy(gameObject);
    }

}

