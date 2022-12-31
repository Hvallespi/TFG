using UnityEngine;

public class BuildingManager : MonoBehaviour
{

    public Texture2D[] cursores;

    public GameObject[] construcciones; //Array que contendra todos los tipos de torreta

    private GameObject uiTienda;

    private GameObject objetoPendiente; //Objeto que se va a construir
    private bool puedeConstruir = true; //Por defecto se asume que si se puede construir la torreta
    private bool modoDestruir = false;

    private Vector3 pos;
    private Estructuras estructuraActual;

    public void setPuedeCons(bool puedeConstruir) // Setter que se usa en el codigo de la torreta para transmitir si se puede construir o no
    {
        this.puedeConstruir = puedeConstruir;
    }

    public void cambiarConstruccion(Estructuras estructuraActual)
    {
        this.estructuraActual = estructuraActual;
        Debug.Log(this.estructuraActual);
    }

    private void Start()
    {
        Cursor.SetCursor(cursores[0], Vector2.zero, CursorMode.Auto);
        uiTienda = GameObject.FindGameObjectWithTag("UI_Ingame");
    }

    void Update()
    {

       if (modoDestruir == true)
        {
            if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
            {
                RaycastHit2D impacto = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (impacto.collider != null)
                {
                    if (impacto.collider.tag == "Construcciones")
                    {
                        EstadisticasJugador.dinero += (int)(impacto.transform.gameObject.GetComponent<Estructuras>().getCosteTorreta() * 0.65); //Al destruir se devuelve un 65% del precio original
                        Destroy(impacto.transform.gameObject);
                    };
                }

                
            }
        }

        if (objetoPendiente != null) //Nada de esto ocurre si no hay un objeto para construir, asi se ahorra memoria
        {
            objetoPendiente.transform.position = new Vector2( //Se ajusta la posicion de la torreta a la cuadricula
                redondearCuadricula(pos.x),
                redondearCuadricula(pos.y));

            if (Input.GetMouseButtonDown(0) && puedeConstruir && Time.timeScale != 0) // Si el lugar de construccion es apto, al hacer click se pondra la torreta
            {
               colocarObjeto();
            }

            if (Input.GetMouseButtonDown(1) && Time.timeScale != 0) //La accion de construir se cancela con un click derecho
            {
               cancelarColocar();
            }
        }
        else 
        {
            if (Input.GetKeyDown("x") && Time.timeScale != 0)
            {
                Cursor.SetCursor(cursores[1], Vector2.zero, CursorMode.Auto);
                modoDestruir = true;
                uiTienda.GetComponent<CanvasGroup>().interactable = false;
                uiTienda.GetComponent<CanvasGroup>().alpha = 0;
                
            }

            if (Input.GetKeyDown("c") && Time.timeScale != 0)
            {
                Cursor.SetCursor(cursores[0], Vector2.zero, CursorMode.Auto);
                modoDestruir = false;
                uiTienda.GetComponent<CanvasGroup>().interactable = true;
                uiTienda.GetComponent<CanvasGroup>().alpha = 1;
            }
        }
        
    }

    public void colocarObjeto()
    {
        estructuraActual.setColocada(true); //Al estar colocada la torreta ya puede empezar a disparar
        AstarPath.active.Scan(); //Al colocar el objeto se realiza un analisis del mapa para que el pathfinding la tenga en cuenta a la hora de hacer los calculos
        EstadisticasJugador.dinero -= estructuraActual.getCosteTorreta(); //Se cobra el coste de la torreta
      
        estructuraActual = null; // Se limpian las variables para estar listas en el caso de que se coloque una nueva torreta
        objetoPendiente = null;
    }

    public void cancelarColocar()
    {
        Destroy(objetoPendiente);
        estructuraActual = null;
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
        objetoPendiente = Instantiate(construcciones[index], pos, transform.rotation);
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
