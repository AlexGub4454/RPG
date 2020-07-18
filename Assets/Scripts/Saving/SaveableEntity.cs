using System;
using System.Collections.Generic;
using RPG.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Saving
{
   [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] string uniqueUdentifier ="";
        static Dictionary<string, SaveableEntity> globalKeys = new Dictionary<string, SaveableEntity>();  
        public string GetUniqueIndentefier()
        {
            return uniqueUdentifier;
        }

        public object CaptureState()
        {
            //   return new SerializableVector3(transform.position);
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach(ISaveable saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;
        }

        public void RestoreState(object state)
        {
            Dictionary<string,object> state1 =(Dictionary<string, object>)state;
            foreach (ISaveable saveable in GetComponents<ISaveable>())
            {
                string typeName = saveable.GetType().ToString();
                if (state1.ContainsKey(typeName))
                {
                    saveable.RestoreState(state1[typeName]);
                }
            }
        }
#if UNITY_EDITOR
        private void Update()
        {

            if (Application.IsPlaying(gameObject)) return;

            if (string.IsNullOrEmpty(gameObject.scene.path)) return;
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("uniqueUdentifier");
            if (string.IsNullOrEmpty(serializedProperty.stringValue) || !IsUnique(serializedProperty.stringValue))
            {
                serializedProperty.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
            globalKeys[serializedProperty.stringValue] = this;
       }
#endif

        private bool IsUnique(string stringValue)
        {
            if (!globalKeys.ContainsKey(stringValue)) return true;
            if (globalKeys[stringValue] == this) return true;
            if (globalKeys[stringValue] == null)
            {
                globalKeys.Remove(stringValue);
                return true;
            }
            if (globalKeys[stringValue].GetUniqueIndentefier() != stringValue)
            {
                globalKeys.Remove(stringValue);
                return true;
            }

            return false;
        }

    } 
}