using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class CylinderChangeColor : MonoBehaviour, IPointerDownHandler
{
    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    public Material blackMaterial;
    public bool activate_start;

    string object_name;
    int index_object;
    List<string> removeList = new List<string>();
    List<string> pathHistory = new List<string>();

    private CylinderRelationships cylinderRelationships;

    private void Start()
    {
        activate_start = false;
        cylinderRelationships = GameObject.Find("Canvas").GetComponent<CylinderRelationships>();
        pathHistory.Add("S");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        object_name = eventData.pointerCurrentRaycast.gameObject.name;
        if (!pathHistory.Contains(object_name)) //not selected yet
        {
            if (cylinderRelationships.level_dict[pathHistory.Last()].Contains(object_name)) //valid cylinder
            {
                if (object_name == "E")
                {
                    GameObject.Find(object_name).GetComponent<MeshRenderer>().material = blueMaterial;
                    activate_start = true;
                }
                else
                {
                    GameObject.Find(object_name).GetComponent<MeshRenderer>().material = greenMaterial;
                }
                
                pathHistory.Add(object_name);
            }
        }
        else //selected already, includes starting cylinder
        {
            if (pathHistory.Count != 1 && object_name != pathHistory.Last()) //sth aside from starting cylinder/last selected object was selected
            {
                index_object = pathHistory.IndexOf(object_name);
                for (int i = (index_object + 1); i < pathHistory.Count; i++)
                {
                    if (pathHistory[i] == "E")
                    {
                        GameObject.Find(pathHistory[i]).GetComponent<MeshRenderer>().material = blackMaterial;
                        activate_start = false;
                    }
                    else
                    {
                        GameObject.Find(pathHistory[i]).GetComponent<MeshRenderer>().material = redMaterial;
                    }
                    removeList.Add(pathHistory[i]);

                }

                foreach (string cylinder in removeList)
                {
                    pathHistory.Remove(cylinder);
                }
                removeList = new List<string>();
            }        
        }
        
    }
}

