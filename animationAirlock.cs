using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationAirlock : MonoBehaviour {

    public float dist = 0;
    public bool airlockActivated = false;
    public bool playerInside = false;
    public bool door1Look = false;
    public bool door2Look = false;
    public bool triggerDoor1 = false;
    public bool triggerDoor2 = false;


    public GameObject Player;
    public GameObject airlockCollider;

    public Animation door1Open;
    public GameObject doorBackground1;
    public float debugdoor1;
    public Animation door2Open;
    public GameObject doorBackground2;
    public Material doorActive;
    public Material doorPassive;
    public float debugdoor2;

    public GameObject cameraCollider;
    public GameObject sandStorm;

    public GameObject pressA;
    public GameObject airlockText;

    public GameObject exterior;
    public GameObject interior;

    // Use this for initialization
    void Start () {


        exterior.SetActive(true);
        interior.SetActive(false);

        door2Open["Take 001"].time = 0.0f;
        door2Open["Take 001"].speed = 0.0f; 
        door2Open.Play();  // Starts Door 2 in Closed Position
        door1Open["Take 001"].time = door1Open["Take 001"].length;
        door1Open["Take 001"].speed = 0.0f;
        door1Open.Play();  // Starts Door 1 in Open Position

        pressA.SetActive(false);
        airlockText.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

        debugdoor1 = door1Open["Take 001"].time;
        debugdoor2 = door2Open["Take 001"].time;

        door1Look = cameraCollider.GetComponent<collision>().door1look;
        door2Look = cameraCollider.GetComponent<collision>().door2look;

        float airlockDist = Vector3.Distance(Player.transform.position, airlockCollider.transform.position);

        // Determines if character is in air lock

        if (airlockDist < dist)
        {
            airlockActivated = true;
        }
        else
        {
            airlockActivated = false;            
        }

        // Resets the door material

        if (door1Look == false)
        {
            doorBackground1.GetComponent<MeshRenderer>().material = doorPassive;
        }

        if (door2Look == false)
        {
            doorBackground2.GetComponent<MeshRenderer>().material = doorPassive;
        }

       //  Resets the press A text

        if (door1Look == false)
        {
            pressA.SetActive(false);
        }

        if (door2Look == false)
        {
            pressA.SetActive(false);
        }

        // Activates airlock animation if within range, visible, and press A

        if (airlockActivated == true && playerInside == false)
        {
            if (door2Look == true)
            {
                pressA.SetActive(true);//Display Press A in canvas
                doorBackground2.GetComponent<MeshRenderer>().material = doorActive;

                //if (OVRInput.GetUp(OVRInput.Button.One))
                if (Input.GetMouseButtonUp(0))
                {
                    triggerDoor2 = true;
                    pressA.SetActive(false);
                }
            }
        }

        if (triggerDoor2 == true)
        {
            airlockText.SetActive(true);
            door1Open["Take 001"].speed = -1.0f;  // close door 1
            door1Open.Play();

            if (door1Open["Take 001"].time <= 0.0f)
            {
                airlockText.SetActive(false);
                door2Open["Take 001"].speed = 1.0f;// Open door 2
                door2Open.Play();
            }

            if (door2Open["Take 001"].time >= 1.0f) 
            {
                triggerDoor2 = false;
                playerInside = true;
                sandStorm.SetActive(false);
                exterior.SetActive(false);
                interior.SetActive(true);
            }
        }
            
        if (airlockDist < dist && playerInside == true)
        {

            if (door1Look == true)
            {
                pressA.SetActive(true);  //Display press A in canvas
                doorBackground1.GetComponent<MeshRenderer>().material = doorActive;

                //if (OVRInput.GetUp(OVRInput.Button.One))
                if (Input.GetMouseButtonUp(0))
                {
                    triggerDoor1 = true;
                    pressA.SetActive(false);
                }
            } 

        }     
        
        if (triggerDoor1 == true)
        {
            airlockText.SetActive(true);
            door2Open["Take 001"].speed = -1.0f;
            door2Open.Play();

            if (door2Open["Take 001"].time <= 0.0f)
            {
                airlockText.SetActive(false);
                door1Open["Take 001"].speed = 1.0f;
                door1Open.Play();
            }

            triggerDoor1 = false;
            playerInside = false;
            exterior.SetActive(true);
            interior.SetActive(false);
        }
    }
}
