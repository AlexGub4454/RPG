using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;


namespace RPG.Saving
{
    public class SavingWrapper : MonoBehaviour
    {
        const string saveFileName = "save";
        private void Awake()
        {
            StartCoroutine(LoadingLevel());
        }
        IEnumerator LoadingLevel()
        {
          //  StartCoroutine(GetComponent<SavingSystem>().LoadLastScene());
         //   Fader fader = FindObjectOfType<Fader>();
         //  fader.FadeIntermediate();
           yield return GetComponent<SavingSystem>().LoadLastScene(saveFileName);
        //    yield return fader.FadeOut(0.3f);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }
        public void Load()
        {
            GetComponent<SavingSystem>().Load(saveFileName);
        }
        public void Save()
        {
            GetComponent<SavingSystem>().Save(saveFileName);
        }
    }
}