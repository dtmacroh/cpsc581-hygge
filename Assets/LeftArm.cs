﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;
using System;
public class LeftArm : MonoBehaviour
{


    //use this variable in the inspector to finetune your gesture detection.
    public float angleTolerance = 10.0f;
    public UnityEngine.AudioSource src;
    //public UnityEngine.AudioSource src2;
   // public UnityEngine.AudioClip drum1;
    public UnityEngine.AudioClip drum2;
    private List<BodyGameObject> bodies = new List<BodyGameObject>();
    public BodyGameObject targetBody;
    public int targetBodyIndex;
    public AudioReverbZone audioReverb;

    void Start()
    {
        src = this.GetComponent<UnityEngine.AudioSource>();
        audioReverb = this.GetComponent<UnityEngine.AudioReverbZone>();
        //src2 = this.GetComponent<UnityEngine.AudioSource>();
    }

    //remember to use late update for after the KinectManager has updated all sensor information
    void LateUpdate()
    {
        checkLeftArm();
        checkDistance();
    }

    private bool InRange(float value, float targetValue, float delta)
    {
        return (value >= (targetValue - delta)) && (value <= (targetValue + delta));
    }
    private void checkDistance()
    {
        float distance_overall = 1000.0f;
        if (bodies.Count ==2)
        {

            Vector3 head1 = bodies[0].GetJoint(Windows.Kinect.JointType.Head).transform.localPosition;
            Vector3 head2 = bodies[1].GetJoint(Windows.Kinect.JointType.Head).transform.localPosition;
            distance_overall = Math.Abs((head1.x - head2.x) + (head1.z - head2.z));
            //both people have to be near
            Debug.Log(distance_overall);

        }
        if (distance_overall < 10)
        {
            Debug.Log("two people close together");

        }

    }
    private void checkLeftArm()
    {
        if (bodies.Count == targetBodyIndex + 1)
        {
            //some bodies, send orientation update

            Vector3 shoulderLeft = targetBody.GetJoint(Windows.Kinect.JointType.ShoulderLeft).transform.localPosition;
            Vector3 elbowLeft = targetBody.GetJoint(Windows.Kinect.JointType.ElbowLeft).transform.localPosition;
            Vector3 wristLeft = targetBody.GetJoint(Windows.Kinect.JointType.WristLeft).transform.localPosition;

            Vector3 wristRight = targetBody.GetJoint(Windows.Kinect.JointType.WristRight).transform.localPosition;
            Vector3 spine = targetBody.GetJoint(Windows.Kinect.JointType.SpineMid).transform.localPosition;



            //Vector3 shoulderLeft = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.ShoulderLeft).transform.localPosition;
            //Vector3 elbowLeft = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.ElbowLeft).transform.localPosition;
            //Vector3 wristLeft = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.WristLeft).transform.localPosition;

            //Vector3 wristRight = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.WristRight).transform.localPosition;
            //Vector3 spine = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.SpineMid).transform.localPosition;




            if (wristLeft.y < spine.y)
            {
                Debug.Log("Left Drumming");
                Debug.Log("wrist" + targetBodyIndex + " " + wristLeft.y + "spine" + targetBodyIndex + " " + spine.y);
                src.clip = drum2;
                audioReverb.maxDistance = (spine.z / 5) * (spine.z / 5) * 3;

                src.Play();

            }

        }
    }

    void Kinect_BodyFound(object args)
    {
        BodyGameObject bodyFound = (BodyGameObject)args;
        bodies.Add(bodyFound);
        for(int i=0; i<bodies.Count;i++)
        {
            if (i==targetBodyIndex)
            {
                targetBody = bodyFound;
            }
        }
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