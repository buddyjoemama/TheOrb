using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Extends hit detection without using a collider
/// by delegating to the associated rigidbody.
/// </summary>
public interface IHitAction
{
    void Apply(UnityEngine.Transform collider, UnityEngine.RaycastHit hit, BasicProjectile projectile);
}