using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviourPunCallbacks
{
    public void ChangeScene()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();

    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
