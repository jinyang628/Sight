using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class CylinderChangeColor : MonoBehaviour, IPointerDownHandler
{
    public GameObject pathTree;
    public GameObject endTree;
    public bool activate_start;
    public List<string> pathHistory = new List<string>();

    string object_name;
    int index_object;
    List<string> removeList = new List<string>();

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
                    var x = cylinderRelationships.tree_dict["E"];
                    var tree = Instantiate(endTree, new Vector3(x[0], 0, x[1]), Quaternion.identity);
                    tree.name = "ETree";
                    activate_start = true;
                }
                else
                {
                    var x = cylinderRelationships.tree_dict[object_name];
                    var tree = Instantiate(pathTree, new Vector3(x[0], 0, x[1]), Quaternion.identity);
                    tree.name = string.Format("{0}Tree", object_name);
                }
                
                pathHistory.Add(object_name);
            }
        }
        else //selected already, includes starting cylinder
        {
            if (pathHistory.Count != 1 && object_name != pathHistory.Last()) //sth aside from starting cylinder/last selected object was selected
            {
                index_object = pathHistory.IndexOf(object_name);
                if (index_object != 0 || (index_object == 0 && pathHistory.Last() != "E"))
                {
                    for (int i = (index_object + 1); i < pathHistory.Count; i++)
                    {
                        var x = pathHistory[i];
                        if (x == "E")
                        {
                            Destroy(GameObject.Find("ETree"));
                            activate_start = false;
                        }
                        else
                        {
                            Destroy(GameObject.Find(string.Format("{0}Tree", x)));
                        }
                        removeList.Add(x);

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
}

