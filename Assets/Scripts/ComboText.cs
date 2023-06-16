using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComboText : MonoBehaviour
{
    public Text textObject;
    public float duration = 2f;
    public float fadeSpeed = 1f;

  

    Color startColor;

    private void Start()
    {
        
        startColor = textObject.color;
        Destroy(gameObject,2f);
    }

    public void ShowScoreText(int score)
    {
        if (score < 0)
        {
            startColor = Color.red;
        }
        else
        {
            startColor = Color.green;
        }

        textObject.color = startColor;


        textObject.text = score.ToString();
        
        

        StartCoroutine(FadeOut());


    }

    private IEnumerator FadeOut()
    {
        float startTime = Time.time;

        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (Time.time - startTime <= duration)
        {
            float t = (Time.time - startTime) / duration;
            textObject.color = Color.Lerp(startColor, endColor, t * fadeSpeed);
            transform.Translate(new Vector2(Random.Range(-2,2),1) * Time.deltaTime * 3f);
            yield return null;
        }

      
    }
}
