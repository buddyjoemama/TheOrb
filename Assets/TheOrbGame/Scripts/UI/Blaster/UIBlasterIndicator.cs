using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlasterIndicator : MonoBehaviour, IRequiresPlayer
{
    public float ShotFillAmount = .11f;
    public float ShotFalloff = .01f;
    public float FillReductionFactor = .35f;

    public Player Player { get; set; }
    private Image _image;

    private bool requireCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        Player.OnShotFired += (p) => 
        {
            float reduceAmt = _image.fillAmount * FillReductionFactor;
            var val = ShotFillAmount - (ShotFillAmount * reduceAmt);

            _image.fillAmount += val;
        };

        _image = GetComponent<UnityEngine.UI.Image>();
        _image.fillAmount = 0;
    }    

    // Update is called once per frame
    void Update()
    {
        if(_image.fillAmount == 1)
        {
            Player.DisableFiring();
        }
        else if(_image.fillAmount == 0)
        {
            Player.EnableFiring();
        }

        if(Player.FiringEnabled)
        {
            _image.color = Color.Lerp(Color.white, Color.red, _image.fillAmount);
        }
        else
        {
            _image.color = Color.red;
        }

        _image.fillAmount -= ShotFalloff;
        

       /* if (_image.fillAmount < 1 && !requireCooldown)
        {
            _image.fillAmount -= ShotFalloff;
            _image.color = Color.Lerp(Color.white, Color.red, _image.fillAmount);
        }
        else if(_image.fillAmount == 1)
        {
            requireCooldown = true;
        }
        else if(_image.fillAmount == 0)
        {
            requireCooldown = false;
        }*/
    }
}
