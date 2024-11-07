using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banderas : MonoBehaviour
{
    public GameObject bridge;
    private bool playerEnBandera = false;

    void Update()
    {
        if (playerEnBandera && Input.GetKeyDown(KeyCode.F) && GameManager.Instance.hasRope)
        {
            ActivateBridge();
            GameManager.Instance.LoseRope();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine && collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            playerEnBandera = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PhotonView pv = collision.gameObject.GetComponent<PhotonView>();
        if (pv != null && pv.IsMine && collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Player2"))
        {
            playerEnBandera = false;
        }
    }

    void ActivateBridge()
    {
        bridge.SetActive(true);
    }
}
