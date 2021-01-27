using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    private Transform targetFocus;

    // Start is called before the first frame update
    void Start()
    {
        targetFocus = GameObject.FindWithTag("Target").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = targetFocus.position - this.transform.position;
        //Debug.Log (target.magnitude);

        //Added proximity detection to avoid laggy collision with target
        if(target.magnitude < 1)
        {
            TargetFocus.instance.moveTarget();
        }
        transform.LookAt (targetFocus.transform);

        float speed = Random.Range(.02f,3f);
        transform.Translate(0,0,speed * Time.deltaTime);
    }
}
