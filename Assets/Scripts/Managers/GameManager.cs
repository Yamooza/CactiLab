using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public GameObject Player;

    // Singleton instanssi tästä manager luokasta
    public static GameManager Instance { get; private set; }
    public bool dontDestroyOnLoad = true;

    void Awake()
    {
        // JOS GameManageria ei ole vielä luotu
        // Asetetaan instanssiksi tämä objekti, muuten tuhotaan tämä objekti
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        // Jos true, niin asetetaan tämä objekti DontDestroyOnLoad scenelle
        // TL;DR Tämä objekti säilyy vaikka scenejä vaihdetaan
        if (dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    private void OnApplicationQuit()
    {
        // Ennen kuin peli suljetaan voidaan tehdä jotain tiettyjä asioita
        // esimerkiksi tallentaan pelin tiedot, pelaajan tiedot, kentän tiedot yms.
    }
}
