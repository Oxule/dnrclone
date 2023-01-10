using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineRotate : MonoBehaviour
{
    public float ampl;
    public float freq;
    
    public Vector3 axis;
    public Vector3 origin;

    public void Start()
    {
        origin = transform.rotation.eulerAngles;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(origin + axis * Mathf.Sin(Time.time * freq) * ampl);
    }
}
