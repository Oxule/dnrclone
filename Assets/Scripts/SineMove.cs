using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SineMove : MonoBehaviour
{
    public Vector3 amplitude;
    public Vector3 frequency;

    public Vector3 origin;
    
    public float offset;

    private void Start()
    {
        origin = transform.position;
        offset = Random.Range(0, 100);
    }

    void Update()
    {
        transform.position = origin + new Vector3(Mathf.Sin(Time.time * frequency.x + offset) * amplitude.x, Mathf.Sin(Time.time * frequency.y + offset) * amplitude.y, Mathf.Sin(Time.time * frequency.z + offset) * amplitude.z);
        
    }
}
