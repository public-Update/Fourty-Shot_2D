using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;
using TMPro;

public class StartServer : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_Text Text_Players;
    [SerializeField] GameObject buttonStart;
    PhotonView view;
    public static event Action onStart; 

    private void Start() {
        view = GetComponent<PhotonView>();
        UpdateTextPlayers();
        if (PhotonNetwork.IsMasterClient) buttonStart.SetActive(true);
    } 

    public override void OnPlayerEnteredRoom (Photon.Realtime.Player newPlayer) {
        UpdateTextPlayers();
    }

    [PunRPC]
    void UpdateTextPlayers() {
      Text_Players.text = PhotonNetwork.CurrentRoom.PlayerCount + "/4";
    }

    public void CallStartGame() {
        view.RPC("StartGame", RpcTarget.All);
    }

    [PunRPC]
    void StartGame() {
        if (PhotonNetwork.IsMasterClient) onStart.Invoke();
        Destroy(gameObject);
    }

}
