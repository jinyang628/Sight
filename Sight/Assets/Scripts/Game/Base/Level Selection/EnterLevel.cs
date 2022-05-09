using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class EnterLevel : MonoBehaviour
{
    string button_name;

    public void LoadLevelPogu()
    {
        button_name = EventSystem.current.currentSelectedGameObject.name;
        SceneManager.LoadScene(button_name);
    }
}
