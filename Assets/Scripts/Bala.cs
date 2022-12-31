using UnityEngine;

public class Bala : MonoBehaviour
{
    protected GameObject objetivo;

    public float velocidad = 10f; //Velocidad del proyectil
    public float daño = 2f; // Daño del proyectil

    public void buscarObjetivo (GameObject objetivo) //Es el equivalente a un setter de objetivo, pero me aclaraba mas con este nombre
    {
        this.objetivo = objetivo;
    }

    protected virtual void Update()
    {
        if (objetivo == null) //En caso de que el objetivo desaparezca (ya sea por que muere o por que llega a la salida) se destruye el proyectil que iba a por el
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direccion = objetivo.transform.position - transform.position;  //La direccion es la posicion actual de la flecha restada a la posicion actual del objetivo

        //La flecha siempre mirara hacia el objetivo, dandole una sensacion de que lo rastrea (Que al fin y al cabo es lo que hace)
        Vector3 vectorDirRotado = Quaternion.Euler(0, 0, 0) * direccion;
        Quaternion rotacionMira = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorDirRotado);
        transform.rotation = rotacionMira;

        float distanciaEsteFrame = velocidad * Time.deltaTime; //Distancia recorrida por la flecha en este frame

        transform.Translate(direccion.normalized * distanciaEsteFrame, Space.World); //En caso de aun no impactar simplemente se movera

    }

    protected virtual void impactoObjetivo() //Cuando impacta con el objetivo el enemigo recibe daño y el objeto se destruye
    {
        objetivo.GetComponent<Enemigo>().recibirDaño(daño);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == 8 && col.transform == objetivo.transform)
        {
            impactoObjetivo();
        }
    }

}
