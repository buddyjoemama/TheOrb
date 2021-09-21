using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.Video;

public class BasicProjectile : MonoBehaviour
{
    private Rigidbody projectile;
    private Vector3 lastBackPosition;
    private IHittable firedFrom; // Where did the projectile originate?
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
            IHittable hittable = hit.collider.gameObject.GetComponentInParent<IHittable>();

            if (hittable != null && 
                hittable.IsValidHit(hit, firedFrom) && 
                hit.distance < closest.distance)
            {
                found = true;
                closest = hit;
            }
        }

        // We have a hit but we might be inside a collider....
        if(found)
        {
            IHittable hittable = closest.collider.gameObject.GetComponentInParent<IHittable>();

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

            if (hittable != null)
            {
                // Ask the hittable if this is a valid hit.
                if(hittable.IsValidHit(closest, firedFrom))
                {
                    hittable.Hit(transform, projectile.transform, closest, this);
                }
            }

            // Destroy the projectile.
            Destroy(projectile.gameObject);
        }

        lastBackPosition = projectileBack.position;
    }

    /// <summary>
    /// Launch the projectile and capture the source (firedFrom).
    /// </summary>
    /// <param name="forward"></param>
    /// <param name="firedFrom"></param>
    public void Fire(Vector3 forward, IHittable firedFrom)
    {
        projectile.velocity = forward * speed;
        lastBackPosition = projectileBack.position;
        this.firedFrom = firedFrom;

        Destroy(projectile.gameObject, 5f);
    }
}
