using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using System;
using UnityEngine.UI;
using RPG.Combat;

public class EnemyHealthDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Health health;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
       
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>().GetTarget();
        if (health == null)
        {
            GetComponent<Text>().text = "N/A";
            return;
        }
        GetComponent<Text>().text = String.Format("{0:0}%", health?.GetPersentage());

    }
}
