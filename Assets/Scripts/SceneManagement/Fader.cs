using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
       public IEnumerator FadeIn(float timeTofade)
        {
        
            while (canvasGroup.alpha<1)
            {
                canvasGroup.alpha +=  1/timeTofade*Time.deltaTime ;
             
                yield return null;
            }
            yield return null;
        }
        public void FadeIntermediate()
        {
            canvasGroup.alpha = 1f;
        }
        public IEnumerator FadeOut(float timeTofade)
        {

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= 1 / timeTofade * Time.deltaTime;

                yield return null;
            }
            yield return null;
        }
    }

}