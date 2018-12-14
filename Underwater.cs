using UnityEngine;
using System.Collections;

public class Underwater : MonoBehaviour
{
    public float waterHeight;
    public bool isUnderwater;
    private Color normalColor;
    private Color underwaterColor;

    // Use this for initialization


    void Start()
    {
        normalColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
        underwaterColor = new Color(.165f,0.2f,0.29f, .5f);
        waterHeight = 0;
        isUnderwater = false;
    }

    // Update is called once per frame
    void Update()
    {

        if ((transform.position.y < waterHeight) != isUnderwater)
        {
            isUnderwater = transform.position.y < waterHeight;
            if (isUnderwater) SetUnderwater();
            if (!isUnderwater) SetNormal();

        }


    }

    void SetNormal()
    {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.01f;
        Physics.gravity = new Vector3(0f, -1f, 0f);

    }

    void SetUnderwater()
    {
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = 0.5f;
        Debug.Log("underwater"); 
        Physics.gravity = new Vector3(0f, -.2f, 0f);

    }


}
