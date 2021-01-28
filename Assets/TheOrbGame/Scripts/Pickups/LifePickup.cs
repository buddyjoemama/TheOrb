using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class LifePickup : Pickup
{
    public int lifeValue;
    public Transform effect;
    public float timeToLive;
    public VisualEffect p;
    private bool pickedUp = false;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, timeToLive);

        this.GetComponent<VisualEffect>().Play();     
    }

    Vector3 velocity = Vector3.zero;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,
            new Vector3(transform.position.x, 2.5f, transform.position.z), ref velocity, 1f);
    }

    public override void Apply(Player player)
    {
        if (!pickedUp)
        {
            pickedUp = true;

            player.GetComponentInChildren<Hitable>().AddHitPoint(lifeValue);
            var o = Instantiate(effect, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(o.gameObject, 2.5f);
            Destroy(this.gameObject);
        }
    }
}
