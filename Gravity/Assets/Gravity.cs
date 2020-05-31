using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gravity : MonoBehaviour
{
    GameObject self;
    GameObject[] Planets;
    GameObject[] Traces;
    public GameObject trace;
    Vector3 dir;
    public float g = 10;
    public float LaunchForce = 10;
    public bool WithStartImpulse;
    bool doTrace = false;
    void Start()
    {
        self = this.gameObject;
        if (WithStartImpulse)
        {
            self.GetComponent<Rigidbody>().AddForce(self.transform.forward * LaunchForce, ForceMode.VelocityChange);
        }
    }

    void Update()
    {
        Planets = GameObject.FindGameObjectsWithTag("Planet");
        Traces = GameObject.FindGameObjectsWithTag("Trace");
        dir = Vector3.zero;
        for (int i = 0; i < Planets.Length; i++)
        {
            dir += Planets[i].transform.position - self.transform.position;
        }
        self.GetComponent<Rigidbody>().AddForce(dir * g);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            doTrace = !doTrace;
        }
        if (doTrace)
        {
            Instantiate(trace, self.transform.position, Quaternion.identity);
        }
        if (!doTrace)
        {
            foreach (GameObject trace in Traces)
            {
                Destroy(trace);
            }
        }
    }
}
