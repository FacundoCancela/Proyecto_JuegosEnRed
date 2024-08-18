using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SalaManager : MonoBehaviour
{
    public static SalaManager instance;

    public TMP_InputField inputFieldTMP;
    private List<string> codigosDeSala = new List<string>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        AsignarBotones();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
            AsignarBotones();
        }
    }

    private void AsignarBotones()
    {
        // Busca los botones por nombre en la escena actual
        Button crearSalaButton = GameObject.Find("CREAR")?.GetComponent<Button>();
        Button unirseASalaButton = GameObject.Find("UNIRSE")?.GetComponent<Button>();

        if (crearSalaButton != null)
        {
            crearSalaButton.onClick.AddListener(CrearSala);
        }

        if (unirseASalaButton != null)
        {
            unirseASalaButton.onClick.AddListener(UnirseASala);
        }

        // Asigna también el inputField por su nombre
        inputFieldTMP = GameObject.Find("InputFieldCodigoSala")?.GetComponent<TMP_InputField>();
    }

    public void CrearSala()
    {
        string codigo = inputFieldTMP.text;
        codigosDeSala.Add(codigo);
        SceneManager.LoadScene("FirstLevel");
    }

    public void UnirseASala()
    {
        string codigo = inputFieldTMP.text;
        if (codigosDeSala.Contains(codigo))
        {
            SceneManager.LoadScene("FirstLevel");
        }
        else
        {
            Debug.Log("La sala no existe");
        }
    }
}
