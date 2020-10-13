using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BasicProjectile : MonoBehaviour
{
    public int speed = 250;

    private Rigidbody projectile;

    private void Awake()
    {
        projectile = GetComponent<Rigidbody>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector3 forward)
    {
        projectile.velocity = forward * speed;
        Destroy(projectile.gameObject, 2f);
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(projectile.gameObject);
    }
}
