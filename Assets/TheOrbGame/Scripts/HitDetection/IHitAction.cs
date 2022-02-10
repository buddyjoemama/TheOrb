using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IHitAction
{
    void Apply(UnityEngine.Transform collider, UnityEngine.Transform transform, UnityEngine.RaycastHit hit, BasicProjectile projectile);
}