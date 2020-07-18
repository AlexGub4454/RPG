using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using RPG.Saving;
namespace RPG.SceneManagement
{
    public enum portalTags
    {
        A,B,C,D,E,F,G
    }
    public class Portal : MonoBehaviour
    {
        [SerializeField] int loadSceneIndex;
        [SerializeField] Transform spawnPoint;
        [SerializeField] portalTags portalTag;
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(LoadSceneAsync());
            }
        }
        private void UpdatePortal(Portal otherPortal)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled = false;
            player.transform.position = otherPortal.spawnPoint.position;
            player.transform.rotation = otherPortal.spawnPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled = true;

        }
        Portal GetPortal()
        {

            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portalTag != portal.portalTag) continue;
                return portal;
            }
            return null;
        }
        IEnumerator LoadSceneAsync()
        {
          //  Fader fader = FindObjectOfType<Fader>();
            DontDestroyOnLoad(gameObject);
            
         //   yield return fader.FadeIn(0.7f);
           SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save();
            yield return SceneManager.LoadSceneAsync(loadSceneIndex);
           
            wrapper.Load();
           // yield return new WaitForSeconds(0.5f);
       
            Portal otherPortal = GetPortal();
            UpdatePortal(otherPortal);
            wrapper.Save();
            //  yield return fader.FadeOut(0.5f);
            Destroy(gameObject);
            
        }
    }
}
