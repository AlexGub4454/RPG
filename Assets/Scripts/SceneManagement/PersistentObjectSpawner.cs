using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentObjectSpawner : MonoBehaviour
{
    [SerializeField] GameObject persistentObject;
    static bool isInstance = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (isInstance) return;
        GameObject @object = Instantiate(persistentObject);
        DontDestroyOnLoad(@object);
        isInstance= true;
    }

   
}
