using System;
using System.Globalization;
using System.Threading;
using UnityEngine;
using UnityEngine.Serialization;
using NetworkAPI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
  //variables made public to allow editing in Unity editor
  public bool alive = true;
  NetworkComm networkComm;
  public float speed = 5;
  [SerializeField] Rigidbody rb1;
  public Rigidbody rb2;
  [SerializeField] float horizontalMultiplier = 2;
  public float speedIncreasePerPoint = 0.1f;
  float horizontalInput;

  [SerializeField] float jumpForce = 400f;
  [SerializeField] LayerMask groundMask;

  public GameObject localPlayer, remotePlayer;
  public Vector3 localPlayerPos = new Vector3();
  public Vector3 remotePlayerPos;

  private void FixedUpdate()
  {
    if (!alive) return; // if alive is not true, stop running the function
    Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
    Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
    rb1.MovePosition(rb1.position + forwardMove + horizontalMove);
    localPlayerPos = rb1.position;
    networkComm.sendMessage("ID=1;" + localPlayerPos.x + "," + localPlayerPos.y + "," + localPlayerPos.z);
  }

  // Start is called before the first frame update
  void Start()
  {
    networkComm = new NetworkComm();
    networkComm.MsgReceived += new NetworkComm.MsgHandler(processMsg);
    (new Thread(new ThreadStart(networkComm.ReceiveMessages))).Start();
    // (new Thread(new ThreadStart(threadfunc))).Start();
    remotePlayer = GameObject.Find("Player2");
    localPlayer = GameObject.Find("Player1");
    remotePlayer.transform.position = remotePlayerPos;
    localPlayer.transform.position = localPlayerPos;
  }

  // Update is called once per frame
  void Update()
  {
    horizontalInput = Input.GetAxis("Horizontal");

    if(Input.GetKeyDown(KeyCode.Space)) {
      Jump();
    }

    // logic for if player falls off the ground strip
    if (transform.position.y < -5) // if player has fallen off the platform
    {
      Die();
    }

    // rb2.position = remotePlayerPos;
    remotePlayer.transform.position = remotePlayerPos;
    Console.WriteLine("Updated remotePlayerPos: " + remotePlayerPos);
  }

  public void Die()
  {
    alive = false;
    // restart the game
    SceneManager.LoadScene(SceneManager.GetActiveScene()
      .name); // will load the scene we are currently in/game will restart
  }

  public void processMsg(string message)
  {
    string[] msgParts = message.Split(";");
    if (msgParts[0].Contains("ID=2"))
    {
      string[] coordinates = msgParts[1].Split(",");
      Debug.Log(message);
      float x = float.Parse(coordinates[0], CultureInfo.InvariantCulture.NumberFormat);
      float y = float.Parse(coordinates[1], CultureInfo.InvariantCulture.NumberFormat);
      float z = float.Parse(coordinates[2], CultureInfo.InvariantCulture.NumberFormat);
      remotePlayerPos.x = x;
      remotePlayerPos.y = y;
      remotePlayerPos.z = z;
    }
  }

    void Jump() {
    // check whether we are currently grounded
    float height = GetComponent<Collider>().bounds.size.y;
    bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

    // if we are, jump
    rb1.AddForce(Vector3.up * jumpForce);
  }
}