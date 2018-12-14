using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour {

    public CameraControl change;
    private Material m_Material;

	// Use this for initialization
	void Start () {
        m_Material = GetComponent<Renderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        if (change.interactPressed) {
            m_Material.color = Color.red;
            Debug.Log("Color Change");
        }

	}
}
