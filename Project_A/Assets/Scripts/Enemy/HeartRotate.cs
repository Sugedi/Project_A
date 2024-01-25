using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartRotate : MonoBehaviour
{
    public float heartRotSpeed = 100f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, heartRotSpeed * Time.deltaTime));
    }
}