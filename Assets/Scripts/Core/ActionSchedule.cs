using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionSchedule : MonoBehaviour
    {
        IAction currentState;

        public void StartAction(IAction state)
        {
            
            if (currentState == state) return;
            if (currentState != null)
            {
                Debug.Log("Calling"+state);
                currentState.Cancel();
              
            }
            currentState = state;
        }
        public void SetCurrentActionNull()
        {
            StartAction(null);
        }
    }
}
