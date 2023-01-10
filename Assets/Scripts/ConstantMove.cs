using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMove : MonoBehaviour
{
    public Vector3 Speed;
    void Update()
    {
        transform.position += Speed * Time.deltaTime;   
    }
}
