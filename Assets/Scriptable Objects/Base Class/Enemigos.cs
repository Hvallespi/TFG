using UnityEngine.UI;
using Pathfinding;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Enemigo", menuName = "Enemigo")]
public class Enemigos : ScriptableObject
{
    [Header("Atributos")]
    public float velocidad;
    public float vidaInicial;
   
    public float daño;
    public float velAtaque;

}
