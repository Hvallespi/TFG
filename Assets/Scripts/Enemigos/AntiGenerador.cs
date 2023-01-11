using UnityEngine.UI;
using Pathfinding;
using UnityEngine;

public class AntiGenerador : Enemigo
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
        GameObject[] recolectadoras = GameObject.FindGameObjectsWithTag("Recolectadora");

        if (recolectadoras.Length != 0)
        {
            float distanciaCorta = Mathf.Infinity; //En un inicio no hay distancia mas corta asi que es infinita
            GameObject recolectadoraCercana = null; //Tampoco hay una recolectadora mas cercana

            foreach (GameObject recolectadora in recolectadoras) //Por cada recolectadora que hay en el mapa
            {
                float distanciaRecolectadora = Vector2.Distance(transform.position, recolectadora.transform.position); //Toma su posicion
                if (distanciaRecolectadora < distanciaCorta && recolectadora.GetComponent<Recolectadora>().colocada == true) //Y saca la recolectadora colocada mas cercana
                {
                    distanciaCorta = distanciaRecolectadora;
                    recolectadoraCercana = recolectadora;
                }
            }

            if (recolectadoraCercana != null) // Si hay una colocada 
            {
                gameObject.GetComponent<AIDestinationSetter>().target = recolectadoraCercana.transform; //la tomara como objetivo
            }
            
        }
        else //Si no hay recolectadoras o se esta colocando el objetivo sera el jugador
        {
            gameObject.GetComponent<AIDestinationSetter>().target = GameObject.FindWithTag("Player").transform;
        }
    }

}
