using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousGunRig : GunRig
{
    public Player target;

    public override void Start()
    {
        StartCoroutine(Fire());
    }

    public override void Update()
    {
        transform.LookAt(target.transform.position);
    }

    public override void OnFire()
    {
        base.OnFire();
    }

    IEnumerator Fire()
    {
        while(true)
        {
            yield return new WaitForSeconds(5);

            if (enabled)
                OnFire();
        }
    }
}
   