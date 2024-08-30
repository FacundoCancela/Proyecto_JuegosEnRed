using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.Demo.SlotRacer;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI text;
    public static GameManager Instance;

    [SerializeField] PhotonView pv;
    private int count = 0;

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
    }


    [PunRPC]
    public void CollectCoin()
    {

        count++;
        Debug.Log("monedas:" + count);
        UpdateCoinText(count);
    }

    private void UpdateCoinText(int updatedCount)
    {
        text.text = "Monedas: " + updatedCount;
    }
}
