using UnityEngine;

internal interface IHitable
{
    void Hit(Transform collider);
}