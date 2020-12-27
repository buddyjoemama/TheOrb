using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    public string materialColorName = "HitColor";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        StartCoroutine(UpdateMaterial());
    }

    IEnumerator UpdateMaterial()
    {
        Material material = GetComponent<Renderer>().material;
        if(material.HasProperty(materialColorName))
        {
            Color currentColor = material.GetColor(materialColorName);

            while(material.GetColor(materialColorName).r <= .4f)
            {
                float amount = currentColor.r + (3.8f * .02f);

                currentColor.r = amount;
                material.SetColor(materialColorName, currentColor);

                yield return new WaitForFixedUpdate();
            }

            while (material.GetColor(materialColorName).r > 0f)
            {
                float amount = material.GetColor(materialColorName).r - (3.8f * .02f);

                currentColor.r = amount;
                material.SetColor(materialColorName, currentColor);

                yield return new WaitForFixedUpdate();
            }
        }

        yield return new WaitForEndOfFrame();
    }
}