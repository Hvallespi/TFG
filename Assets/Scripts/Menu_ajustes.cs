using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class Menu_ajustes : MonoBehaviour
{
    public TextMeshProUGUI volumenTXT;
    public TextMeshProUGUI resolucionTXT;

    public ConfigVolumen configuracionVol;
    private float volumenEnTexto;
    public AudioMixer audioMixer;

    Resolution[] resoluciones;
    private int indexResoluciones;
    private Resolution resolucionActual;

    private void Start()
    {
  
        resoluciones = Screen.resolutions;
        int contador = 0;
        foreach (var res in resoluciones)
        {
            
            if (res.width == Screen.currentResolution.width &&
                res.height == Screen.currentResolution.height)
            {
                indexResoluciones = contador;
                resolucionActual = res;
            }
            contador++;
        }
    }

    //Cabe destacar que volumenEnTexto es una tirita sobre un problema que es usar la funcion logaritmica (que es como hay que hacerlo correctamente para que el volumen
    //se adapte adecuadamente) si el volumen es 0 dicha funcion no se aplica el cambio del volumen. Normalmente esto se arregla usando un slider y poniendo su valor minimo a 0.0001 (parecido a lo que he hecho)
    //No obstante por mucho buscar no he encontrado ningun slider que se adapte adecuadamente a la estetica de la interfaz que estoy usando, asi que por ende he tenido que sacar esta solucion (aunque sea algo mas chapucera)
    void Update()
    {
        if (configuracionVol.volumen < 5)
        {
            volumenEnTexto = 0;
        }
        else
        {
            volumenEnTexto = configuracionVol.volumen;
        }

        volumenTXT.SetText(volumenEnTexto.ToString()    );
        resolucionTXT.SetText(resolucionActual.width + "x" + resolucionActual.height);
        audioMixer.SetFloat("Master", Mathf.Log10(configuracionVol.volumen/100)*20);
    }

    public void establecerVolumen(int i)
    {
        if (i > 0 )
        {
            if (configuracionVol.volumen < 5)
            {
                configuracionVol.volumen = 5;

            }
            else if (configuracionVol.volumen < 100)
            {
                configuracionVol.volumen += 5;
            }
        }
        else
        {
            if (configuracionVol.volumen > 0)
            {
                configuracionVol.volumen -= 5;
            }

            if (configuracionVol.volumen == 0)
            {
                configuracionVol.volumen = 0.0001f;
            }
        }
    }

    public void pantallaCompleta(bool completa)
    {
        Screen.fullScreen = completa;
    }

    public void resolucion(int i)
    {
        if (indexResoluciones > 0 && i < 0)
        {
            indexResoluciones--;
        }

        if (indexResoluciones < resoluciones.Length-1 && i > 0)
        {
            indexResoluciones++;
        }

        resolucionActual = resoluciones[indexResoluciones];
    }

    public void establecerResolucion()
    {
        Screen.SetResolution(resolucionActual.width, resolucionActual.height,Screen.fullScreen);
    }

}
