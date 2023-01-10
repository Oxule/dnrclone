using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public AnimationCurve curve;
    public float duration;
    public float offset;

    private void Update()
    {
        float p = curve.Evaluate(Time.time%duration);
        transform.GetChild(0).localPosition = new Vector3(transform.GetChild(0).localPosition.x, p, transform.GetChild(0).localPosition.z);
        transform.GetChild(0).gameObject.active = p+offset > 0;
        
    }
}
