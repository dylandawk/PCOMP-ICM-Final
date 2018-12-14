using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextController : MonoBehaviour {

    public Text instructionText;
    public CameraControl player;
    public Transform playerTrans;
    public LightControl light;
    public IsVisible lightVis;
    public IsVisible tankVis;
    public IsVisible dolphinVis;
    public LightControl tank;
    public Underwater underwater;
    public Timer timer;
    public GameObject dolphin;
    // Use this for initialization
    void Start () {

        instructionText.text = "Step anywhere to start";
		
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log("lightVis = " + lightVis.isVis);
        //Debug.Log("inRange = " + light.inRange);
        //Debug.Log("lightAcquired = " + player.lightAcquired);

        if(playerTrans.position.y > -5 && !player.lightAcquired && underwater.isUnderwater)
        {
            instructionText.text = "Anywhere but there.";
        }
        if (playerTrans.position.y < -7 && !player.lightAcquired)
        {
            instructionText.text = "Sure is dark down here. You better look for a light.";
        }

        if (lightVis.isVis && light.inRange && !player.lightAcquired)
        {
            instructionText.text = "Great! Press the light to turn it on.";
        }

        if (player.lightAcquired && !tankVis.isVis && !player.airAcquired)
        {
            instructionText.text = "Quick! You're running out of breath, maybe you can find an oxygen tank?";
        }
        if (tankVis.isVis && tank.inRange && player.lightAcquired && !player.dolphinInstantiated)
        {
            instructionText.text = "Press the tank to equip the oxygen";

        }
        if (tankVis.isVis && tank.inRange && player.airAcquired)
        {
            instructionText.text = "Whew. You can breathe now. Now, how to get out of this cave?";
        }
        if (dolphinVis.isVis)
        {
            instructionText.text = "Wonder where that dolphin is going. Grab on?";
        }
        if (player.dolphinAcquired)
        {
            instructionText.text = "You can steer the dolphin to the surface!";
        }
        if (timer.targetTime < .01){
            instructionText.text = "You Died.";
        }
        if(dolphin.transform.position.y > -0.3 && timer.targetTime > .01f)
        {
            instructionText.text = "Congratulations! You made it to the Surface!";
        }





    }


}
