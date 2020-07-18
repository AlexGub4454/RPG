using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;
using RPG.Combat;
using RPG.Movement;

namespace RPG.Cinematic
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            GetComponent<PlayableDirector>().played += OnEnabled;
            GetComponent<PlayableDirector>().stopped += OnFinished;
        }

        void OnEnabled(PlayableDirector pd)
        {
            player.GetComponent<Fighter>().Cancel();
            player.GetComponent<Mover>().Cancel();
            player.GetComponent<PlayerController>().enabled = false;
        }
        void OnFinished(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
