using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Torreta", menuName = "Estructuras/Muro")]

public class Estructura : ScriptableObject
{
    [Header("Atributos generales")]
    public float vidaInicial;
    public int coste;
}
