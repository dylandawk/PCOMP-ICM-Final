using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {

    public Underwater underwater;
    public CameraControl player;
    public GameObject dolphin;
    public Timer timer;
    public AudioSource ambient, splash, bright, breathe, squeak, gameOver, gameWon, surfaced;


    private bool brightPlayed, breathPlayed, gameOverPlayed, gameWonPlayed;
    public bool gameStarted;

	// Use this for initialization
	void Start () {

        brightPlayed = false;
        breathPlayed = false;
        gameOverPlayed = false;
        gameStarted = false;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(underwater.isUnderwater && !splash.isPlaying &&  !ambient.isPlaying) {
            splash.Play(); 
            ambient.Play();
            gameStarted = true;

        }

        if(player.lightAcquired && !bright.isPlaying && !brightPlayed)
        {
            bright.Play();
            brightPlayed = true;

        }

        if (player.airAcquired && !breathe.isPlaying)
        {

            breathe.Play();
     
        }
        if (player.airAcquired && !squeak.isPlaying && player.dolphinAudioStart)
        {
        
            squeak.Play();
        }

        if (timer.targetTime < 0.01 && !gameOver.isPlaying && !gameOverPlayed)
        {
            gameOver.Play();
            gameOverPlayed= true;
        }
        if(timer.targetTime > 0.01 && dolphin.transform.position.y > -0.3f && !gameWon.isPlaying && !gameWonPlayed && player.airAcquired && !surfaced.isPlaying)
        {
            gameWon.Play();
            surfaced.Play();
            gameWonPlayed = true;
        }

	}
}
