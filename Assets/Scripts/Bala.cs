using UnityEngine;

public class Bala : MonoBehaviour
{
    private Transform objetivo;

    public float velocidad = 10f;
    public float daño = 2f;

    public void buscarObjetivo (Transform _objetivo)
    {
        objetivo = _objetivo;
    }

    // Update is called once per frame
    void Update()
    {
        if (objetivo == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direccion = objetivo.position - transform.position;
        float distanciaEsteFrame = velocidad * Time.deltaTime;

        if (direccion.magnitude <= distanciaEsteFrame)
        {
            impactoObjetivo();
            return;
        }

        transform.Translate(direccion.normalized * distanciaEsteFrame, Space.World);

    }

    void impactoObjetivo()
    {
        objetivo.GetComponent<Enemigo>().recibirDaño(daño);
        Destroy(gameObject);
    }

}
