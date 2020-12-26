using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MiningTrigger : MonoBehaviour
{
    public GameObject velocityBool;
    public GameObject goodJobUI;
    public GameObject Crystal1;
    public GameObject Crystal2;
    public GameObject Crystal3;
    public GameObject Crystal4;

    public Transform CrystalSpawn1;
    public Transform CrystalSpawn2;
    public Transform CrystalSpawn3;
    public Transform CrystalSpawn4;



    int alreadyTriggered;
    
    void Start()
    {
        alreadyTriggered = 0;
        goodJobUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if (velocityBool.tag == "True" && alreadyTriggered == 0){
            spawnCrystals();
        }
    }

    void spawnCrystals(){
        Instantiate(Crystal1, CrystalSpawn1.position, CrystalSpawn1.rotation);
        Instantiate(Crystal2, CrystalSpawn2.position, CrystalSpawn2.rotation);
        Instantiate(Crystal3, CrystalSpawn3.position, CrystalSpawn3.rotation);
        Instantiate(Crystal4, CrystalSpawn4.position, CrystalSpawn4.rotation);
        velocityBool.tag = "False";
        alreadyTriggered = 1;
    }

}
