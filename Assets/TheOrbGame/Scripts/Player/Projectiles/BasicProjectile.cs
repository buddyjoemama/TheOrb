using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

[ExecuteInEditMode]
public class BasicProjectile : MonoBehaviour
{
    private Rigidbody projectile;
    private Vector3 origin;
    public  Vector3 direction;
    private Vector3 hitPoint;
    private float currentHitDistance;

    public int speed = 250;
    public Transform explosion;
    public LayerMask mask;
    public GameObject currentHitObject;
    public float max = 5;
    public float radius = 1;
    public Transform bulletMark;

    private void Awake()
    {
        projectile = GetComponent<Rigidbody>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        origin = projectile.transform.position;
        direction = projectile.transform.forward;

        RaycastHit hit;
        if (Physics.SphereCast(origin - new Vector3(0, 0, 5), radius, direction, out hit, max, mask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            hitPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z - .5f);

            if (explosion != null)
                Instantiate(explosion, hitPoint, Quaternion.Euler(180, 0, 0));

            if (bulletMark != null)
                Instantiate(bulletMark, hitPoint, Quaternion.identity);

            Destroy(projectile.gameObject);

            var hittable = hit.collider.gameObject.GetComponent<Hitable>();
            if (hittable != null)
            {
                hittable.Hit(transform, projectile.transform);
            }
        }
        else
        {
            currentHitDistance = 1;
            currentHitObject = null;
            hitPoint = origin;
        }

    }

    public void Fire(Vector3 forward, Vector3 firedFrom)
    {
        projectile.velocity = forward * speed;
        Destroy(projectile.gameObject, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin  + direction * currentHitDistance, radius);
    }
}
