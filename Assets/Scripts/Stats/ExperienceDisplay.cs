using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    [SerializeField] float experience;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        experience = (float)GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>().CaptureState();
        GetComponent<Text>().text = experience.ToString();
    }
}