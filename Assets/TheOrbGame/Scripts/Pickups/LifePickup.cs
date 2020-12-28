using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePickup : Pickup
{
    public int lifeValue;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 5f);
    }

    Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(transform.position.x, 0, transform.position.z), ref velocity, 1f);
    }

    public override void Apply(Player player)
    {
        player.GetComponentInChildren<Hitable>().AddHitPoint(lifeValue);
        Destroy(this.gameObject);
    }
}
