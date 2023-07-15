using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyUI : MonoBehaviourPunCallbacks
{
    [Header("Fields")]
    [SerializeField] TMP_InputField JoinField;
    [SerializeField] TMP_InputField CreateField;
    [SerializeField] GameObject Text_Waiting;

    public void CreateRoom() {
       RoomOptions roomOptions = new RoomOptions();
       roomOptions.MaxPlayers = 4;
       PhotonNetwork.CreateRoom(CreateField.text, roomOptions);
    }

    public void JoinRoom() {
        PhotonNetwork.JoinRoom(JoinField.text);
    }

    public override void OnJoinedRoom() {
        PhotonNetwork.LoadLevel(2);
    }
    
}
