using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectTTL : MonoBehaviour
{
    public float TimeToLiveInSeconds;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, TimeToLiveInSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
