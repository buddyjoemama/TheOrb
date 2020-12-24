using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    private Transform owner;
    private Transform target;
    private GunRig rig;
    public List<Transform> boundaries;
    private Enemy parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponent<Enemy>();

        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        int index = 0;
        
        Vector3 velocity = Vector3.zero;
        while (true)
        {
            while(!this.Approached(parent.transform.position, boundaries[index].position))
            {
                parent.transform.position = Vector3.SmoothDamp(parent.transform.position, boundaries[index].position, ref velocity, .75f);
                yield return new WaitForFixedUpdate();
            }

            index += 1;

            if (index >= boundaries.Count)
                index = 0;

            yield return new WaitForFixedUpdate();
        }
    }

    private bool Approached(Vector3 position1, Vector3 position2)
    {
        return Mathf.CeilToInt(position1.x) == Mathf.CeilToInt(position2.x)
            && Mathf.CeilToInt(position1.y) == Mathf.CeilToInt(position2.y)
            && Mathf.CeilToInt(position1.z) == Mathf.CeilToInt(position2.z);
    }

    // Update is called once per frame
    void Update()
    {
        owner.LookAt(target);
    }

    internal void Configure(Transform owner, Transform target, GunRig gunRig)
    {
        this.owner = owner;
        this.target = target;
        this.rig = gunRig;

        StartCoroutine(Process());
    }

    private IEnumerator Process()
    {
        while (true)
        {
            owner.LookAt(target);
            rig.OnFire();

            yield return new WaitForSeconds(5);
        }
    }
}
