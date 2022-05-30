using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateForever : MonoBehaviour
{
    private float _initialY = 0;
    public int Displacement = 10;

    // Start is called before the first frame update
    void Start()
    {
        _initialY = transform.position.y;
    }

    Vector3 vel = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 1);
        transform.position = new Vector3(transform.position.x, 
            (Mathf.Sin(Time.fixedTime) * Displacement) + _initialY);
    }

    private void FixedUpdate()
    {

    }
}
