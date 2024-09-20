using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Control : MonoBehaviour, IControl
{
    [SerializeField] private IActor _playerModel;
    [SerializeField] private IView _playerView;
    [SerializeField] PhotonView pv;

    private void Awake()
    {
        _playerModel = GetComponent<IActor>();
        _playerView = GetComponent<IView>();
        pv = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if(pv.IsMine)
        {
            GetInputs();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pv.IsMine && collision.transform.CompareTag("Coin"))
        {
            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("CollectCoin", RpcTarget.AllBuffered, collision.gameObject.GetComponent<PhotonView>().ViewID);
        }

        if(pv.IsMine && collision.transform.CompareTag("WinDoor"))
        {
            GameManager.Instance.CheckWin();
        }
    }

    public void GetInputs()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            _playerModel.Jump();
        }

        float x = Input.GetAxis("Horizontal");
        Vector2 dir = new Vector2(x, 0).normalized;
        _playerModel.Move(dir);
    }

    [PunRPC]
    void CollectCoin(int coinViewID)
    {
        GameManager.Instance.CollectCoin();

        PhotonView coinPhotonView = PhotonView.Find(coinViewID);
        if (coinPhotonView != null)
        {
            
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(coinPhotonView.gameObject);
            }
        }
    }

}
