using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.UIElements;
using UnityEngine;

public class Hitable : MonoBehaviour, IHitable
{
    public int maxHitPoints;
    public int currentHitPoints;
    public PlayerManager player;
    private bool hit = false;
    private bool reverse = false;
    public List<Transform> effects;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void Awake()
    {
        currentHitPoints = maxHitPoints;
    }

    bool destroyed = false;
    // Update is called once per frame
    void Update()
    {
        if (currentHitPoints == 0)
        {
            if (effects != null && effects.Count > 0 && !destroyed)
            {
                destroyed = true;
                Transform r = effects[Random.Range(0, effects.Count - 1)];
                Transform shatterEffect = Instantiate(r, transform.position, player.rig.firePoint.rotation);

                Destroy(shatterEffect.gameObject, 5f);
            }

            Destroy(gameObject);
        }
    }

    public void FixedUpdate()
    {
        if (hit)
        {
            Material material = GetComponent<Renderer>().material;
            Color currentColor = material.GetColor("HitColor");

            if (material.GetColor("HitColor").r <= .4f && !reverse)
            {
                float amount = currentColor.r + (3.8f * Time.deltaTime);

                currentColor.r = amount;
                material.SetColor("HitColor", currentColor);
            }
            else
            {
                reverse = true;
            }

            if (reverse)
            {
                if (material.GetColor("HitColor").r >= 0f)
                {
                    float amount = material.GetColor("HitColor").r - (3.8f * Time.deltaTime);

                    currentColor.r = amount;
                    material.SetColor("HitColor", currentColor);
                    hit = material.GetColor("HitColor").r > 0f;
                }
            }
        }
    }

    public void Hit(Transform collider, Transform transform)
    {
        hit = true;
        reverse = false;
        currentHitPoints -= 1;
    }
}
