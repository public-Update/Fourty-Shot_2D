using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ConnectedServer : MonoBehaviourPunCallbacks
{
    private void Start() {
        PhotonNetwork.ConnectUsingSettings();
        Application.targetFrameRate = 60;
    }

    public override void OnConnectedToMaster() {
        SceneManager.LoadScene(1);
    }
}
