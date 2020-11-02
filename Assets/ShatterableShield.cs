using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterableShield : MonoBehaviour
{
    public float scale;
    public float minForce;
    public float maxForce;
    public float radius;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(scale, scale, scale);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Destroy()
    {
        foreach(Transform t in transform)
        {
            var rb = t.GetComponent<Rigidbody>();

            if(rb != null)
            {
                rb.AddExplosionForce(Random.Range(minForce, maxForce), transform.position, radius);
            }

            Destroy(t.gameObject, 5);
        }
    }
}
