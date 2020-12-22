using System;
using UnityEngine;

internal interface IHitable
{
    void Hit(Transform collider, Transform transform);
    Guid Id { get; set; }
}