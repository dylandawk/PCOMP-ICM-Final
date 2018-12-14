using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsVisibleTank : MonoBehaviour {

    Renderer m_Renderer;
    public bool isVis;

    // Use this for initialization
    void Start()
    {
        m_Renderer = GetComponent<Renderer>();
        isVis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Renderer.isVisible)
        {
            //Debug.Log("Object is visible");
            isVis = true;

        }
        else if (!m_Renderer.isVisible)
        {

            //Debug.Log("Object is no longer visible");
            isVis = false;

        }
    }
}
