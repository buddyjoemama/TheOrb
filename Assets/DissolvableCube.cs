using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolvableCube : MonoBehaviour
{
    private Material material;
    private float amount = 0;


    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        amount = material.GetFloat("DissolveAmount");
    }

    // Update is called once per frame
    void Update()
    {
        if(material.GetFloat("DissolveAmount") > 1.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        amount += (Time.deltaTime / 3.5f);
        material.SetFloat("DissolveAmount", amount);
    }
}
