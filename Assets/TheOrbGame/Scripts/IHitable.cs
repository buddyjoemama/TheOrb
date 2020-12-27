using System;
using UnityEngine;

internal interface IHitable
{
    void Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile);
    Guid Id { get; set; }
}