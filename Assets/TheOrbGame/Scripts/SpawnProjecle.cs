using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjecle : MonoBehaviour
{
    public GameObject firePoint;
    public GameObject vfx;

    private GameObject effectToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        effectToSpawn = vfx;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnFire()
    {
        Instantiate(vfx, firePoint.transform.position, Quaternion.identity);
    }

    void SpawnVFX()
    {

    }
}
