using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class Enemigo : MonoBehaviour
{

    [Header("Atributos")]
    public Enemigos enemigo;
    protected float vida;

    [Header("Animaciones")]
    public Animator animator;

    [Header("Unity Setups")]
    public AIPath aiPath;
    public Image barraVida;
    public AudioSource sonidoMuerte;

    private float cuentaAtrasAtaque = 1f;
    private bool muerto = false;

    public virtual void Start()
    {
        vida = enemigo.vidaInicial;
        gameObject.GetComponent<AIDestinationSetter>().target = GameObject.FindWithTag("Player").transform;
        aiPath.maxSpeed = enemigo.velocidad;
    }

    public virtual void Update()
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
        barraVida.fillAmount = vida / enemigo.vidaInicial;
    }

    void morir()
    {
        aiPath.canMove = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().enabled = false;

        transform.gameObject.tag = "Untagged"; //El objeto tarda un poco en destruirse para que se pueda ver la animacion de muerte, por eso al cambiarle el tag las torretas ya no seleccionaran a un enemigo muerto

        //AudioSource.PlayClipAtPoint(sonidoMuerte, transform.position);
        sonidoMuerte.Play();

        animator.SetTrigger("Muerto");
        Destroy(gameObject,0.7f);
        
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == 6 && col.GetComponent<Estructuras>().colocada == true && muerto == false)
        {
            aiPath.canMove = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            cuentaAtrasAtaque -= Time.deltaTime;
            if (cuentaAtrasAtaque <= 0)
            {
                Atacar(col.transform);
                cuentaAtrasAtaque = 1 / enemigo.velAtaque; 
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 6 && muerto == false)
        {
            aiPath.canMove = true;
            cuentaAtrasAtaque = 1f;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionEnter2D(Collision2D col) //Si llegan a tocar al jugador se declara el fin del juego
    {
        if (col.gameObject.layer == 9 && muerto == false)
        {
            Menu_Ingame.vivo = false;
            Destroy(gameObject);
        }
    }

    void Atacar(Transform objetivo)
    {
        objetivo.GetComponent<Estructuras>().recibirDaño(enemigo.daño);
    }
}