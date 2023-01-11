using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class botones_ingame : MonoBehaviour
{
    public Button boton;
    public Estructura objetoEnTienda;

    // Update is called once per frame
    void Update()
    {
        if (EstadisticasJugador.piedra < objetoEnTienda.coste)
        {
            boton.interactable = false;
        }
        else
        {
            boton.interactable = true;
        }
    }
}
