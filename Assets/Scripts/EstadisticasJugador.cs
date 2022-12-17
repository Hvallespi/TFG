using TMPro;
using UnityEngine;

public class EstadisticasJugador : MonoBehaviour
{
    public static int dinero;
    public int dineroInicial = 200;

    public TextMeshProUGUI dineroTexto;

    private void Start()
    {
        dinero = dineroInicial;
    }

    private void Update()
    {
        dineroTexto.SetText(dinero.ToString() + " $");
    }

}
