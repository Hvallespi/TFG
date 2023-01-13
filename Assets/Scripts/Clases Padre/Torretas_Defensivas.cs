using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torretas_Defensivas : Estructuras
{
    [Header("Tipo de estructura")]
    public Torretas torreta;

    [Header("Unity Setups Torretas")]
    public AudioSource sonidoDisparo;
    public GameObject balaPrefab; //Que proyectil disparara la torreta
    public Transform puntoDisparo; //Desde donde se generara el proyectil
    public string tagEnemigos = "Enemigo"; //A que puede apuntar la torreta
    protected GameObject objetivo; //El objetivo actual de la torreta
    public Animator animacion; // Que animaciones tendra la torreta

    private float cuentaAtras;
    public override void Start()
    {
        base.Start();

        setCosteTorreta(torreta.coste);
        vida = torreta.vidaInicial;
        cuentaAtras = torreta.cuentaAtrasDisparo;

        InvokeRepeating("actualizarObjetivo", 0f, 0.25f); //Cada 0.25 segundos la torreta busca un objetivo nuevo

    }

    public override void Update()
    {
        base.Update();

        if (colocada == false) //Si no esta colocada no queremos que haga nada
            return;

        hitboxJug.enabled = true;

        barraVida.fillAmount = vida / torreta.vidaInicial;
       
        cuentaAtras -= Time.deltaTime; //Aunque la torreta no este fijando un objetivo, su contador de ataque sigue avanzando, esto le permite tener un ataque listo para cuando un enemigo entre en rango

        if (vida <= 0)
        {
            destruir();
            return;
        }

        if (objetivo != null)//Si no hay objetivo no hace falta que rote ni dispare
        {
            comprobarDisparo();
        } 
        
    }

    protected void comprobarDisparo()
    {
        if (cuentaAtras <= 0) //Cuando llegue la cuenta atras a 0 la torreta disparara
        {
            Disparar();
            cuentaAtras = 1 / torreta.velAtaque; //Y reseteamos la cuenta atras, esto es menor que al inicio, pero asi queda como un tiempo en el cual la torreta tiene que "prepararse" para dispararw
        }
    }

    protected void actualizarObjetivo()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(tagEnemigos); //La torreta busca todos los objetos con la tag "enemigo"
        float distanciaCorta = Mathf.Infinity; //En un inicio no hay distancia mas corta asi que es infinita
        GameObject enemigoCercano = null; //Tampoco hay un enemigo mas cercado

        foreach (GameObject enemigo in enemigos) //Por cada enemigo que hay en el mapa
        {
            float distanciaEnemigo = Vector2.Distance(transform.position, enemigo.transform.position); //Toma su posicion
            if (distanciaEnemigo < distanciaCorta) //Y saca el enemigo mas cercano
            {
                distanciaCorta = distanciaEnemigo;
                enemigoCercano = enemigo;
            }
        }

        if (enemigoCercano != null && distanciaCorta <= torreta.rangoTorreta) //Si dicho enemigo se encuentra en el rango de la torreta
        {
            objetivo = enemigoCercano; //Pasara a ser su objetivo
        }
        else
        {
            objetivo = null; //en caso contrario ya no lo sera mas
        }

    }

    protected void Disparar()
    {
        animacion.SetTrigger("Disparar"); //Al disparar se ejecuta la animacion
        GameObject balaGO = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation); //Se instancia un proyectil que esta apuntando al objetivo
        Bala bala = balaGO.GetComponent<Bala>(); //Guardamos el componente en una variable

        sonidoDisparo.Play();

        if (bala != null) //Para luego darle un objetivo
        {
            bala.buscarObjetivo(objetivo);
        }
    }

    

}
