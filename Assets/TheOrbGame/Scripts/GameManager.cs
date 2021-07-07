using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    private Hitable hitablePlayer;
    private Image lifeBar;
    public Transform objectToCreate;

    // Start is called before the first frame update
    void Start()
    {
        lifeBar = GetComponentInChildren<Image>();

        hitablePlayer = player.GetComponent<Hitable>();
        hitablePlayer.OnHit += HitablePlayer_OnHit;
        hitablePlayer.OnHitPointsChanged += HitablePlayer_OnHitPointsChanged;

        StartCoroutine(CreateObject());
    }

    private void HitablePlayer_OnHitPointsChanged()
    {
        lifeBar.fillAmount = hitablePlayer.currentHitPoints / (float)hitablePlayer.maxHitPoints;
    }

    private void HitablePlayer_OnHit(int damage)
    {
        lifeBar.fillAmount = hitablePlayer.currentHitPoints / (float)hitablePlayer.maxHitPoints;
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
