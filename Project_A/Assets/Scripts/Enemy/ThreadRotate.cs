using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadRotate : MonoBehaviour
{
    public float ThreadRotSpeed = 100f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, ThreadRotSpeed * Time.deltaTime));
    }
}