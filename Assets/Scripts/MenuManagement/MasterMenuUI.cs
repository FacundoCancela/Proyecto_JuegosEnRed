using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject MasterUI;
    [SerializeField] private GameObject PlayerUI;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            MasterUI.gameObject.SetActive(true);
            PlayerUI.gameObject.SetActive(false);
        }
        else
        {
            MasterUI.gameObject.SetActive(false);
            PlayerUI.gameObject.SetActive(true);
        }
    }
}
