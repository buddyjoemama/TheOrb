using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
         transform.position = player.transform.position + offset;
        //if (Time.frameCount % 10 == 0)
        //{
        //    transform.position = Vector3.Lerp(transform.position, transform.position - (transform.forward * 10), 10f);
        //}
    }
}
