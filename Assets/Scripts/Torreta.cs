using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Torreta : MonoBehaviour
{
    private Transform objetivo; //El objetivo actual de la torreta

    [Header("Atributos")]

    public float rangoTorreta = 2.25f; //Rango efectivo de la torreta
    public float velAtaque = 1f; //A mas alto mas rapido atacara
    public float ajusteRotacion = 90; // 
    public float vidaInicial = 4f;
    private float vida;
    public int coste = 100;
    public bool puedeAtacar = true;
    private float cuentaAtrasDisparo = 1f; //El valor inicial determina cuanto tardara en atacar la torreta una vez haya sido colocada
    public Image barraVida;
    [HideInInspector] public bool colocada = false;

    [Header("Unity Setups")]

    public string tagEnemigos = "Enemigo"; //A que puede apuntar la torreta
    public AudioClip sonidoDisparo;
    public Transform aRotar; //Que parte de la torreta rota
    //public float velRotacion = 5f; desmarcar esto si se quiere que la torreta rote de manera mas natural

    public BoxCollider2D hitboxJug;
    public GameObject balaPrefab; //Que proyectil disparara la torreta
    private BuildingManager construccionManager; //Clase encargada de la construccion de la torreta en el mundo
    private SpriteRenderer[] colorTorreta;
    public Transform puntoDisparo; //Desde donde se generara el proyectil

    [Header("Animaciones")]

    public Animator animacion; // Que animaciones tendra la torreta


    void Start()
    {
        vida = vidaInicial;

        InvokeRepeating("actualizarObjetivo", 0f, 0.25f); //Cada 0.25 segundos la torreta busca un objetivo nuevo
        construccionManager = FindObjectOfType<BuildingManager>();//Tambien inicializa el componente encargado de ver si la torreta se encuentra en un lugar adecuado 

        colorTorreta = GetComponentsInChildren<SpriteRenderer>();
        cambiarColor(colorTorreta, new Color(0f, 255f, 0f, 190f));

    }

    void actualizarObjetivo() 
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(tagEnemigos); //La torreta busca todos los objetos con la tag "enemigo"
        float distanciaCorta = Mathf.Infinity; //En un inicio no hay distancia mas corta asi que es infinita
        GameObject enemigoCercano = null; //Tampoco hay un enemigo mas cercado

        foreach (GameObject enemigo in enemigos) //Por cada enemigo que hay en el mapa
        {
            float distanciaEnemigo = Vector2.Distance(transform.position, enemigo.transform.position); //Toma su posicion
            if (distanciaEnemigo < distanciaCorta) //Y saca el enemigo mas cercano
            {
                distanciaCorta = distanciaEnemigo;
                enemigoCercano = enemigo;
            }
        }

        if (enemigoCercano != null && distanciaCorta <= rangoTorreta) //Si dicho enemigo se encuentra en el rango de la torreta
        {
            objetivo = enemigoCercano.transform; //Pasara a ser su objetivo
        }
        else
        {
            objetivo = null; //en caso contrario ya no lo sera mas
        }

    }
   
    void Update()
    {
        if (colocada == false) //Si no esta colocada no queremos que haga nada
            return;

        hitboxJug.enabled = true;

        barraVida.fillAmount = vida / vidaInicial;
        cambiarColor(colorTorreta, new Color(255f, 255f, 255f, 255f));
        cuentaAtrasDisparo -= Time.deltaTime; //Aunque la torreta no este fijando un objetivo, su contador de ataque sigue avanzando, esto le permite tener un ataque listo para cuando un enemigo entre en rango

        if (vida <= 0)
        {
            destruir();
            return;
        }

        if (puedeAtacar == false)
            return;

        if (objetivo == null) //Si no hay objetivo no hace falta que rote ni dispare
            return;

        Vector3 direccion = objetivo.position - transform.position;
        Vector3 vectorDirRotado = Quaternion.Euler(0, 0, ajusteRotacion) * direccion;
        Quaternion rotacionMira = Quaternion.LookRotation(forward:Vector3.forward, upwards:vectorDirRotado); 
        //Descomentar la siguiente linea y ambiar aRotar.rotation = rotacionMira por aRotar.rotation = rotacion para un giro no tan espontaneo, pero las balas a veces saldran raras
        //Quaternion rotacion = Quaternion.Lerp(aRotar.rotation, rotacionMira, Time.deltaTime * velRotacion);
        aRotar.rotation = rotacionMira; 

        if (cuentaAtrasDisparo <= 0) //Cuando llegue la cuenta atras a 0 la torreta disparara
        {
            Disparar();
            cuentaAtrasDisparo = 1 / velAtaque; //Y reseteamos la cuenta atras, esto es menor que al inicio, pero asi queda como un tiempo en el cual la torreta tiene que "prepararse" para dispararw
        }

      

    }

    private void OnDrawGizmosSelected() //No tiene efecto alguno en la jugabilidad, pero en el modo inspector al seleccionar la torreta se puede ver su rango de ataque
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoTorreta);
    }

    void Disparar()
    {
        animacion.SetTrigger("Disparar"); //Al disparar se ejecuta la animacion
        GameObject balaGO = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation); //Se instancia un proyectil que esta apuntando al objetivo
        Bala bala = balaGO.GetComponent<Bala>(); //Guardamos el componente en una variable

        AudioSource.PlayClipAtPoint(sonidoDisparo, transform.position);

        if (bala != null) //Para luego darle un objetivo
        {
            bala.buscarObjetivo(objetivo);
        }
    }

    void cambiarColor(SpriteRenderer[] sprites,Color color)
    {
        foreach (SpriteRenderer hijos in sprites)
        {
            hijos.color = color;
        }
    }

    public void recibirDaño(float daño)
    {
        vida -= daño;
        Debug.Log(vida);
    }

    public void destruir()
    {
        Destroy(gameObject);
    }

    void OnTriggerStay2D(Collider2D col) //En caso de que colisione (y siga haciendolo) con un lugar en el que no se puede construir (osease, esta encima) se lo comunicara al building manager
                                               //de manera que no permitira construir la torreta
    { //Esto puede reventar violentamente el rendimiento, si ocurre, cambiar por OnTriggerEnter, pero puede dar problemas

        switch (col.gameObject.layer)
        {
            case 6:
            case 7:
            case 8:
            case 10:

                if (colocada == false) //El colocada es necesario ya que si no da un bug que hace que cuando un enemigo esta atacando una construccion no se pueden colocar mas
                {
                    construccionManager.setPuedeCons(false);
                    cambiarColor(colorTorreta, new Color(255f, 0f, 0f, 190f));
                }
                break;
        }

    }
    void OnTriggerExit2D(Collider2D col) //En caso de salir de las zonas prohibidas la opcion de poder construir vuelve a estar disponible
    {

        switch (col.gameObject.layer)
        {
            case 6:
            case 7:
            case 8:
            case 10:
                construccionManager.setPuedeCons(true);
                cambiarColor(colorTorreta, new Color(0f, 255f, 0f, 190f));
                break;
        }
    }

}
