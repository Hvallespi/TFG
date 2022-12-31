using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballesta : Torretas_Defensivas
{
  
    [Header("Unity Setup Ballesta")]
    
    public Transform aRotar; //Que parte de la torreta rota
    public int ajusteRotacion = 0;
    
    public override void Update()
    {
        base.Update();

        if (objetivo && colocada)
        {
            Vector3 direccion = objetivo.transform.position - transform.position;
            Vector3 vectorDirRotado = Quaternion.Euler(0, 0, ajusteRotacion) * direccion;
            Quaternion rotacionMira = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorDirRotado);

            aRotar.rotation = rotacionMira;
        }

        
    }
}
