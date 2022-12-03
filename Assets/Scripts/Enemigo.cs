using UnityEngine;
using Pathfinding;

public class Enemigo : MonoBehaviour
{

    public float velocidad = 2f;
    public float vida = 10f;
    public float daño = 1f;
    public float velAtaque = 0.5f;
    public int recompensa = 25;

    private float cuentaAtrasAtaque = 1f;

    public Animator animator;
    public AIPath aiPath;
    private bool muerto = false;

    void Start()
    {
        gameObject.GetComponent<AIDestinationSetter>().target = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (vida <= 0 && muerto == false)
        {
            muerto = true;
            morir();
            return;
        }

        animator.SetFloat("Horizontal", Mathf.Clamp(aiPath.desiredVelocity.x,-1, 1));
        animator.SetFloat("Vertical", Mathf.Clamp(aiPath.desiredVelocity.y, -1, 1));

    }


    public void recibirDaño(float daño)
    {
        vida -= daño;
    }

    void morir()
    {
        aiPath.canMove = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;
        transform.gameObject.tag = "Untagged"; //El objeto tarda un poco en destruirse para que se pueda ver la animacion de muerte, por eso al cambiarle el tag las torretas ya no seleccionaran a un enemigo muerto
        EstadisticasJugador.dinero += recompensa;
        animator.SetTrigger("Muerto");
        Destroy(gameObject,0.7f);
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == 6 && col.GetComponent<Torreta>().colocada == true)
        {
            aiPath.canMove = false;
            cuentaAtrasAtaque -= Time.deltaTime;
            if (cuentaAtrasAtaque <= 0)
            {
                Debug.Log("atacando");
                Atacar(col.transform);
                cuentaAtrasAtaque = 1 / velAtaque;
                
            }

        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 6)
        {
            aiPath.canMove = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void Atacar(Transform objetivo)
    {
        objetivo.GetComponent<Torreta>().recibirDaño(daño);
    }
}