using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.SlotRacer;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] PhotonView pv;
    private int count = 0;

    [SerializeField] public List<GameObject> coins;
    [SerializeField] public int coinsToWin;

    [SerializeField] public int lifes;
    [SerializeField] private List<GameObject> hearts;

    [SerializeField] public TextMeshProUGUI text;
    [SerializeField] public Canvas winScreen;
    [SerializeField] public Canvas loseScreen;

    [SerializeField] public int actualLevel;

    public bool isGamePaused = false;

    [SerializeField] public bool hasRope;


    private void Awake()
    {
        pv = GetComponent<PhotonView>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }


    }

    private void Start()
    {
        UpdateCoinText(count);
        coinsToWin = coins.Count;
        UpdateHearts();
    }

    public void GetRope()
    {
        hasRope = true;
    }
    public void LoseRope()
    {
        hasRope = false;
    }

    public void LoseHealth()
    {
        pv.RPC("LoseHealthRPC", RpcTarget.AllBuffered);
    }


    [PunRPC]
    public void LoseHealthRPC()
    {
        lifes--;
        UpdateHearts();
        CheckLose();
    }

    [PunRPC]
    public void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (i < lifes)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }



    [PunRPC]
    public void CollectCoin()
    {
        count++;
        coinsToWin--;
        UpdateCoinText(count);
    }

    private void UpdateCoinText(int updatedCount)
    {
        text.text = "Monedas: " + updatedCount;
    }

    public void CheckWin()
    {
        if (coinsToWin <= 0)
        {
            pv.RPC("Win", RpcTarget.AllBuffered);
            if(actualLevel == LevelDataManager.Instance.levelData.maxLevelReached)
            {
                LevelDataManager.Instance.NextLevel();
            }
        }
        else
        {
            Debug.Log("te faltan monedas");
        }
    }

    public void CheckLose()
    {
        if(lifes <= 0)
        {
            pv.RPC("Lose", RpcTarget.AllBuffered);
        }
        else Debug.Log("vidas restantes: " + lifes);
    }


    [PunRPC]
    private void Win()
    {
        Debug.Log("ganaste");
        isGamePaused = true;
        winScreen.gameObject.SetActive(true);
    }

    [PunRPC]
    private void Lose()
    {
        Debug.Log("perdiste");
        isGamePaused = true;
        loseScreen.gameObject.SetActive(true);
    }


}