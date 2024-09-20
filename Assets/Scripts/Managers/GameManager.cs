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

    public bool isGamePaused = false;


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

    public void LoseHealth()
    {
        lifes--;
        pv.RPC("UpdateHearts", RpcTarget.AllBuffered);
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
        Debug.Log("monedas:" + count);
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