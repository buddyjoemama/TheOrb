using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyTurret : MonoBehaviour
{
    public bool followsPlayer = true;
    public PlayerManager manager;
    public Transform turret;
    public BasicProjectile projectile;
    public Transform firePoint;
    public LineRenderer laserLine;
    public Vector3 raycastHit;
    public string colliderTag;
    public bool laserEnabled = true;
    private Player player;

    private LineRenderer laser;

    // Start is called before the first frame update
    void Start()
    {
        turret.LookAt(manager.player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(manager.player.transform.position);

        if(followsPlayer)   
            turret.LookAt(manager.player.transform.position);

        if (laser != null && laserEnabled)
        {
            if (Physics.Raycast(firePoint.position, turret.forward, out RaycastHit hit))
            {
                raycastHit = hit.point;

                colliderTag = hit.collider.tag;

                if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Shield"))
                {
                    laser.enabled = true;

                    //BasicProjectile clone = Instantiate(projectile, firePoint.transform.position, firePoint.rotation);
                    //clone.Fire(firePoint.forward);
                    laser.SetPositions(new Vector3[]
                    {
                    turret.position,
                    manager.player.transform.position
                    });
                }
                else
                {
                    laser.enabled = false;
                }
            }
        }
        else
        {
            laser = Instantiate(laserLine, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
