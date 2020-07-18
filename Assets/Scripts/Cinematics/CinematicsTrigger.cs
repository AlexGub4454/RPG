using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace RPG.Cinematic
{
    public class CinematicsTrigger : MonoBehaviour
    {
        bool isAlreadyPlayed = false;
        private void OnTriggerEnter(Collider other)
        {
            if (isAlreadyPlayed || other.tag!="Player") return;
            GetComponent<PlayableDirector>().Play();
            isAlreadyPlayed = true;
        }
    }
}
