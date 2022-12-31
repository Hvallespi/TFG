using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muros : Estructuras
{
    [Header("Tipo de estructura")]
    public Estructura muro;

    public override void Start()
    {
        base.Start();

        setCosteTorreta(muro.coste);
        vida = muro.vidaInicial;

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if (colocada == false) //Si no esta colocada no queremos que haga nada
            return;

        hitboxJug.enabled = true;

        barraVida.fillAmount = vida / muro.vidaInicial;
    }
}
