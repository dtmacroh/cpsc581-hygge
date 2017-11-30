using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour {
    AudioSource aud;

    // Use this for initialization
    void Start () {
        aud = this.GetComponent<AudioSource>();
        aud.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
        aud.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void pauseRecorder()
    {
        aud.Pause();
    }

}
