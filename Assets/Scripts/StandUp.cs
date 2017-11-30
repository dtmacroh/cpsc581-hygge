using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
public class StandUp : MonoBehaviour {


    //use this variable in the inspector to finetune your gesture detection.
    public float sittingThreshold = -5.0f;

    private List<BodyGameObject> bodies = new List<BodyGameObject>();


    void Start () {
		
	}
	
    //remember to use late update for after the KinectManager has updated all sensor information
	void LateUpdate () {
        if (bodies.Count > 0)
        {
            //measure butt
            Vector3 spineBasePos = bodies[0].GetJoint(Windows.Kinect.JointType.SpineBase).transform.localPosition;

            //Debug.Log(spineBasePos);

            if (spineBasePos.y <= sittingThreshold)
            {
                Debug.Log("Sitting");
            }
            else
            {
                //Debug.Log("Standing");
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