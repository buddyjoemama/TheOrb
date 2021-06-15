using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private string materialColorName = "ActivatedColor";
    private Color originalColor;
    private Color targetColor;

    // Start is called before the first frame update
    void Start()
    {
        targetColor = originalColor = GetComponent<Renderer>().material.GetColor(materialColorName);    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Material material = GetComponent<Renderer>().material;
        //Color matColor = material.GetColor(materialColorName);

       // Color color = Color.Lerp(matColor, targetColor, 1f);        
        //material.SetColor(materialColorName, color);
        
        //if(color == targetColor)
        //{
        //    targetColor = originalColor;
       // }
    }

    public void Hit(Color hitColor)
    {
        targetColor = hitColor;
    }
}