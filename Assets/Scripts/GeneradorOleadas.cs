using UnityEngine;
using System.Collections;

public class GeneradorOleadas : MonoBehaviour
{
    public Transform enemigoPrefab;
    public Transform [] spawners;

    public float tiempoOleadas = 5f;
    private float cuentaAtras = 2f;
    private int indexOleada = 0;

    private void Update()
    {

        if (cuentaAtras <=0f)
        {
            StartCoroutine(generarOleada());
            cuentaAtras = tiempoOleadas;
        }

        cuentaAtras -= Time.deltaTime;

    }

    IEnumerator generarOleada()
    {
        indexOleada++;

        foreach (var spawn in spawners)
        {
            Debug.Log("JOSEPUTO");
            for (int i = 0; i < indexOleada; i++)
            {
                generarEnemigo(spawn);
                yield return new WaitForSeconds(0.4f);
            }
        }

    }

    void generarEnemigo(Transform spawn)
    {
        Instantiate(enemigoPrefab, spawn.position, spawn.rotation);
    }

}
