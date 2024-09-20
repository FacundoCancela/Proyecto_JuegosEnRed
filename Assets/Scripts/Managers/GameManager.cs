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

    [SerializeField] public TextMeshProUGUI text;
    [SerializeField] public Canvas winScreen;

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

    [PunRPC]
    private void Win()
    {
        Debug.Log("ganaste");
        winScreen.gameObject.SetActive(true);
    }

}