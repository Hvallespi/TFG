using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala_Ricochet : Bala
{
    public int numeroRebotes = 2;
    public string tagEnemigos = "Enemigo"; //A que puede apuntar la torreta
    private HashSet<GameObject> enemigosDañados = new HashSet<GameObject>(); //Hashset es como un array al cual poder añadir elementos y luego comprobarlos, la diferencia es que se usa cuando no hay que tener en cuenta el orden de los objetos dentro

    private void Start()
    {
        enemigosDañados.Add(objetivo);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void impactoObjetivo()
    {
        objetivo.GetComponent<Enemigo>().recibirDaño(daño);

        if (numeroRebotes > 0 && objetivo)
        {
            actualizarObjetivo();
            
        }
        else
        {
            objetivo = null;
        }
        
    }

    protected void actualizarObjetivo()
    {
        numeroRebotes--;

        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(tagEnemigos); //La torreta busca todos los objetos con la tag "enemigo"
        float distanciaCorta = Mathf.Infinity; //En un inicio no hay distancia mas corta asi que es infinita
        GameObject enemigoCercano = null; //Tampoco hay un enemigo mas cercado

        foreach (GameObject enemigo in enemigos) //Por cada enemigo que hay en el mapa
        {
            float distanciaEnemigo = Vector2.Distance(transform.position, enemigo.transform.position); //Toma su posicion

            if (distanciaEnemigo < distanciaCorta && !enemigosDañados.Contains(enemigo)) //Y saca el enemigo mas cercano
            {
                distanciaCorta = distanciaEnemigo;
                enemigoCercano = enemigo;
                enemigosDañados.Add(enemigo);
            }


        }

        if (enemigoCercano != null && distanciaCorta <= 2) //Si dicho enemigo se encuentra en el rango de la torreta
        {
            objetivo = enemigoCercano; //Pasara a ser su objetivo
 
        }
        else
        {
            objetivo = null; //en caso contrario ya no lo sera mas
        }

    }
}
