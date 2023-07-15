using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Characteristic")]
    [SerializeField] float HP;
    public float Coins;
    public bool Died {get; private set;}
    public string _name;

    [Header("Objects")]
    [SerializeField] Joystick Joystick;
    [SerializeField] Slider HP_Amount;
    [SerializeField] GameObject Rip;
    [SerializeField] TMP_Text Text_CoinCount;
    [SerializeField] GameObject mainCanvas;
    Rigidbody2D body;
    Transform trWepaon, playerTransform;
    PhotonView view;
    Win Win;

    [Header("Move Settings")]
    [Range(0.1f,15)] [SerializeField] float speedMove;
    Vector2 moveDirection;
    Vector2 RotateVector;

    private void Start()
    {
      body = GetComponent<Rigidbody2D>();   
      playerTransform = transform;
      trWepaon = transform.GetChild(0);
      view = GetComponent<PhotonView>();
      Win = FindObjectOfType<Win>();
    }

    private void Update() {
      if(view.IsMine) {
      Move();
      Rotate();
      }
      else if(mainCanvas != null)Destroy(mainCanvas);
    }

    private void Move() {
      moveDirection = new Vector2(Joystick.Horizontal(), Joystick.Vertical());
      body.MovePosition(body.position + moveDirection * Time.fixedDeltaTime * speedMove);
    }

    private void Rotate() {
      if(Joystick.Horizontal() > 0) {
          transform.rotation = Quaternion.Euler(0,0,0);
          trWepaon.localPosition = new Vector3(-0.032f,-0.075f,-0.1f);
          RotateVector = (Vector3.left * Joystick.Vertical() + Vector3.up * Joystick.Horizontal());
      }
      else if(Joystick.Horizontal() < 0) {
          transform.rotation = Quaternion.Euler(0,180,0);
          trWepaon.localPosition = new Vector3(-0.032f,-0.075f,0.1f);
          RotateVector = (Vector3.left * Joystick.Vertical() - Vector3.up * Joystick.Horizontal());
      } 
      if (Joystick.Horizontal() != 0 || Joystick.Vertical() != 0) {
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, RotateVector);
        trWepaon.localRotation = Quaternion.RotateTowards(trWepaon.localRotation, toRotation , 600 * Time.deltaTime);
      }
    }

    public void UpdateCoinCounter() {
      Coins++;
      Text_CoinCount.text = Coins.ToString();
    }

    public void TakeDamage(int damage) {
      if(HP > 0) {
        HP -= damage;
        if(HP_Amount != null)HP_Amount.value = HP;
        if(HP <= 0) {
          gameObject.SetActive(false);
          Died = true;
          PhotonNetwork.Instantiate(Rip.name, transform.position, Quaternion.identity);
          Win.Check();
        }
      }
    }
}
