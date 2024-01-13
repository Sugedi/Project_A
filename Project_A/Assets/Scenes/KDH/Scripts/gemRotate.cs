using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gemRotate : MonoBehaviour
{
    public float gemRotSpeed = 100f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, gemRotSpeed * Time.deltaTime));
    }
}
