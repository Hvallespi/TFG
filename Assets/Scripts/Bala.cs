using UnityEngine;

public class Bala : MonoBehaviour
{
    private Transform objetivo;

    public float velocidad = 10f; //Velocidad del proyectil
    public float daño = 2f; // Daño del proyectil

    public void buscarObjetivo (Transform objetivo) //Es el equivalente a un setter de objetivo, pero me aclaraba mas con este nombre
    {
        this.objetivo = objetivo;
    }

    void Update()
    {
        if (objetivo == null) //En caso de que el objetivo desaparezca (ya sea por que muere o por que llega a la salida) se destruye el proyectil que iba a por el
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direccion = objetivo.position - transform.position;  //La direccion es la posicion actual de la flecha restada a la posicion actual del objetivo

        //La flecha siempre mirara hacia el objetivo, dandole una sensacion de que lo rastrea (Que al fin y al cabo es lo que hace)
        Vector3 vectorDirRotado = Quaternion.Euler(0, 0, 0) * direccion;
        Quaternion rotacionMira = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorDirRotado);
        transform.rotation = rotacionMira;

        float distanciaEsteFrame = velocidad * Time.deltaTime; //Distancia recorrida por la flecha en este frame

        if (direccion.magnitude <= distanciaEsteFrame) //Si la magnitud de la direccion es menor que la distancia recorrida significa que la flecha ha alcanzado a su objetivo
        {
            impactoObjetivo();
            return;
        }

        transform.Translate(direccion.normalized * distanciaEsteFrame, Space.World); //En caso de aun no impactar simplemente se movera

    }

    void impactoObjetivo() //Cuando impacta con el objetivo el enemigo recibe daño y el objeto se destruye
    {
        objetivo.GetComponent<Enemigo>().recibirDaño(daño);
        Destroy(gameObject);
    }

}
