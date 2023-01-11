using UnityEngine.UI;
using UnityEngine;

public class Recolectadora : Estructuras
{
    [Header("Tipo de estructura")]
    public Recolectadoras recolectadora;
    public Image barraProgreso;

    private float cuentaAtras;

    public override void Start()
    {
        base.Start();

        setCosteTorreta(recolectadora.coste);
        vida = recolectadora.vidaInicial;
        cuentaAtras = 0;
    }

    public override void Update()
    {
        base.Update();

        if (colocada == false) //Si no esta colocada no queremos que haga nada
            return;

        hitboxJug.enabled = true;

        barraVida.fillAmount = vida / recolectadora.vidaInicial;
        barraProgreso.fillAmount = cuentaAtras/recolectadora.velRecoleccion;

        cuentaAtras += Time.deltaTime;

        if (cuentaAtras >= recolectadora.velRecoleccion)
        {
            EstadisticasJugador.piedra += recolectadora.NumRecursosRecolectados;
            cuentaAtras = 0;
        }
    }

    public override void OnTriggerStay2D(Collider2D col) //En caso de que colisione (y siga haciendolo) con un lugar en el que no se puede construir (osease, esta encima) se lo comunicara al building manager
                                                        //de manera que no permitira construir la torreta
    { //Esto puede reventar violentamente el rendimiento, si ocurre, cambiar por OnTriggerEnter, pero puede dar problemas

        switch (col.gameObject.layer)
        {
            case 0:
            case 6:
            case 7:
            case 8:
            case 10:

                if (colocada == false) //El colocada es necesario ya que si no da un bug que hace que cuando un enemigo esta atacando una construccion no se pueden colocar mas
                {
                    construccionManager.setPuedeCons(false);
                    cambiarColor(colorEstructura, new Color(255f, 0f, 0f, 190f));
                }
                break;
        }

    }
    public override void OnTriggerExit2D(Collider2D col) //En caso de salir de las zonas prohibidas la opcion de poder construir vuelve a estar disponible
    {

        switch (col.gameObject.layer)
        {
            case 0:
            case 6:
            case 7:
            case 8:
            case 10:

                if (colocada == false)
                {
                    construccionManager.setPuedeCons(true);
                    cambiarColor(colorEstructura, new Color(0f, 255f, 0f, 190f));
                }

                break;
        }
    }

}
