using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform heightReference;
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    [SerializeField] private string selectableTag = "Selectable";
 

    private Transform _selection;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        if (Input.GetMouseButtonDown(0)){
        
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f)){
                if (hit.transform != null){
                    SetHeightReference(hit.transform);
                }
            }
        }
    }

    private void SetHeightReference(Transform go){
        if (go.gameObject.tag == selectableTag){
            heightReference = go;
        }
    }


}
