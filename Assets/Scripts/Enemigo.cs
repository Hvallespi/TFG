using UnityEngine;

public class Enemigo : MonoBehaviour
{

    public float velocidad = 2f;
    public float vida = 10;

    private Transform objetivo;
    private int indexPuntosControl = 0;

    void Start()
    {
        objetivo = PuntosControl.puntos[0];
    }

    void Update()
    {

        if (vida <= 0)
        {
            morir();
            return;
        }

        Vector2 direccion = objetivo.position - transform.position;
        transform.Translate(direccion.normalized * velocidad * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, objetivo.position) <= 0.02f)
        {
            siguientePunto();
        }

    }

    void siguientePunto(){

        if (indexPuntosControl >=PuntosControl.puntos.Length -1)
        {
            Destroy(gameObject);
            return;
        }

        indexPuntosControl++;
        objetivo = PuntosControl.puntos[indexPuntosControl];

    }

    public void recibirDaño(float daño)
    {
        vida = vida - daño;
    }

    void morir()
    {
        Destroy(gameObject);
    }
}