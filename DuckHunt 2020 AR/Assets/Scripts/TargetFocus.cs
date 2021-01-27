using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TargetFocus : DefaultTrackableEventHandler
{
    public static TargetFocus instance;
    //public Vector3 newLocation = new Vector3(Random.Range(-48.0f, 48.0f), Random.Range(10.0f, 48.0f), Random.Range(-48.0f, 48.0f));
    public Vector3 newLocation;
    public Transform targetFocus;
    //public int numToSpawn;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        moveTarget();
    }

    public void moveTarget()
    {
        /*Vector3 newLocation;
        newLocation.x = Random.Range(-40.0f, 40f);
        newLocation.y = Random.Range (10f, 30f);
        newLocation.z = Random.Range (-40f, 40f);
        transform.postion = new Vector3(newLocation.x, newLocation.y, newLocation.z);*/

        newLocation = new Vector3(Random.Range(-5.0f, 5.0f), Random.Range(1f, 5f), Random.Range(-5f, 5f));
        transform.position = new Vector3(newLocation.x, newLocation.y, newLocation.z);
        if(DefaultTrackableEventHandler.trueFalse==true)
            {
                RaycastController.instance.playSound(0);
            }
    }
}
