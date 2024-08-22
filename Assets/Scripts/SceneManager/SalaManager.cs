using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SalaManager : MonoBehaviour
{
    public static SalaManager instance;
    [SerializeField] GameObject _gameplayCanvas;
    [SerializeField] GameObject _menuCanvas;
    [SerializeField] GameObject _instructionsCanvas;

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
        _gameplayCanvas.SetActive(false);
        _menuCanvas.SetActive(true);
        _instructionsCanvas.SetActive(false);

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

        // Asigna tambi�n el inputField por su nombre
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

    public void GoMenu()
    {
        if(_gameplayCanvas != null)
        {
            _gameplayCanvas.SetActive(false);
            _menuCanvas.SetActive(true);
        }
        if(_instructionsCanvas != null)
        {
            _instructionsCanvas.SetActive(false);
            _menuCanvas.SetActive(true);
        }
    }
    
    #region MainMenu_Methods
    public void Play()
    {
        _menuCanvas.SetActive(false);
        _gameplayCanvas.SetActive(true);
    }

    public void Instrutions()
    {
        _menuCanvas.SetActive(false);
        _instructionsCanvas.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
    #endregion
}
