using UnityEngine;
using Photon.Pun;

public class ShootGun : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float DelayReload;
    [SerializeField] GameObject prefabBullet;
    bool Reloading;
    PhotonView view;
    Transform ShotPoint;
    
    [Header("Sounds")]
    [SerializeField] AudioClip gunshotSound;
    AudioSource Alayer;

    private void Start() {
        Alayer = GetComponent<AudioSource>();
        ShotPoint = transform.GetChild(0);
        view = GetComponent<PhotonView>();
    }

    public void Fire() {
        if(!Reloading) {
          view.RPC("SoundShoot", RpcTarget.All);
          PhotonNetwork.Instantiate(prefabBullet.name, ShotPoint.position, ShotPoint.rotation);
          Invoke("Reload", DelayReload);
          Reloading = true;
        } 
    }

    [PunRPC]
    private void SoundShoot() {
      Alayer.PlayOneShot(gunshotSound); 
    }

    private void Reload() {
       Reloading = false;
    }
}