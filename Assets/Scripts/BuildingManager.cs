using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{

    public GameObject[] torretas;
    private GameObject objetoPendiente;
    private Torreta torreta;
    private bool puedeConstruir = true;

    private Vector3 pos;
   // private RaycastHit RaycastHit;
   // private Vector3 worldPosition;

    [SerializeField] private LayerMask mascaraCapa;

    public void setPuedeCons(bool puedeConstruir)
    {
        this.puedeConstruir = puedeConstruir;
    }

    void Update()
    {
        if (objetoPendiente != null)
        {
            objetoPendiente.transform.position = new Vector2(
                redondearCuadricula(pos.x),
                redondearCuadricula(pos.y));

            if (Input.GetMouseButtonDown(0) && puedeConstruir)
            {
               colocarObjeto();
            }
        }
    }

    public void colocarObjeto()
    {
        torreta.colocada = true;
        torreta = null;
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

        /* Vector3 mousePos = Input.mousePosition;
         mousePos.z = Camera.main.nearClipPlane;
         worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if (Physics.Raycast(ray, out RaycastHit, 1000))
         {
             Debug.Log(RaycastHit.transform.name);
             Debug.Log("hit");
         

        //Debug.Log(Physics.Raycast(ray, out RaycastHit, 1000, mascaraCapa));

        if (Physics.Raycast(ray, out RaycastHit, 1000, mascaraCapa))
         {
             pos = RaycastHit.point;
         }*/
    }

    public void seleccionarObjeto(int index)
    {
        objetoPendiente = Instantiate(torretas[index], pos, transform.rotation);
        torreta = objetoPendiente.GetComponent<Torreta>();
        torreta.colocada = false;
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
