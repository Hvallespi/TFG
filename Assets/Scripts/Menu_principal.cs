using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu_principal : MonoBehaviour
{
    public string nivelCargar = "Nivel1";

   public void empezarPartida()
    {
        SceneManager.LoadScene(nivelCargar);
    }

    public void salirJuego()
    {
        Debug.Log("Saliendo");
        Application.Quit();
    }

    public void ajustesJuego()
    {
        Debug.Log("Ajustes");
    }
}
