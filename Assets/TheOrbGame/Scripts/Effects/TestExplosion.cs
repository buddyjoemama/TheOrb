using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestExplosion : AbstractHittable
{
    public float Strength = 10;

    public override void Hit(Transform collider, RaycastHit hit, BasicProjectile projectile)
    {
        BlowUp(hit.point);
    }

    public override bool IsValidHit(RaycastHit hit, IHittable firedFrom)
    {
        return true;
    }

    // Start is called before the first frame update
    void BlowUp(Vector3 hitPosition)
    {
       // void SetForce()
       // {
            // Set same random state
            Random.InitState(1);

            // Set forceMode by mass state
            ForceMode forceMode = ForceMode.Impulse;
            //if (forceByMass == false)
            //    forceMode = ForceMode.VelocityChange;

            float variation = 50;
        float chaos = 1;
      //  Vector3 hitPosition = new Vector3(40f, 12.5f, -20f);// this.transform.position;

            // Get str for each object by explode type with variation
            foreach (var collider in this.GetComponentsInChildren<Collider>())// Projectile projectile in projectiles)
            {
                // TODO check if not activated and doesn't need to be forced

                // Get local velocity strength
                float strVar = Strength * variation / 100f + Strength;
                float str = Random.Range(Strength, strVar);
                var closest = collider.bounds.ClosestPoint(hitPosition);
                float fade = 1f - Vector3.Distance(hitPosition, collider.transform.position) / 5.5f;    

                float strMult = fade * str * 10f;

                // Get explosion vector from explosion position to projectile center of mass
                Vector3 vector = Vector3.Normalize(hitPosition - closest);

                // Apply force
                collider.attachedRigidbody.AddForce(vector * strMult, forceMode);

                // Get local rotation strength 
                Vector3 rot = new Vector3(Random.Range(-chaos, chaos), Random.Range(-chaos, chaos), Random.Range(-chaos, chaos));

                // Set rotation impulse
                collider.attachedRigidbody.angularVelocity = rot;
            collider.attachedRigidbody.useGravity = true;
            }
       // }
    }

    //Vector3 Vector(Collider projectile)
    //{
    //    Vector3 vector = Vector3.up;

    //    // Spherical range
    //    if (rangeType == RangeType.Spherical)
    //        vector = Vector3.Normalize(projectile.positionPivot - explPosition);

    //    // Cylindrical range
    //    //else if (rangeType == RangeType.Cylindrical)
    //    //{
    //    //    Vector3 lineDir = transForm.InverseTransformDirection(Vector3.up);
    //    //    lineDir = Vector3.up;
    //    //    lineDir.Normalize();
    //    //    var vec = projectile.positionPivot - explPosition;
    //    //    var dot = Vector3.Dot(vec, lineDir);
    //    //    Vector3 nearestPointOnLine = explPosition + lineDir * dot;
    //    //    vector = Vector3.Normalize(projectile.positionPivot - nearestPointOnLine);
    //    //}

    //    return vector;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}