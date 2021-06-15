using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleHitEffect : MonoBehaviour
{
    private Color? originalColor = null;
    private Coroutine coroutine;

    public void Apply(string varName, Color hitColor)
    {
        if (originalColor == null)
        {
            originalColor = GetComponent<Renderer>().material.GetColor(varName);
        }

        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(ApplyEffect(varName, originalColor.Value, hitColor));
    }

    IEnumerator ApplyEffect(string varName, Color originalColor, Color hitColor)
    {
        GetComponent<Renderer>().material.SetColor(varName, hitColor);

        Color currentColor = GetComponent<Renderer>().material.GetColor(varName);

        while (currentColor != originalColor)
        {
            currentColor = Color.Lerp(currentColor, originalColor, Time.deltaTime * 5);
            GetComponent<Renderer>().material.SetColor(varName, currentColor);
            yield return null;
        }
    }
}