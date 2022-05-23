using System;
using UnityEngine;

public interface IHittable
{
    void Hit(Transform collider, RaycastHit hit, BasicProjectile projectile);
    Guid Id { get; }
    String Tag { get; }
    bool ShouldDestroy { get; }
    bool IsValidHit(RaycastHit hit, IHittable firedFrom);
}