using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionPulser : MonoBehaviour {

    public float maxIntensity = 1f;
    public Light myLight;
    public float minIntensity = 0f;
    public float pulseSpeed = 1f; //here, a value of 0.5f would take 2 seconds and a value of 2f would take half a second
    private float targetIntensity = 1f;
    private float currentIntensity;

    Renderer mat;

	// Use this for initialization
	void Start () {
        mat = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {

        currentIntensity = Mathf.MoveTowards(myLight.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
        if (currentIntensity >= maxIntensity)
        {
            currentIntensity = maxIntensity;
            targetIntensity = minIntensity;
        }
        else if (currentIntensity <= minIntensity)
        {
            currentIntensity = minIntensity;
            targetIntensity = maxIntensity;
        }

        float G = currentIntensity;
        float R = currentIntensity;
        float B = currentIntensity;
        mat.material.SetColor("_EmissionColor", new Color(R, G, B));
    }
}
