using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SphereCaster : MonoBehaviour
{

    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    GameObject currentHitObject;
    // Update is called once per frame
    void Update()
    {
        origin = transform.position;
        direction = transform.forward;

        RaycastHit hit;
        if(Physics.SphereCast(origin, .5f, direction, out hit, 5, mask, QueryTriggerInteraction.UseGlobal))
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDistance = hit.distance;
        }
        else
        {
            currentHitDistance = 5;
            currentHitObject = null;
        }
    }

    Vector3 origin;
    Vector3 direction;
    public float currentHitDistance;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, 5);
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
