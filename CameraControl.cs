using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

/// <summary>
/// THe script defines how the user serial input affects the game main references
/// for this code com efrom Unity Documentation and Unity Forum
/// link: https://docs.unity3d.com/ScriptReference/
/// link: https://answers.unity.com/index.html
/// </summary>

public class CameraControl : MonoBehaviour
{

    public Camera cam1;
    public Camera cam2;
    public GameObject camera1;

    private static string port = "/dev/cu.usbmodem146401";
    private static int baudRate = 9600;
    SerialPort sp = new SerialPort(port, baudRate);
    public IsVisible lightVis;
    public IsVisible tankVis;
    public IsVisible dolphinVis;
    public GameObject dolphin;
    public GameObject flashlight;
    public GameObject airTank;

    private float t;
    private float speed = 2f;
    private bool gameStarted;
    private Rigidbody player;

    public LightControl light;
    public LightControl tank;
    public GameObject spotlight;
    public Transform spotlightPrefab;
    private Quaternion spotlightRot; 
    private Vector3 lastPos;
    private Quaternion lastRot;
    public bool lightAcquired, airAcquired, dolphinInstantiated, dolphinAcquired, interactPressed, dolphinAudioStart;

    public int direction;

    private bool isRunning;



    //Source code referenced for IEnumerator https://answers.unity.com/questions/1141073/move-object-in-button-click-smooth-to-position.html
    IEnumerator MoveToObject(Vector3 targetPos,Quaternion targetRot, float delta)
    {
        // Will need to perform some of this process and yield until next frames
        float closeEnough = 0.1f;
        isRunning = true;
        float distance = (transform.position - targetPos).magnitude;
        Quaternion fromRotation = transform.rotation;


        // GC will trigger unless we define this ahead of time
        WaitForEndOfFrame wait = new WaitForEndOfFrame();

        // Continue until we're there
        while (distance >= closeEnough)
        {
            // Confirm that it's moving
            //Debug.Log("Executing Movement");

            // Move a bit then  wait until next  frame
            transform.position = Vector3.Slerp(transform.position, targetPos, delta);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, delta);
            yield return wait;

            // Check if we should repeat
            distance = (transform.position - targetPos).magnitude;
        }

        // Complete the motion to prevent negligible sliding
        transform.position = targetPos;
        transform.rotation = targetRot;



        // Confirm  it's ended
        isRunning = false;
        if(airAcquired && !dolphinAudioStart)
        {
            dolphinAudioStart = true;
        }
        //Debug.Log("Movement Complete");
    }




    // Use this for initialization
    void Start()
    {

        cam1.enabled = true;
        cam2.enabled = false;
        interactPressed = false;
        isRunning = false;
        gameStarted = false;
        lightAcquired = false;
        airAcquired = false;
        dolphinAcquired = false;
        dolphinInstantiated = false;
        dolphin.SetActive(false);
        spotlightRot = Quaternion.Euler(.62f, 80.22f, 0);

        Physics.gravity = new Vector3(0f, -1f, 0f);
        player = GetComponent<Rigidbody>();

        sp.Open();
        sp.ReadTimeout = 1;


    }

    // Update is called once per frame
    void Update()
    {

        if (sp.IsOpen)
        {

            try
            {
                RotateView(sp.ReadByte());

            }
            catch (System.Exception)
            {

            }
        }

        direction = sp.ReadByte();
        KeyInput();


    }

    private void KeyInput(){

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
            Debug.Log("Move Backwards");
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * speed * Time.deltaTime;
            Debug.Log("Move Forwards");

        }
        if (Input.GetKey(KeyCode.Space))
        {
            cam1.enabled = false;
            cam2.enabled = true;
            sp.Close();
        }
    }

    private void RotateView(int button)
    {

        Vector3 byAngles;
        float inTime = 4f;
        if(gameStarted == false){
            player.useGravity = true;
            gameStarted = true;

        }

        if (button == 0 && !interactPressed && !isRunning)
        {
            byAngles = Vector3.up * 5;
            var fromAngle = transform.rotation;
            var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
         
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, Time.deltaTime * inTime);

            //Debug.Log("right came through");

        }

        if (button == 1 && !interactPressed && !isRunning)
        {
            byAngles = Vector3.up * -5;
            var fromAngle = transform.rotation;
            var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);

            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, Time.deltaTime * inTime);

            //Debug.Log("left came through");

        }

        if (button == 2 && !interactPressed && !isRunning) {

            lastPos = transform.position;
            lastRot = transform.rotation;
            float delta = .005f;

            if (lightVis.isVis)
            {
                Vector3 toSpotlight = new Vector3(1.63f, -7f, -10.2f);
                StartCoroutine(MoveToObject(toSpotlight, spotlightRot, delta));
            }

            if (tankVis.isVis && lightAcquired)
            {
                Vector3 toTank = new Vector3(-2f, -6.9f, -9.6f);
                Quaternion tankRot = Quaternion.Euler(0f, 322.35f, 0f);
                StartCoroutine(MoveToObject(toTank, tankRot, delta));
            }

            interactPressed = true;
            //Debug.Log("interact pressed = true");

        
        }

        if (button == 3 && interactPressed && !isRunning)
        {
            float delta = .005f;
            StartCoroutine(MoveToObject(lastPos, lastRot, delta));
            interactPressed = false;
            //Debug.Log("interact pressed = false");


        }


        if (button == 4)
        {

            //Debug.Log("button 5 pressed");
            //Debug.Log(dolphinVis.isVis);

            if (lightVis.isVis && light.inRange && !lightAcquired)
            {
                GameObject newlight = Instantiate(spotlight, transform.position, transform.rotation);
                newlight.transform.parent = this.transform;
                lightAcquired = true;
               //flashlight.SetActive(false);
            } 
            else if (tankVis.isVis && tank.inRange && lightAcquired && !dolphinInstantiated)
            {
                //Debug.Log("Oxygen Acquired");
                airAcquired = true;
                dolphin.SetActive(true);
                dolphinInstantiated = true;
                //airTank.SetActive(false);
            }
            else if (dolphinVis.isVis == true)
            {
                cam1.enabled = false;
                cam2.enabled = true;
                GetComponent<AudioListener>().enabled = false;
                camera1.SetActive(false);
                //Debug.Log("Dolphin is Vis");
                dolphinAcquired = true;
            }

        }



    }
}
