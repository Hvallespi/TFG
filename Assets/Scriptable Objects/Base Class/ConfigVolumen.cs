using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConfigVolumen", menuName = "ConfigVolumen")]
public class ConfigVolumen : ScriptableObject
{
    [Header("Nivel Volumen")]
    public float volumen;
}
