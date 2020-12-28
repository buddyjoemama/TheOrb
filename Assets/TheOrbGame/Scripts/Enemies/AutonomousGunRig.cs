using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousGunRig : GunRig
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
    }

    protected override void Update()
    {
        if (target)
        {
            transform.LookAt(target.transform.position);
        }
    }

    public override void OnFire()
    {
        base.OnFire();
    }

    IEnumerator Fire()
    {
        while(true)
        {
            OnFire();

            yield return new WaitForSeconds(2);
        }
    }
}
