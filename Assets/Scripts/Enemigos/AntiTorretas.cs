using UnityEngine.UI;
using Pathfinding;
using UnityEngine;

public class AntiTorretas : Enemigo
{
    

    // Start is called before the first frame update
    public override void Start()
    {
        vida = enemigo.vidaInicial;
        aiPath.maxSpeed = enemigo.velocidad;
        buscarObjetivo();
    }

    // Update is called once per frame
    public override void Update()
    {
        buscarObjetivo();
        base.Update();
        
    }

    private void buscarObjetivo()
    {
        GameObject[] torretas = GameObject.FindGameObjectsWithTag("Torretas");

        if (torretas.Length != 0)
        {
            float distanciaCorta = Mathf.Infinity; //En un inicio no hay distancia mas corta asi que es infinita
            GameObject torretaCercana = null; //Tampoco hay una torreta mas cercana

            foreach (GameObject torreta in torretas) //Por cada torreta que hay en el mapa
            {
                float distanciaTorreta = Vector2.Distance(transform.position, torreta.transform.position); //Toma su posicion
                if (distanciaTorreta < distanciaCorta && torreta.GetComponent<Torretas_Defensivas>().colocada == true) //Y saca la torreta colocada mas cercana
                {
                    distanciaCorta = distanciaTorreta;
                    torretaCercana = torreta;
                }
            }

            if (torretaCercana != null) // Si hay una colocada 
            {
                gameObject.GetComponent<AIDestinationSetter>().target = torretaCercana.transform; //la tomara como objetivo
            }
            
        }
        else //Si no hay recolectadoras o se esta colocando el objetivo sera el jugador
        {
            gameObject.GetComponent<AIDestinationSetter>().target = GameObject.FindWithTag("Player").transform;
        }
    }

}
