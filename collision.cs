using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class collision : MonoBehaviour
{

    public bool door1look;
    public bool door2look;

    // Use this for initialization
    void Start()
    {
        door1look = false;
        door2look = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.name == "Door1_Collider")
        {
            door1look = true;

        }

        if (other.name == "Door2_Collider")
        {
            door2look = true;
        }


    }



    void OnTriggerExit(Collider other)
    {

        if (other.name == "Door1_Collider")
        {
            door1look = false;
        }

        if (other.name == "Door2_Collider")
        {
            door2look = false;
        }

    }


}