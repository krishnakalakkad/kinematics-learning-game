using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMoveScript : MonoBehaviour
{

    public static bool GameIsPaused = false;

//-------------------------------------------------------------------------------------------
    //THIS SECTION IS FOR UI
    public Text velocityText; //displayed on the top right of the screen
    public Text heightText; //displayed right below velocityText
    public Text netForceText; //displayed in the pause menu
    
    public GameObject PauseUI; //The UI that lets you control the jetpack force (press esc)
    public GameObject GameOverUI; //The game over screen for when you hit the ground too hard
    
    public string jetString;
    public GameObject inputField;
    public GameObject velocityBool;
//-------------------------------------------------------------------------------------------
    public CharacterController controller;
    public Transform playerModel;
    public Transform groundSet;
    public Transform heightReference;

    public float speed = 12f;
    public float jetForce = 0f;
    float playerMass = 68.4f;
    public float netForce = 68.4f * -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public Transform velocityCheck;
    public float groundDistance = 0.4f;
    public float hitDistance = 1f;
    public LayerMask groundMask;
    public LayerMask hitMask;
    

    public Vector3 velocity;
    bool isGrounded;
    bool hitGround;

    
    void Start(){
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        GameIsPaused = true;
        heightReference = groundSet;
        PauseUI.SetActive(false);
        GameOverUI.SetActive(false);
        velocityBool.tag = "False";
    }
    
    // Update is called once per frame


    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        hitGround = Physics.CheckSphere(velocityCheck.position, hitDistance, hitMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y  = -2f;
            velocityText.text = (velocity.y + 2f).ToString("N2") + " m/s";

        } else{
            velocityText.text = (velocity.y).ToString("N2") + "m/s";
        }

        if (hitGround && (velocity.y > -17) && (velocity.y < -14.5f)){
            StartCoroutine(goodJob());
        }

        if (hitGround && (velocity.y < -17)){
            Cursor.lockState = CursorLockMode.None;
            GameOverUI.SetActive(true);
            Time.timeScale = 0f;
        }

        heightText.text = (playerModel.position.y - heightReference.position.y).ToString("N2") + " m";
        netForceText.text = "Net force: " + (netForce).ToString("N2") + " N";

        float x  = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded && !GameIsPaused){
            velocity.y = 7.6720f;
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            if (GameIsPaused){
                Resume();
            } else{
                Pause();
            }
        }
        if (Input.GetMouseButtonDown(0)){
        
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f)){
                if (hit.transform != null){
                    SetHeightReference(hit.transform);
                }
            }
        }

        velocity.y += (netForce/playerMass) * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    void Resume(){
        Cursor.lockState = CursorLockMode.Locked;
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause(){
        Cursor.lockState = CursorLockMode.None;
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ChangeGravity(){
        jetString = inputField.GetComponent<Text>().text;
        jetForce = float.Parse(jetString, CultureInfo.InvariantCulture.NumberFormat);
        netForce = jetForce;
        jumpHeight = (velocity.y * velocity.y) / (-2f * -9.81f);
    }

    public void turnOffJetpack(){
        netForce = -671.004f;
    }

    public IEnumerator goodJob(){
        velocityBool.tag = "True";
        yield return new WaitForSeconds(.6f);
        velocityBool.tag = "False"; 
    }

    private void SetHeightReference(Transform go){
        if (go.gameObject.tag == "Selectable"){
            heightReference = go;
        }
    }

    public void restart(){
        string scene = SceneManager.GetActiveScene().name;
        //Load it
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
