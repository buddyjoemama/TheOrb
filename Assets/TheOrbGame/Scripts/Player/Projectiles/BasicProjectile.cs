using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

[ExecuteInEditMode]
public class BasicProjectile : MonoBehaviour
{
    private Rigidbody projectile;

    public int speed = 250;
    public Transform explosion;
    public LayerMask mask;
    public GameObject currentHitObject;
    public Vector3 origin;
    public Vector3 direction;
    public float max = 5;
    public float radius = 1;
    public Vector3 point;
    public Transform bulletMark;
    public float currentHitDistance;

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
        if (Physics.SphereCast(origin, radius, direction, out hit, max, mask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
            point = new Vector3(hit.point.x, hit.point.y, hit.point.z - max);

            Instantiate(explosion, point, Quaternion.Euler(180, 0, 0));
            Instantiate(bulletMark, point, Quaternion.identity);
            Destroy(projectile.gameObject);
        }
        else
        {
            currentHitDistance = 1;
            currentHitObject = null;
            point = origin;
        }
    }

    public void Fire(Vector3 forward)
    {
        projectile.velocity = forward * speed;
        Destroy(projectile.gameObject, 2f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, radius);
    }
}
