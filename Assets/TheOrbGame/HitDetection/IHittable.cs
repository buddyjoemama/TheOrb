using System;
using UnityEngine;

public interface IHittable
{
    bool Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile);
    Guid Id { get; }
    bool ShouldDestroy { get; }
    Quaternion EffectOrientation { get; }
    bool IsValidHit(RaycastHit hit, IHittable firedFrom);
}