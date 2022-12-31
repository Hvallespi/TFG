using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Nueva Torreta", menuName ="Estructuras/Torreta")]
public class Torretas : Estructura
{
    [Header("Atributos Especificos")]

    public float rangoTorreta; //Rango efectivo de la torreta
    public float velAtaque; //A mas alto mas rapido atacara
    public float ajusteRotacion; // 
    public float cuentaAtrasDisparo; //El valor inicial determina cuanto tardara en atacar la torreta una vez haya sido colocada

}
