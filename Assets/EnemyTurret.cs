using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class EnemyTurret : MonoBehaviour
{
    public bool followsPlayer = true;
    public PlayerManager manager;
    public Transform turret;
    public BasicProjectile projectile;
    public Transform firePoint;
    public LineRenderer laserLine;
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

        if (laser != null)
        {
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
            laser = Instantiate(laserLine, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }
}
