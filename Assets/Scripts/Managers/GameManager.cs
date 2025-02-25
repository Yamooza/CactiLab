using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public GameObject Player;

    // Singleton instanssi t�st� manager luokasta
    public static GameManager Instance { get; private set; }
    public bool dontDestroyOnLoad = true;

    void Awake()
    {
        // JOS GameManageria ei ole viel� luotu
        // Asetetaan instanssiksi t�m� objekti, muuten tuhotaan t�m� objekti
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        // Jos true, niin asetetaan t�m� objekti DontDestroyOnLoad scenelle
        // TL;DR T�m� objekti s�ilyy vaikka scenej� vaihdetaan
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        // Ennen kuin peli suljetaan voidaan tehd� jotain tiettyj� asioita
        // esimerkiksi tallentaan pelin tiedot, pelaajan tiedot, kent�n tiedot yms.
    }
}
