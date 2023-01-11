using TMPro;
using UnityEngine;

public class EstadisticasJugador : MonoBehaviour
{
    public static int piedra;
    public int piedraInicial = 200;

    public TextMeshProUGUI dineroTexto;

    private void Start()
    {
        piedra = piedraInicial;
    }

    private void Update()
    {
        dineroTexto.SetText(piedra.ToString());
    }

}
