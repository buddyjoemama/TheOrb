using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    Quaternion target = Quaternion.Euler(20, 90, 0);
    int val = 90;
    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 1.2f);

        if (Mathf.Round(transform.rotation.eulerAngles.y) % 90 == 0)
        {
            target = Quaternion.Euler(20, val += 90, 0);
        }
    }
}
 