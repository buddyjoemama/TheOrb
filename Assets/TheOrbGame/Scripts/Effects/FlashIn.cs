using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashIn : MonoBehaviour
{
    private Material material;
    public Material transitionMaterial;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateColor());
    }

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    IEnumerator UpdateColor()
    {
        Color currentColor = new Color(5, 5, 5);

        while (currentColor.r >= 0.0f)
        {
            currentColor.r = currentColor.g = currentColor.b = (currentColor.r -= .1f);
            material.color = currentColor;

            yield return new WaitForEndOfFrame();
        }

        if(transitionMaterial != null)
            GetComponent<Renderer>().material = transitionMaterial;
    }
}
