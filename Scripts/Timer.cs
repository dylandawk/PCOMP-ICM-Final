using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour {

    public Text timerText;
    public float targetTime;
    public AudioScript audioScript;
	// Use this for initialization

	void Start () {

        targetTime = 360.0f;


    }

    // Update is called once per frame
    void Update()
    {
        if (audioScript.gameStarted)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0f)
            {
                targetTime = 0;
            }
            string minutes = ((int)targetTime / 60).ToString();
            string seconds = (targetTime % 60).ToString("f1");
            timerText.text = minutes + ":" + seconds;
        }
    }
}
