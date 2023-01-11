using UnityEngine.UI;
using UnityEngine;

public class Estructuras : MonoBehaviour
{

    [Header("Unity Setups Estructuras")]
    public Image barraVida;
    public BoxCollider2D hitboxJug;
    [HideInInspector]public bool colocada = false;
    protected BuildingManager construccionManager; //Clase encargada de la construccion de la torreta en el mundo
    protected SpriteRenderer[] colorEstructura;

    protected float vida;
    protected int costeTorreta;

    public virtual void Start()
    {
        construccionManager = FindObjectOfType<BuildingManager>();//Tambien inicializa el componente encargado de ver si la torreta se encuentra en un lugar adecuado 
        construccionManager.cambiarConstruccion(this);

        colorEstructura = GetComponentsInChildren<SpriteRenderer>();
        cambiarColor(colorEstructura, new Color(0f, 255f, 0f, 190f));
    }

    public virtual void Update()
    {
        if (vida <= 0)
        {
            destruir();
        }
    }

    public void setColocada(bool colocada)
    {
        cambiarColor(colorEstructura, new Color(255f, 255f, 255f, 255f));
        this.colocada = colocada;
    }   

    public void setCosteTorreta(int costeTorreta)
    {
        this.costeTorreta = costeTorreta;
    }

    public int getCosteTorreta()
    {
        return costeTorreta;
    }

    protected void cambiarColor(SpriteRenderer[] sprites, Color color)
    {
        foreach (SpriteRenderer hijos in sprites)
        {
            hijos.color = color;
        }
    }

    public void recibirDaño(float daño)
    {
        vida -= daño;
    }

    protected void destruir()
    {
        Destroy(gameObject);
    }

    public virtual void OnTriggerStay2D(Collider2D col) //En caso de que colisione (y siga haciendolo) con un lugar en el que no se puede construir (osease, esta encima) se lo comunicara al building manager
                                         //de manera que no permitira construir la torreta
    { //Esto puede reventar violentamente el rendimiento, si ocurre, cambiar por OnTriggerEnter, pero puede dar problemas

        switch (col.gameObject.layer)
        {
            case 6:
            case 7:
            case 8:
            case 10:
            case 12:

                if (colocada == false) //El colocada es necesario ya que si no da un bug que hace que cuando un enemigo esta atacando una construccion no se pueden colocar mas
                {
                    construccionManager.setPuedeCons(false);
                    cambiarColor(colorEstructura, new Color(255f, 0f, 0f, 190f));
                }
                break;
        }

    }
    public virtual void OnTriggerExit2D(Collider2D col) //En caso de salir de las zonas prohibidas la opcion de poder construir vuelve a estar disponible
    {

        switch (col.gameObject.layer)
        {
            case 6:
            case 7:
            case 8:
            case 10:
            case 12:

                if (colocada == false)
                {
                    construccionManager.setPuedeCons(true);
                    cambiarColor(colorEstructura, new Color(0f, 255f, 0f, 190f));
                }
                
                break;
        }
    }

}
