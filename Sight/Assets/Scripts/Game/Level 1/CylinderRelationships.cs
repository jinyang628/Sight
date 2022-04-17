using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderRelationships : MonoBehaviour
{

    public Dictionary<string, List<string>> level_dict;
    public Dictionary<string, List<int>> tree_dict;

    void Start()
    {
        level_dict = new Dictionary<string, List<string>>()
        {
            {"S", new List<string> { "2" } },
            {"2", new List<string> { "3", "S" } },
            {"3", new List<string> { "4", "2" } },
            {"4", new List<string> { "E", "3" } }
        };

        tree_dict = new Dictionary<string, List<int>>()
        {
            {"2", new List<int>{0, 14} },
            {"3", new List<int>{14, 14} },
            {"4", new List<int>{14, 28} },
            {"E", new List<int>{14, 42} },
        };
    }
 
}
