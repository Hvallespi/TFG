using UnityEngine;
using Pathfinding;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{

    public GameObject[] construcciones; //Array que contendra todos los tipos de torreta
    private GameObject objetoPendiente; //Objeto que se va a construir
    private Torreta construccion; // Objeto de la clase torreta para operar con el
    private bool puedeConstruir = true; //Por defecto se asume que si se puede construir la torreta

    private Vector3 pos;

    //[SerializeField] private LayerMask mascaraCapa;

    public void setPuedeCons(bool puedeConstruir) // Setter que se usa en el codigo de la torreta para transmitir si se puede construir o no
    {
        this.puedeConstruir = puedeConstruir;
    }

    void Update()
    {
        if (objetoPendiente != null) //Nada de esto ocurre si no hay un objeto para construir, asi se ahorra memoria
        {
            objetoPendiente.transform.position = new Vector2( //Se ajusta la posicion de la torreta a la cuadricula
                redondearCuadricula(pos.x),
                redondearCuadricula(pos.y));

            if (Input.GetMouseButtonDown(0) && puedeConstruir) // Si el lugar de construccion es apto, al hacer click se pondra la torreta
            {
               colocarObjeto();
            }

            if (Input.GetMouseButtonDown(1)) //La accion de construir se cancela con un click derecho
            {
               cancelarColocar();
            }
        }
    }

    public void colocarObjeto()
    {
        construccion.colocada = true; //Al estar colocada la torreta ya puede empezar a disparar
        AstarPath.active.Scan(); //Al colocar el objeto se realiza un analisis del mapa para que el pathfinding la tenga en cuenta a la hora de hacer los calculos
        EstadisticasJugador.dinero -= construccion.coste; //Se cobra el coste de la torreta
      
        construccion = null; // Se limpian las variables para estar listas en el caso de que se coloque una nueva torreta
        objetoPendiente = null;
    }

    public void cancelarColocar()
    {
        Destroy(objetoPendiente);
        construccion = null;
        objetoPendiente = null;
    }

    private void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        float ajuste = 0.4f;
        mousePosition.x -= ajuste;
        mousePosition.y -= ajuste;
        pos = mousePosition;

    }

    public void seleccionarObjeto(int index)
    {

        if (EstadisticasJugador.dinero < construcciones[index].GetComponent<Torreta>().coste) //Si hay menos dinero disponible del que cuesta la torreta salta un aviso de que no se puede comprar                                                                                //TODO: Mostrar en pantalla y ver si puedo modificar el boton para que no sea pulsable
        {
            Debug.Log("No hay suficiente dinero");
            return;
        }

        objetoPendiente = Instantiate(construcciones[index], pos, transform.rotation);
        construccion = objetoPendiente.GetComponent<Torreta>();
        construccion.colocada = false;
    }

    float redondearCuadricula(float pos)
    {
        float xDiff = pos % 1;
        pos -= xDiff;

        if (xDiff >(1/2))
        {
            pos += 1;
        }

        return pos;
    }

}
