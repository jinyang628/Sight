using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstantiateTime : MonoBehaviour
{
    public GameObject InstructionCanvas;
    string char_name;
    string value_in_string;
    string scene_name;
    
    private void Start()
    {
        scene_name = SceneManager.GetActiveScene().name;
    }

    public void DelayInstantiate()
    {
        if (scene_name == "Level 3")
        {
            if (GameObject.Find("TimeStart"))
            {
                GameObject.Find("TimeStart").SetActive(false);
                InstructionCanvas.SetActive(true);
            }
        }
        char_name = EventSystem.current.currentSelectedGameObject.name;
        value_in_string = GameObject.Find(char_name).GetComponent<Text>().text;
        if (value_in_string == "0")
        {
            value_in_string = "1";
        }
        else if (value_in_string == "1")
        {
            value_in_string = "2";
        }
        else if (value_in_string == "2")
        {
            value_in_string = "3";
        }
        else
        {
            value_in_string = "0";
        }

        GameObject.Find(char_name).GetComponent<Text>().text = value_in_string;
    }
}
