using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DistanceManager : MonoBehaviour {
    private List<BodyGameObject> bodies = new List<BodyGameObject>();
    public UnityEngine.AudioClip ping;
    public UnityEngine.AudioSource src;
    // Use this for initialization
    void Start () {
        //checkDistance();
        src = this.GetComponent<UnityEngine.AudioSource>();
    }
	
	// Update is called once per frame
	void LateUpdate () {
        checkDistance();
	}
    private void checkDistance()
    {
        float distance_overall = 0.0f;
        if (bodies.Count == 2)
        {

            Vector3 head1 = bodies[0].GetJoint(Windows.Kinect.JointType.Head).transform.localPosition;
            Vector3 head2 = bodies[1].GetJoint(Windows.Kinect.JointType.Head).transform.localPosition;
            distance_overall = Math.Abs((head1.x - head2.x) + (head1.z - head2.z));
           

            //lowPass.cutoffFrequency = 5000;
            if (distance_overall < 10 && distance_overall > 0)
            {
                Debug.Log("two people close together");

                src.clip = ping;
                src.Play();



            }
        }

    }
    void Kinect_BodyFound(object args)
    {
        BodyGameObject bodyFound = (BodyGameObject)args;
        bodies.Add(bodyFound);

    }

    void Kinect_BodyLost(object args)
    {
        ulong bodyDeletedId = (ulong)args;

        lock (bodies)
        {
            foreach (BodyGameObject bg in bodies)
            {
                if (bg.ID == bodyDeletedId)
                {
                    bodies.Remove(bg);

                    return;
                }
            }
        }
    }

}
