using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public int DelaySeconds = 5;

    // Start is called before the first frame update
    void Start()
    {
        var mat = this.GetComponent<Renderer>().material;
        StartCoroutine(Fade(mat, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Fade(Material mat, float targetAlpha)
    {
        yield return new WaitForSeconds(DelaySeconds);

        int scale = UnityEngine.Random.Range(1, 8);

        while (mat.color.a != targetAlpha)
        {
            var newAlpha = Mathf.MoveTowards(mat.color.a, targetAlpha, Time.deltaTime / scale);
            mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, newAlpha);
            yield return null;
        }
    }
}
