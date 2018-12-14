using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour {

    public bool inRange;


	// Use this for initialization
	void Start () {

        inRange = false;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            inRange = true;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            inRange = false;
           

        }
    }
}
