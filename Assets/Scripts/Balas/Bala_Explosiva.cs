using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala_Explosiva : Bala
{
    public float radioExplosion;
    public Animator animacion;
    public AudioSource sonidoExplosion;
    private bool impactado;

    protected override void Update()
    {
        if (impactado == false) //Cuando impacta no hace falta que la explosion siga al enemigo, si no que se queda en el sitio donde impacto
        {
            base.Update();
        }
        
    }

    protected override void impactoObjetivo()
    {
        sonidoExplosion.Play();
        impactado = true;

        Collider2D[] colisiones = Physics2D.OverlapCircleAll(transform.position, radioExplosion);
        foreach (Collider2D colision in colisiones)
        {
            if (colision.tag == "Enemigo")
            {
                colision.gameObject.GetComponent<Enemigo>().recibirDaño(daño);
            }
        }

        transform.localScale = new Vector3(radioExplosion * 6, radioExplosion * 6, 1); //El *6 es necesario para que la explosion tenga el mismo radio, esto lo he sacado a base de pruebas
        
        animacion.SetTrigger("Explotar"); //Al disparar se ejecuta la animacion

    }

    public void destruir()
    {
        Destroy(gameObject);
    }

}

