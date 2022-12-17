using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_pausa : MonoBehaviour
{

    public GameObject UI_Pausa;


    private void Update()
    {
        if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
        {
            activarPausa();
        }
    }

    public void activarPausa()
    {
        UI_Pausa.SetActive(!UI_Pausa.activeSelf);

        if (UI_Pausa.activeSelf)
        {
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void reiniciar()
    {
        activarPausa();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void menu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

}
