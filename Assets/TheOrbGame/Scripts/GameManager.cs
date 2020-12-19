using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform objectToCreate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateObject());
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    IEnumerator CreateObject()
    {
        while(true)
        {
            Instantiate(objectToCreate, new Vector3(0, 20, 50), Quaternion.identity);

            yield return new WaitForSeconds(5f);
        }
    }
}
