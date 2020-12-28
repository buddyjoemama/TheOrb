using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    private Hitable hitablePlayer;
    private Image lifeBar;

    // Start is called before the first frame update
    void Start()
    {
        lifeBar = GetComponentInChildren<Image>();

        hitablePlayer = player.GetComponent<Hitable>();
        hitablePlayer.OnHit += HitablePlayer_OnHit;
        hitablePlayer.OnHitPointsChanged += HitablePlayer_OnHitPointsChanged;
    }

    private void HitablePlayer_OnHitPointsChanged()
    {
        lifeBar.fillAmount = hitablePlayer.currentHitPoints / (float)hitablePlayer.maxHitPoints;
    }

    private void HitablePlayer_OnHit(int damage)
    {
        lifeBar.fillAmount = hitablePlayer.currentHitPoints / (float)hitablePlayer.maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    //IEnumerator CreateObject()
    //{
    //    while(true)
    //    {
    //        if(objectToCreate != null)
    //            Instantiate(objectToCreate, new Vector3(0, 20, 50), Quaternion.identity);

    //        yield return new WaitForSeconds(5f);
    //    }
    //}
}
