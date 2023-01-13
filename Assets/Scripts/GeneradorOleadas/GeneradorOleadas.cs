using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GeneradorOleadas : MonoBehaviour
{
    public Transform enemigoPrefab;
    public Transform [] spawners;


    public List<Rondas> ronda = new List<Rondas>();

    public float tiempoOleadas = 5f;
    public float tiempoPreparacionInicial;
    private float cuentaAtras = 3f;
    private int indexOleada = 0;

    public TextMeshProUGUI tiempoOleadaTXT;
    public TextMeshProUGUI indexOleadaTXT;

    private void Start()
    {
        cuentaAtras = tiempoPreparacionInicial;
    }

    private void Update()
    {

        if (cuentaAtras <=0f && indexOleada < ronda.Count)
        {
            StartCoroutine(generarOleada());
            cuentaAtras = tiempoOleadas;
        }

        tiempoOleadaTXT.SetText("Oleada en: " + Mathf.Round(cuentaAtras).ToString()); ;
        indexOleadaTXT.SetText("Oleada Nº " + indexOleada.ToString());

        if (indexOleada == ronda.Count && GameObject.FindGameObjectsWithTag("Enemigo").Length == 0)
        {
            Menu_Ingame.victoria = true;
        }
        else
        {
            if (indexOleada != ronda.Count) //Necesario para que el contador no se vaya a los numeros negativos mientras se termina de limpiar la ultima oleadas
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
        for (int j = 0; j < ronda[indexOleada].enemigosEnRonda.Count; j++) //Se generaran los enemigos asignados a la ronda
        {
            foreach (var spawn in spawners) //En todos los generadores
            {
                generarEnemigo(spawn, ronda[indexOleada].enemigosEnRonda[j]);
            }
            yield return new WaitForSeconds(ronda[indexOleada].enemigosEnRonda[j].tiempo);
        }

        indexOleada++;

    }

    void generarEnemigo(Transform spawn, EnemigoAGenerar enemigo)
    {
        Instantiate(enemigo.enemigoGenerado, spawn.position, spawn.rotation);
    }

}
