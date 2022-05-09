using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTint : MonoBehaviour
{
    float time_delay = 0.5f;
    float nextUsage = 1000000f;
    float nextUsage2 = 1f;

    void Update()
    {
        if (Time.time > nextUsage2)
        {
            nextUsage2 = 1000000f;
            nextUsage = Time.time + time_delay;
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, gameObject.GetComponent<Image>().color.a);
        }

        if (Time.time > nextUsage)
        {
            nextUsage = 1000000f;
            nextUsage2 = Time.time + time_delay;
            gameObject.GetComponent<Image>().color = new Color((float)0.5, (float)0.5, (float)0.5, gameObject.GetComponent<Image>().color.a);
        }
    }
}
