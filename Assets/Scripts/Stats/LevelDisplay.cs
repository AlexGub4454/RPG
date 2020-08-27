using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] float level;

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {

        level = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>().CalculateLevel();
        GetComponent<Text>().text = level.ToString();
    }
}
