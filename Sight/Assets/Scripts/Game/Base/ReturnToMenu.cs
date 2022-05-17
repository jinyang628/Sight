using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour
{
    float nextUsage;

    void Start()
    {
        nextUsage = Time.time + 2;
    }

    private void Update()
    {
        if (Time.time > nextUsage)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
