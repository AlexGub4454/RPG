using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    
    Health health;
    // Start is called before the first frame update
    void Awake()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = String.Format("{0:0}%",  health.GetPersentage());
    }
}
