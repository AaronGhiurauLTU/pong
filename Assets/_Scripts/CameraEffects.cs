using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public static float effectSize = 8.0f;
    public static float strongMultiplier = 1.8f;
    public static float stopTime;
    public static bool strong;

    public static Vector3 startLocation;

    void Start()
    {
        // this only works if the camera doesn't move during play!
        startLocation = Camera.main.transform.position;
    }

    void Update()
    {
        if (Time.time < stopTime)
        {
            float size = strong ? effectSize * strongMultiplier : effectSize;
            float randX = Random.Range(-1 * size, size) * Time.deltaTime;
            float randY = Random.Range(-1 * size, size) * Time.deltaTime;
            float randZ = Random.Range(-1 * size, size) * Time.deltaTime;
            Camera.main.transform.position += new Vector3(randX, randY, randZ);
            
        }
        else
        {
            if (Vector3.Distance(Camera.main.transform.position, startLocation) > effectSize * .75f)
            {
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, startLocation,
					effectSize * .75f * Time.deltaTime);
            }
			else
			{
				Camera.main.transform.position = startLocation;
			}
        }
    }

    public static void Shake(float seconds, bool str = false)
    {
        //change it to strong now if that's the call
        //or if the last shake is done, set it however
        if (str || stopTime <= Time.time)
        {
            strong = str;
        }
       //otherwise it's already running so don't downgrade it in case it's already strong

        //calculate the end time for the new effect
        float newTime = Time.time + seconds;
		
        //only set stopTime if it's going to be further out than the current run
        if (newTime > stopTime)
        {
            stopTime = newTime;
        }
    }
}