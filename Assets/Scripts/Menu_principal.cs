using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu_principal : MonoBehaviour
{
    public GameObject UI_MenuPrincipal;
    public GameObject UI_Ajustes;
    public string nivelCargar = "Nivel1";
    public Texture2D cursor;

    public void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

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
        UI_Ajustes.SetActive(true);
        UI_MenuPrincipal.SetActive(false);
    }

    public void volverAMenu()
    {
        UI_Ajustes.SetActive(false);
        UI_MenuPrincipal.SetActive(true);
    }
}
