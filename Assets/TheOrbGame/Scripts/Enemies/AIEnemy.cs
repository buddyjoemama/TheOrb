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

        while (true)
        {
            while(!Approched(parent.transform.position, boundaries[index].position))
            {
                parent.transform.position = Vector3.Lerp(parent.transform.position, boundaries[index].position, Time.deltaTime);
                yield return new WaitForFixedUpdate();
            }

            index += 1;

            if (index >= boundaries.Count)
                index = 0;

            yield return new WaitForFixedUpdate();
        }
    }

    private bool Approched(Vector3 position1, Vector3 position2)
    {
        return (int)position1.x == (int)position2.x
            && (int)position1.y == (int)position2.y
            && (int)position1.z == (int)position2.z;
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
