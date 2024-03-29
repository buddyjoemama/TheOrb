using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHitEffect : MonoBehaviour, IHitEffect
{
    private Color? _originalColor = null;
    private Coroutine coroutine;
    public Color HitColor = Color.red;

    public string OriginalColorPropertyName;

    private void Start()
    {
        _originalColor = GetComponent<Renderer>().material.GetColor(OriginalColorPropertyName);
    }

    public void Apply()
    {
        if (_originalColor == null)
        {
            _originalColor = GetComponent<Renderer>().material.GetColor(OriginalColorPropertyName);
        }

        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(ApplyEffect(OriginalColorPropertyName, _originalColor.Value, HitColor));
    }

    IEnumerator ApplyEffect(string varName, Color originalColor, Color hitColor)
    {
        GetComponent<Renderer>().material.SetColor(varName, hitColor);

        Color currentColor = GetComponent<Renderer>().material.GetColor(varName);

        while (currentColor != originalColor)
        {
            currentColor = Color.Lerp(currentColor, originalColor, Time.deltaTime / 1.5f);
            GetComponent<Renderer>().material.SetColor(varName, currentColor);

            yield return new WaitForEndOfFrame();
        }
    }
}