using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ForceHitAction : MonoBehaviour, IHitAction
{
    public int Force;

    public void Apply(Transform collider, RaycastHit hit, BasicProjectile projectile)
    {
        Vector3 force = projectile.transform.forward * Force;
        this.GetComponent<Rigidbody>().AddForceAtPosition(force, hit.transform.position);
    }
}