using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.Video;

[ExecuteInEditMode]
public class BasicProjectile : MonoBehaviour
{
    private Rigidbody projectile;
    private Vector3 lastBackPosition;
    private Hitable firedFrom; // Where did the projectile originate?
    public Color hitColor;
    public int speed = 550;
    public Transform explosion;
    public LayerMask mask;
    public GameObject currentHitObject;
    public Transform bulletMark;

    public Transform projectileFront;
    public Transform projectileBack;
    public int damage = 2;

    private void Awake()
    {
        projectile = GetComponent<Rigidbody>(); 
    }

    /// <summary>
    /// It's possible the tip of the projectile has already passed the object, resulting in a missed hit.
    /// The way to get around this is to basically spherecast along the entire displacement vector, which will
    /// allow us to determine if (even though we've passed the mesh) the projectile *would have* hit the mesh if 
    /// it were slower (or the framerate was faster).
    /// </summary>
    void Update()
    {
        bool found = false;

        // How far did we move this frame?
        Vector3 displacement = projectileFront.position - lastBackPosition;

        // Find everything we've collided with...but use the closest mesh.
        RaycastHit[] hits = Physics.SphereCastAll(lastBackPosition, 1, displacement.normalized, displacement.magnitude, -1, QueryTriggerInteraction.Collide);

        RaycastHit closest = new RaycastHit { distance = Mathf.Infinity };
        foreach(RaycastHit hit in hits)
        {
            if (IsValidHit(hit) && hit.distance < closest.distance)
            {
                found = true;
                closest = hit;
            }
        }

        // We have a hit but we might be inside a collider....
        if(found)
        {
            Hitable hitable = closest.collider.gameObject.GetComponentInParent<Hitable>();

            if (closest.distance <= 0f)
            {
                closest.point = projectileBack.position;
                closest.normal = -transform.forward; /// its behind us
            }

            // Break up the projectile.
            if (explosion != null)
                Instantiate(explosion, closest.point, Quaternion.identity);

            // Show the mark
            if (bulletMark != null)
                Instantiate(bulletMark, closest.point, Quaternion.identity);

            if (hitable != null)
            {
                hitable.Hit(transform, projectile.transform, closest, this);
            }

            // Any effects attached to the hitable?
            HitEffect hitEffect = closest.collider.gameObject.GetComponentInParent<HitEffect>();
            if(hitEffect != null)
            {
                hitEffect.Hit(this.hitColor);
            }

            // Destroy the projectile.
            Destroy(projectile.gameObject);
        }

        lastBackPosition = projectileBack.position;
    }

    /// <summary>
    /// It's valid if its not me
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    private bool IsValidHit(RaycastHit hit)
    {
        var hittable = hit.collider.gameObject.GetComponentInParent<Hitable>();

        // Cant hit yourself.
        return hittable != null && hittable.Id != firedFrom.Id && !hit.collider.isTrigger;
    }

    public void Fire(Vector3 forward, Hitable firedFrom)
    {
        projectile.velocity = forward * speed;
        lastBackPosition = projectileBack.position;
        this.firedFrom = firedFrom;

        Destroy(projectile.gameObject, 5f);
    }
}
