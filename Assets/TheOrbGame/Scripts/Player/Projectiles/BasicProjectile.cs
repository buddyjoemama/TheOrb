using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BasicProjectile : MonoBehaviour
{
    public int speed = 250;
    public Transform explosion;

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
        Instantiate(explosion, collision.GetContact(0).point, Quaternion.identity * Quaternion.Euler(180, 0, 0));
        Destroy(projectile.gameObject);
    }
}
