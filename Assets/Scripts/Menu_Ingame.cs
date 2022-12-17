using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Ingame : MonoBehaviour
{

    public GameObject UI_Pausa;
    public GameObject UI_GameOver;
    public GameObject UI_Victoria;

    [HideInInspector] public static bool vivo = true;
    [HideInInspector] public static bool victoria = false;

    private void Start()
    {
        Time.timeScale = 1f;
        UI_GameOver.SetActive(false);
        UI_Victoria.SetActive(false);
        vivo = true;
        victoria = false;
    }

    private void Update()
    {
        if (vivo == false)
        {
            Time.timeScale = 0f;
            UI_GameOver.SetActive(true);
        }

        if (victoria == true)
        {
            Time.timeScale = 0f;
            UI_Victoria.SetActive(true);
        }

        if (Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape))
        {
            activarPausa();
        }
    }

    public void activarPausa()
    {
        if (vivo == true)
        {
            UI_Pausa.SetActive(!UI_Pausa.activeSelf);
        }

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
