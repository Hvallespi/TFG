using UnityEngine;
using TMPro;
using System.Collections;

public class GeneradorOleadas : MonoBehaviour
{
    public Transform enemigoPrefab;
    public Transform [] spawners;

    public float tiempoOleadas = 5f;
    private float cuentaAtras = 2f;
    private int indexOleada = 0;
    public int numeroOleadasTotal = 5;

    public TextMeshProUGUI tiempoOleadaTXT;
    public TextMeshProUGUI indexOleadaTXT;

    private void Update()
    {

        if (cuentaAtras <=0f && indexOleada < numeroOleadasTotal)
        {
            StartCoroutine(generarOleada());
            cuentaAtras = tiempoOleadas;
        }

        tiempoOleadaTXT.SetText("Oleada en: " + Mathf.Round(cuentaAtras).ToString()); ;
        indexOleadaTXT.SetText("Oleada Nº " + indexOleada.ToString());

        if (indexOleada == numeroOleadasTotal && GameObject.FindGameObjectsWithTag("Enemigo").Length == 0)
        {
            Menu_Ingame.victoria = true;
        }
        else
        {
            if (indexOleada != numeroOleadasTotal) //Necesario para que el contador no se vaya a los numeros negativos mientras se termina de limpiar la ultima oleadas
            {
                cuentaAtras -= Time.deltaTime;
            }
            else
            {
                tiempoOleadaTXT.SetText("Oleada final");
            }
        }

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
