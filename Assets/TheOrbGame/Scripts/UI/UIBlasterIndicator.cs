using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlasterIndicator : MonoBehaviour, IRequiresPlayer
{
    public float ShotFillAmount = .11f;
    public float ShotFalloff = .01f;

    public Player Player { get; set; }
    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        Player.OnShotFired += (p) => 
        {
            _image.fillAmount += ShotFillAmount;
        };

        _image = GetComponent<UnityEngine.UI.Image>();
        _image.fillAmount = 0;
    }    

    // Update is called once per frame
    void Update()
    {
        _image.fillAmount -= ShotFalloff;
        _image.color = Color.Lerp(Color.white, Color.red, _image.fillAmount);
    }
}
