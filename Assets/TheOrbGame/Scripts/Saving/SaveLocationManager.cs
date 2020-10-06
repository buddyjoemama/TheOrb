using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLocationManager : MonoBehaviour
{
    public Player player;
    public SaveLocationDetector detector;

    // Start is called before the first frame update
    void Start()
    {
        detector.triggerEvent += Detector_triggerEvent;
    }

    private void Detector_triggerEvent(Collider other)
    {
        Vector3 position = new Vector3(detector.transform.position.x, 0, detector.transform.position.z);
        player.MoveToSaveCenter(position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
