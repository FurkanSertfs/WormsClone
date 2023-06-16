using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillBarAnimation : MonoBehaviour
{
    [SerializeField]
    Image image;

    private void Start() 
    {
       
        StartCoroutine(FillTheBar());
    }
    
   
   IEnumerator FillTheBar()
   {
    while (image.fillAmount < 1)
    {
        image.fillAmount += 0.01f;
        yield return new WaitForSeconds(0.01f);
    }

   }
}
