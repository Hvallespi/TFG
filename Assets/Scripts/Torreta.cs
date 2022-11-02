using UnityEngine;

public class Torreta : MonoBehaviour
{
    private Transform objetivo; //El objetivo actual de la torreta

    [Header("Atributos")]

    public float rangoTorreta = 2.25f; //Rango efectivo de la torreta
    public float velAtaque = 1f; //A mas alto mas rapido atacara
    public float ajusteRotacion = 90; // 
    private float cuentaAtrasDisparo = 1f; //El valor inicial determina cuanto tardara en atacar la torreta una vez haya sido colocada
    [HideInInspector] public bool colocada = false;
    

    [Header("Unity Setups")]

    public string tagEnemigos = "Enemigo"; //A que puede apuntar la torreta

    public Transform aRotar; //Que parte de la torreta rota
   // public float velRotacion = 5f; desmarcar esto si se quiere que la torreta rote de manera mas natural

    public GameObject balaPrefab; //Que proyectil disparara la torreta
    private BuildingManager construccionManager; //Clase encargada de la construccion de la torreta en el mundo
    public Transform puntoDisparo; //Desde donde se generara el proyectil

    [Header("Animaciones")]

    public Animator animacion; // Que animaciones tendra la torreta

    void Start()
    {
        InvokeRepeating("actualizarObjetivo", 0f, 0.25f);
        construccionManager = FindObjectOfType<BuildingManager>();
    }

    void actualizarObjetivo()
    {
        GameObject[] enemigos = GameObject.FindGameObjectsWithTag(tagEnemigos);
        float distanciaCorta = Mathf.Infinity;
        GameObject enemigoCercano = null;

        foreach (GameObject enemigo in enemigos)
        {
            float distanciaEnemigo = Vector2.Distance(transform.position, enemigo.transform.position);
            if (distanciaEnemigo < distanciaCorta)
            {
                distanciaCorta = distanciaEnemigo;
                enemigoCercano = enemigo;
            }
        }

        if (enemigoCercano != null && distanciaCorta <= rangoTorreta)
        {
            objetivo = enemigoCercano.transform;
        }
        else
        {
            objetivo = null;
        }

    }
   
    void Update()
    {
        if (colocada == false)
            return;

        cuentaAtrasDisparo -= Time.deltaTime; //Aunque la torreta no este fijando un objetivo, su contador de ataque sigue avanzando, esto le permite tener un ataque listo para cuando un enemigo entre en rango

        if (objetivo == null)
            return;

        Vector3 direccion = objetivo.position - transform.position;
        Vector3 vectorDirRotado = Quaternion.Euler(0, 0, ajusteRotacion) * direccion;
        Quaternion rotacionMira = Quaternion.LookRotation(forward:Vector3.forward, upwards:vectorDirRotado); 
        //Descomentar la siguiente linea y ambiar aRotar.rotation = rotacionMira por aRotar.rotation = rotacion para un giro no tan espontaneo, pero las balas a veces saldran raras
        //Quaternion rotacion = Quaternion.Lerp(aRotar.rotation, rotacionMira, Time.deltaTime * velRotacion);
        aRotar.rotation = rotacionMira; 

        if (cuentaAtrasDisparo <= 0)
        {
            Disparar();
            cuentaAtrasDisparo = 1 / velAtaque;
            
        }

        
    }

    private void OnDrawGizmosSelected() //No tiene efecto alguno en la jugabilidad, pero en el modo inspector al seleccionar la torreta se puede ver su rango de ataque
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoTorreta);
    }

    void Disparar()
    {
        animacion.SetTrigger("Disparar");
        GameObject balaGO = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Bala bala = balaGO.GetComponent<Bala>();

        if (bala != null)
        {
            bala.buscarObjetivo(objetivo);
        }
    }

    void OnCollisionStay2D(Collision2D col)
    { //Esto puede reventar violentamente el rendimiento, si ocurre, cambiar por OnCollisionEnter, pero puede dar problemas
        if (col.gameObject.layer == 7 || col.gameObject.layer == 6)
        {
            construccionManager.setPuedeCons(false);
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == 7 || col.gameObject.layer == 6)
        {
            construccionManager.setPuedeCons(true);
        } 
    }
}
