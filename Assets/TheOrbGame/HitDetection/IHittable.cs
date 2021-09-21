using System;
using UnityEngine;

public interface IHittable
{
    void Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile);
    Guid Id { get; }
    String Tag { get; }
    bool ShouldDestroy { get; }
    Quaternion EffectOrientation { get; }
    bool IsValidHit(RaycastHit hit, IHittable firedFrom);
}