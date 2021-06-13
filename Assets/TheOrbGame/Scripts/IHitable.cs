using System;
using UnityEngine;

internal interface IHitable
{
    bool Hit(Transform collider, Transform transform, RaycastHit hit, BasicProjectile projectile);
    Guid Id { get; set; }
}