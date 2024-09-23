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

    private void Start()
    {
        UpdateButtonInteractivity();
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
        int maxLevelReached = LevelDataManager.Instance.levelData.maxLevelReached - 1;

        if (index <= maxLevelReached)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                photonView.RPC("LoadLevel", RpcTarget.All, levelNames[index]);
            }
        }
        else
        {
            Debug.Log("No tienes acceso a este nivel. Desbloquea más niveles para acceder.");
        }
    }

    [PunRPC]
    private void LoadLevel(string levelName)
    {
        PhotonNetwork.LoadLevel(levelName);
    }

    private void UpdateButtonInteractivity()
    {
        int maxLevelReached = LevelDataManager.Instance.levelData.maxLevelReached - 1;

        for (int i = 0; i < levelButtons.Count; i++)
        {

            if (i > maxLevelReached)
            {
                levelButtons[i].interactable = false;
            }
        }
    }
}
