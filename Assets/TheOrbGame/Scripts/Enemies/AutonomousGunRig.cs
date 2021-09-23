using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousGunRig : GunRig
{
    public bool enabled = true;

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
            yield return new WaitForSeconds(5);

            if (enabled)
                OnFire();
        }
    }
}
   