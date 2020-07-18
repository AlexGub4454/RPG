using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public IEnumerator LoadLastScene(string filePath)
        {
            Dictionary<string, object> state = LoadFile(filePath);
            if (state.ContainsKey("SceneIndexOfLastLoadedScene"))
            {
                int buildIndex = (int)state["SceneIndexOfLastLoadedScene"];
                if (buildIndex !=SceneManager.GetActiveScene().buildIndex)
                {
                    yield return SceneManager.LoadSceneAsync(buildIndex);
                }
            }
            RestoreState(state);
            
        }
        public void Save(string filePath)
        {
            Dictionary<string, object> state = LoadFile(filePath);
            CaptureState(state);
            SaveFile(filePath,state);
        }

       

        public void Load(string filePath)
        {
            RestoreState(LoadFile(filePath));
        }
        

        
        private Dictionary<string, object> LoadFile(string filePath)
        {
            string _path = GetPathFromSaveFile(filePath);
            if (!File.Exists(_path))
            {
                Debug.Log("Это ты тварь что ли все портишь");
                return new Dictionary<string, object>();
            }
            using (FileStream stream = File.Open(_path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }
        private void SaveFile(string filePath, object state)
        {
            string _path = GetPathFromSaveFile(filePath);
            Debug.Log("Saving..." + GetPathFromSaveFile(_path));
            using (FileStream stream = File.Open(_path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }
        private void CaptureState(Dictionary<string, object> stateDict)
        {
           
            foreach (SaveableEntity entity in FindObjectsOfType<SaveableEntity>())
            {
                stateDict[entity.GetUniqueIndentefier()] = entity.CaptureState();
            }
            stateDict["SceneIndexOfLastLoadedScene"] = SceneManager.GetActiveScene().buildIndex;
   
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity entity in FindObjectsOfType<SaveableEntity>())
            {
                Debug.Log("checking");
                string id = entity.GetUniqueIndentefier();
                Debug.Log(state.ContainsKey(id));

                if (state.ContainsKey(id))
                {
                    Debug.Log("wtf");
                    entity.RestoreState(state[id]);
                }
            }
        }
        string GetPathFromSaveFile(string path)
        {
          path += ".sav";
         return Path.Combine(Application.dataPath,path);
        }

        

    }   
}