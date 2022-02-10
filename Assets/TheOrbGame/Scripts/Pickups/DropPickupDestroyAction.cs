using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DropPickupDestroyAction : MonoBehaviour, IDestroyAction
{
    public int PercentDropRate = 25;
    public Pickup PickupType;

    public void Apply()
    {
        var t = UnityEngine.Random.Range(1, 100);

        if (t <= PercentDropRate)
            Instantiate(PickupType, this.transform.position, Quaternion.identity);
    }
}