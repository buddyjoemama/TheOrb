﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GameManager : MonoBehaviour
{
    public Player player;
   // private Hittable hitablePlayer;
    private Image lifeBar;
    public Transform objectToCreate;

    public Camera mainCamera;

    private void Awake()
    {
        Physics.IgnoreLayerCollision(0, 3);

        // Set children properties
        foreach (var item in this.GetComponentsInChildren<IRequiresPlayer>())
        {
            item.Player = player;
        }
    }

    //// Start is called before the first frame update
    //void Awake()
    //{
    //    Physics.IgnoreLayerCollision(0, 3);
    //    //lifeBar = GetComponentInChildren<Image>();

    //    //hitablePlayer = player.GetComponent<Hittable>();
    //    //hitablePlayer.OnHit += HitablePlayer_OnHit;
    //    //hitablePlayer.OnHitPointsChanged += HitablePlayer_OnHitPointsChanged;

    //    //Cursor.visible = false;
    //    //StartCoroutine(CreateObject());
    //}


    public void FixedUpdate()
    {
    }

    private void HitablePlayer_OnHitPointsChanged()
    {
       // lifeBar.fillAmount = hitablePlayer.currentHitPoints / (float)hitablePlayer.maxHitPoints;
    }

    private void HitablePlayer_OnHit(int damage)
    {
     //   lifeBar.fillAmount = hitablePlayer.currentHitPoints / (float)hitablePlayer.maxHitPoints;
    }

    IEnumerator CreateObject()
    {
        yield return new WaitForSeconds(10);

        while (true)
        {
            if (objectToCreate != null)
                Instantiate(objectToCreate, new Vector3(-83, 15, 20), Quaternion.identity);

            yield return new WaitForSeconds(10f);
        }
    }
}
