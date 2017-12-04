using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour {
    //AudioSource aud;
    public float time = 0.0f;
    public float MAX_TIME = 60.0f;
    // Use this for initialization
    void Start () {
        //aud = this.GetComponent<AudioSource>();
        //aud.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
        //aud.loop = true;
        //while (!(Microphone.GetPosition(null) > 0)) { }
        //aud.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void pauseRecorder(string args)
    {
        time += Time.deltaTime;
        Debug.Log("timestamp" + args + " " + time);
        //aud.Pause();
    }

}
