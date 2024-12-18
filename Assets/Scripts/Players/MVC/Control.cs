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
        if (!pv.IsMine || GameManager.Instance.isGamePaused)
        {
            return;
        }

        GetInputs();
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
}
