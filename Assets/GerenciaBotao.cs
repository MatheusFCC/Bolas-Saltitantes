using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GerenciaBotao : MonoBehaviour
{

    private bool isPaused = false;
    public Toggle crescerToggle;
    private void Start()
    {
        // Carrega o valor salvo do toggle ao iniciar a cena
        if (PlayerPrefs.HasKey("CrescerToggle"))
        {
            crescerToggle.isOn = PlayerPrefs.GetInt("CrescerToggle") == 1;
        }
    }
    

    // Método para pausar e despausar o jogo
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0; // Pausa o jogo
            Debug.Log("Jogo pausado");
        }
        else
        {
            Time.timeScale = 1; // Retoma o jogo
            Debug.Log("Jogo retomado");
        }
    }

    // Método para resetar o jogo
    public void ResetGame()
    {
        Time.timeScale = 1; // Garante que o jogo esteja em velocidade normal
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarrega a cena atual
        Debug.Log("Jogo resetado");
    }

    public void alteraCrescer()
    {
        PlayerPrefs.SetInt("CrescerToggle", crescerToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
