using UnityEngine;

public class PuntosControl : MonoBehaviour
{
    public static Transform[] puntos;

    void Awake ()
    {
        puntos = new Transform[transform.childCount];
        for (int i = 0; i < puntos.Length; i++)
        {
           puntos[i] = transform.GetChild(i);
        }
    }



}