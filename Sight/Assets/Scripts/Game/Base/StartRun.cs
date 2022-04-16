using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRun : MonoBehaviour
{
    private CylinderChangeColor cylinderChangeColor;

    void Start()
    {
        cylinderChangeColor = GameObject.Find("Canvas").GetComponent<CylinderChangeColor>();
    }

    void Update()
    {
        if (cylinderChangeColor.activate_start == true)
        {

        }
    }
}
