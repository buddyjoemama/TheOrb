using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ILookAt
{
    public Vector3 GetLookAtPoint(RaycastHit raycastHit);
}