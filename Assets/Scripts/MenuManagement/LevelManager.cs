using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private List<Button> levelButtons;
    [SerializeField] private List<string> levelNames;

    private void Awake()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            int index = i;
            levelButtons[i].onClick.AddListener(() => JoinLevel(index));
        }
    }

    private void OnDestroy()
    {
        foreach (var button in levelButtons)
        {
            button.onClick.RemoveAllListeners();
        }

    }

    private void JoinLevel(int index)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("LoadLevel", RpcTarget.All, levelNames[index]);
        }
    }

    [PunRPC]
    private void LoadLevel(string levelName)
    {
        PhotonNetwork.LoadLevel(levelName);
    }

}
