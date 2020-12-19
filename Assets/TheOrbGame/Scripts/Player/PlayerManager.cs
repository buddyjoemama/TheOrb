using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[ExecuteInEditMode]
public class PlayerManager : MonoBehaviour
{
    public Player player;
    public Shield mainShield;
    public GunRig rig;
    public BasicProjectile projectile;
    public bool shieldEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shieldEnabled)
        {
            EnableShield();
        }
        else
        {
            DisableShield();
        }
    }

    private void FixedUpdate()
    {
        mainShield.Move(player.transform.position);
        rig.Move(player.transform.position);
    }

    /// <summary>
    /// Fire the projectile.
    /// </summary>
    void OnFire() 
    {
        rig.Fire(projectile);
    }

    /// <summary>
    /// Move the player ball.
    /// </summary>
    /// <param name="move"></param>
    private void OnMove(InputValue move)
    {
        Vector2 movementVector = move.Get<Vector2>();
        player.Move(movementVector);
    }

    public void EnableShield()
    {
        mainShield.Activate();
    }

    public void DisableShield()
    {
        mainShield.Deactivate();
    }
}
