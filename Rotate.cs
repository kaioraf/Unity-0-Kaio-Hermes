using UnityEngine;
using System.Collections;

public class Rotate: MonoBehaviour {
    public float rotateX = 15;
    public float rotateY = 30;
    public float rotateZ = 45;
    void Update()
    {
        transform.Rotate(new Vector3 (rotateX, rotateY, rotateZ) * Time.deltaTime);
    }
}