using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GenericArm : MonoBehaviour {


    //use this variable in the inspector to finetune your gesture detection.
    public float angleTolerance = 10.0f;
    public UnityEngine.AudioSource src;
    //public UnityEngine.AudioSource src2;
    public UnityEngine.AudioClip sound1;
    public UnityEngine.AudioClip sound2;
    public UnityEngine.AudioClip level2_sound1;
    public UnityEngine.AudioClip level2_sound2;
    public UnityEngine.AudioClip level3_sound1;
    public UnityEngine.AudioClip level3_sound2;

    public UnityEngine.AudioClip ping;
    private List<BodyGameObject> bodies = new List<BodyGameObject>();
    //public BodyGameObject targetBody;
    public ulong targetBodyID;
    public int targetBodyIndex;
    public AudioReverbZone audioReverb;
    public AudioLowPassFilter lowPass;
    public string rightOrLeft;
    public bool notAnArm;
    public int levelCounter;
    private List<UnityEngine.AudioClip> sounds ;
    // Use this for initialization
    void Start () {
        src = this.GetComponent<UnityEngine.AudioSource>();
        audioReverb = this.GetComponent<UnityEngine.AudioReverbZone>();
        lowPass = this.GetComponent<UnityEngine.AudioLowPassFilter>();
        lowPass.cutoffFrequency = 5000;
        sounds = new List<UnityEngine.AudioClip> { sound1, sound2, level2_sound1, level2_sound2 };
        levelCounter = 0;


    }
	
	// Update is called once per frame
	void LateUpdate () {
        if (!notAnArm)
        {
            if (rightOrLeft.Equals("right"))
            { checkRightArm(); }
            else
            { checkLeftArm(); }
        }
        checkDistance();
    }
    private void checkLeftArm()
    {
        if (bodies.Count == targetBodyIndex + 1)
        {
            //some bodies, send orientation update

            Vector3 shoulderLeft = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.ShoulderLeft).transform.localPosition;
            Vector3 elbowLeft = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.ElbowLeft).transform.localPosition;
            Vector3 wristLeft = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.WristLeft).transform.localPosition;

            Vector3 wristRight = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.WristRight).transform.localPosition;
            Vector3 spine = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.SpineMid).transform.localPosition;

            Vector3 head = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.Head).transform.localPosition;


            //Vector3 shoulderLeft = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.ShoulderLeft).transform.localPosition;
            //Vector3 elbowLeft = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.ElbowLeft).transform.localPosition;
            //Vector3 wristLeft = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.WristLeft).transform.localPosition;

            //Vector3 wristRight = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.WristRight).transform.localPosition;
            //Vector3 spine = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.SpineMid).transform.localPosition;



            audioReverb.maxDistance = spine.z * 2;
            if (wristLeft.y < spine.y && wristLeft.y < elbowLeft.y)
            {
                Debug.Log("Left Drumming");
                Debug.Log("wrist" + targetBodyIndex + " " + wristLeft.y + "spine" + targetBodyIndex + " " + spine.y);
                src.clip = sound1;

                src.Play();

            }
            else if (wristLeft.y > spine.y && wristLeft.y > head.y)
            {
                src.clip = sound2;
                src.Play();

            }

        }
    }
    private bool InRange(float value, float targetValue, float delta)
    {
        return (value >= (targetValue - delta)) && (value <= (targetValue + delta));
    }

    private void checkRightArm()
    {
        if (bodies.Count == targetBodyIndex + 1)
        {

            Vector3 wristLeft = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.WristLeft).transform.localPosition;

            Vector3 wristRight = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.WristRight).transform.localPosition;
            Vector3 spine = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.SpineMid).transform.localPosition;
            Vector3 head = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.Head).transform.localPosition;

            Vector3 elbowRight = bodies[targetBodyIndex].GetJoint(Windows.Kinect.JointType.ElbowRight).transform.localPosition;

            audioReverb.maxDistance = spine.z * 2;
            if (wristRight.y < spine.y && wristRight.y < elbowRight.y)
            {
                Debug.Log("Right Drumming");
                Debug.Log("Right wrist" + targetBodyIndex + " " + wristLeft.y + "spine" + targetBodyIndex + " " + spine.y);
                audioReverb.maxDistance = spine.z * 2; //magic formula for reverb to work

                src.clip = sound1;

                src.Play();
                //lowPass.cutoffFrequency = 1839;
            }
            else if (wristRight.y > spine.y && wristRight.y > head.y)
            {
                Debug.Log("Right Overhead");
                // Debug.Log("Right wrist" + targetBodyIndex + " " + wristLeft.y + "spine" + targetBodyIndex + " " + spine.y);
                audioReverb.maxDistance = spine.z * 2; //magic formula for reverb to work

                src.clip = sound2;
                src.Play();
                //lowPass.cutoffFrequency = 1839;
            }

            //elbowShoulder.Normalize();
            //elbowWrist.Normalize();

            // float armBendAngle = Vector3.SignedAngle(elbowShoulder, elbowWrist, Vector3.right);

            //// Debug.Log("Arm angle" + armBendAngle);


            // if (InRange (armBendAngle, 180, angleTolerance) || InRange(armBendAngle, -180, angleTolerance))
            // {
            //     Debug.Log("Straight out");
            // }
            // else if (InRange(armBendAngle, 90, angleTolerance))
            // {
            //     Debug.Log("Arm Bent up");
            // }
            // else if (InRange(armBendAngle, -90, angleTolerance))
            // {
            //     Debug.Log("Arm Bent down");
            // }
        }
    }

    private void checkDistance()
    {
        float distance_overall = 1000.0f;
        if (bodies.Count == 2)
        {

            Vector3 head1 = bodies[0].GetJoint(Windows.Kinect.JointType.Head).transform.localPosition;
            Vector3 head2 = bodies[1].GetJoint(Windows.Kinect.JointType.Head).transform.localPosition;
            distance_overall = Math.Abs((head1.x - head2.x) + (head1.z - head2.z));
            //both people have to be near
            Debug.Log("distance overall " + distance_overall);
           
            lowPass.cutoffFrequency = 1500 + (distance_overall * 200);
            //lowPass.cutoffFrequency = 1839;

        }
        if (distance_overall < 10)
        {
            Debug.Log("two people close together");
            if (notAnArm)
            {
                src.clip = ping;
                src.Play();
            }
            else
            {
                //loop through sounds
                if (sounds.Count > levelCounter + 2)
                { levelCounter = levelCounter + 2; }
                else { levelCounter = 0; }
                sound1 = sounds[levelCounter];
                sound2 = sounds[levelCounter + 1];
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
