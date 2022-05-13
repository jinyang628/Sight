using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class CylinderChangeColor : MonoBehaviour, IPointerDownHandler
{
    public GameObject CharacterSelection;
    public GameObject TimeStart;
    public GameObject pathTree;
    public GameObject endTree;
    public bool activate_start;
    public List<string> removeList = new List<string>();
    public bool c1_path;
    public bool c2_path;
    public bool c3_path;
    public List<string> pathHistory;
    public List<string> c1_pathHistory;
    public List<string> c2_pathHistory;
    public List<string> c3_pathHistory;

    string object_name;
    string cursor_name;
    string cursor_number;
    string scene_name;
    string char_name;
    GameObject object_ob;
    int index_object;
    
    private MoveCharacter moveCharacter;

    private void Start()
    {
        activate_start = false;
        moveCharacter = GameObject.Find("StartButton").GetComponent<MoveCharacter>();
        scene_name = SceneManager.GetActiveScene().name;
        c1_path = true;
        c2_path = false;
        c3_path = false;
        pathHistory = new List<string>() { "S" };
        c1_pathHistory = new List<string>() { "S" };
        c2_pathHistory = new List<string>() { "S" };
        c3_pathHistory = new List<string>() { "S" };
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        object_ob = eventData.pointerCurrentRaycast.gameObject;
        object_name = object_ob.name;
        if (!pathHistory.Contains(object_name))
        {
            if (pathHistory.Count >= 2 && pathHistory.Last().Contains("T") && pathHistory[pathHistory.Count-2].Contains("T"))
            {
                if (moveCharacter.teleport_dict[pathHistory.Last()].Contains(object_name)) //valid cylinder
                {
                    if (object_name == "E")
                    {
                        var x = moveCharacter.tree_dict["E"];
                        var tree = Instantiate(endTree, new Vector3(x[0], 0, x[1]), Quaternion.identity);
                        tree.name = "ETree";
                    }
                    else
                    {                          
                        var x = moveCharacter.tree_dict[object_name];
                        var tree = Instantiate(pathTree, new Vector3(x[0], 0, x[1]), Quaternion.identity);
                        tree.name = string.Format("{0}Tree", object_name);
                    }

                    AddtoPath();
                }
            }
            
            else
            {
                if (moveCharacter.level_dict[pathHistory.Last()].Contains(object_name)) //valid cylinder
                {
                    if (scene_name == "Level 1")
                    {
                        if (object_name == "E")
                        {
                            cursor_name = "5";
                        }

                        else
                        {
                            cursor_name = object_name;
                        }
                        Color i = GameObject.Find(string.Format("Cursor{0}", cursor_name)).GetComponent<Image>().color;
                        GameObject.Find(string.Format("Cursor{0}", cursor_name)).GetComponent<Image>().color = new Color(i.r, i.g, i.b, 0);
                    }

                    if (object_name == "E")
                    {
                        if (scene_name == "Level 1")
                        {
                            Color j = GameObject.Find("CursorS").GetComponent<Image>().color;
                            GameObject.Find("CursorS").GetComponent<Image>().color = new Color(j.r, j.g, j.b, 1);
                        }
                        var x = moveCharacter.tree_dict["E"];
                        var tree = Instantiate(endTree, new Vector3(x[0], 0, x[1]), Quaternion.identity);
                        tree.name = "ETree";

                    }
                    else
                    {
                        if (scene_name == "Level 1")
                        {
                            Color j = GameObject.Find(string.Format("Cursor{0}", Convert.ToInt32(object_name) + 1)).GetComponent<Image>().color;
                            GameObject.Find(string.Format("Cursor{0}", Convert.ToInt32(object_name) + 1)).GetComponent<Image>().color = new Color(j.r, j.g, j.b, 1);
                        }
 
                        var y = moveCharacter.tree_dict[object_name];
                        var tree1 = Instantiate(pathTree, new Vector3(y[0], 0, y[1]), Quaternion.identity);
                        tree1.name = string.Format("{0}Tree", object_name);
                        
                    }

                    AddtoPath();
                }
            }

            

        }
        else
        {
            if (object_name != pathHistory.Last()) //sth aside from starting cylinder/last selected object was selected
            {
                if (scene_name == "Level 1")
                {
                    if (object_name == "S")
                    {
                        Color z = GameObject.Find("Cursor2").GetComponent<Image>().color;
                        GameObject.Find("Cursor2").GetComponent<Image>().color = new Color(z.r, z.g, z.b, 1);
                    }
                    else if (object_name == "2")
                    {
                        Color z = GameObject.Find("Cursor3").GetComponent<Image>().color;
                        GameObject.Find("Cursor3").GetComponent<Image>().color = new Color(z.r, z.g, z.b, 1);
                    }
                    else if (object_name == "3")
                    {
                        Color z = GameObject.Find("Cursor4").GetComponent<Image>().color;
                        GameObject.Find("Cursor4").GetComponent<Image>().color = new Color(z.r, z.g, z.b, 1);
                    }
                    else if (object_name == "4")
                    {
                        Color z = GameObject.Find("Cursor5").GetComponent<Image>().color;
                        GameObject.Find("Cursor5").GetComponent<Image>().color = new Color(z.r, z.g, z.b, 1);
                    }   
                }

                index_object = pathHistory.IndexOf(object_name);
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
                        if (x.Substring(x.Length - 1) == "T")
                        {
                            Destroy(GameObject.Find(string.Format("{0}Tree", moveCharacter.teleport_dict[x])));
                        }
                    }
                    removeList.Add(x);
                }

                if (scene_name == "Level 1")
                {
                    cursor_number = (pathHistory.Count + 1).ToString();
                    if (cursor_number == "6")
                    {
                        cursor_number = "S";
                    }
                    Color y = GameObject.Find(string.Format("Cursor{0}", cursor_number)).GetComponent<Image>().color;
                    GameObject.Find(string.Format("Cursor{0}", cursor_number)).GetComponent<Image>().color = new Color(y.r, y.g, y.b, 0);
                }

                foreach (string cylinder in removeList)
                {
                    pathHistory.Remove(cylinder);
                    if (c1_path == true)
                    {
                        c1_pathHistory.Remove(cylinder);
                    }
                    else if (c2_path == true)
                    {
                        c2_pathHistory.Remove(cylinder);
                    }
                    else
                    {
                        c3_pathHistory.Remove(cylinder);
                    }
                }
                removeList = new List<string>();    
            }        
        }  
    }

    public void SelectCharacter()
    {
        char_name = EventSystem.current.currentSelectedGameObject.name;
        foreach (Transform c_button in CharacterSelection.transform)
        {
            if (c_button.name != char_name)
            {
                c_button.GetComponent<Image>().color = new Color(1, 0.04129943f, 0, 1); //red
            }
            else
            {
                c_button.GetComponent<Image>().color = new Color(0.1083024f, 1, 0, 1); //green
            }
        }

        if (char_name.Substring(char_name.Length - 1) == "1" && c1_path == false)
        {
           c1_path = true;
            if (c2_path == true)
            {
                DestroyTrees();
                c2_path = false;
            }
            else
            {
                DestroyTrees();
                c3_path = false;
            }
            pathHistory = new List<string>(c1_pathHistory);
            InstantiateTrees();

        }
        else if (char_name.Substring(char_name.Length - 1) == "2" && c2_path == false)
        {
            if (scene_name == "Level 3")
            {   
                if (GameObject.Find("PathSelection"))
                {
                    GameObject.Find("PathSelection").SetActive(false);
                    TimeStart.SetActive(true);
                }
            }

            c2_path = true;

            if (c1_path == true)
            {
                DestroyTrees();
                c1_path = false;
            }
            else
            {
                DestroyTrees();
                c3_path = false;
            }
            pathHistory = new List<string>(c2_pathHistory);
            InstantiateTrees();
        }
        else if (char_name.Substring(char_name.Length - 1) == "3" && c3_path == false)
        {
            c3_path = true;
            if (c1_path == true)
            {
                DestroyTrees();
                c1_path = false;
            }
            else
            {
                DestroyTrees();
                c2_path = false;
            }
            pathHistory = new List<string>(c3_pathHistory);
            InstantiateTrees();
        }
    }

    void DestroyTrees()
    {
        for (int i = 1; i < pathHistory.Count; i++)
        {
            string x = pathHistory[i];
            Destroy(GameObject.Find(string.Format("{0}Tree", x)));
            if (x.Substring(x.Length - 1) == "T")
            {
                Destroy(GameObject.Find(string.Format("{0}Tree", moveCharacter.teleport_dict[x])));
            }  
        }
    }

    void InstantiateTrees()
    {
        for (int i = 1; i < pathHistory.Count; i++)
        {
            string x = pathHistory[i];
            if (x == "E")
            {
                var y = moveCharacter.tree_dict["E"];
                var tree = Instantiate(endTree, new Vector3(y[0], 0, y[1]), Quaternion.identity);
                tree.name = "ETree";
            }
            else
            {
                var y = moveCharacter.tree_dict[x];
                if (x.Substring(x.Length - 1) == "T")
                {
                    var tree1 = Instantiate(pathTree, new Vector3(y[0], 0, y[1]), Quaternion.identity);
                    tree1.name = string.Format("{0}Tree", x);
                }
                else
                {         
                    var tree = Instantiate(pathTree, new Vector3(y[0], 0, y[1]), Quaternion.identity);
                    tree.name = string.Format("{0}Tree", x);
                }
            }
                
        }
    }

    void AddtoPath()
    {
        pathHistory.Add(object_name);
        if (c1_path == true)
        {
            c1_pathHistory.Add(object_name);
        }
        else if (c2_path == true)
        {
            c2_pathHistory.Add(object_name);
        }
        else
        {
            c3_pathHistory.Add(object_name);
        }

        if (c1_pathHistory[c1_pathHistory.Count - 1] == "E")
        {
            if (moveCharacter.player_count == 1)
            {
                activate_start = true;
            }
            else
            {
                if (c2_pathHistory[c2_pathHistory.Count - 1] == "E")
                {
                    if (moveCharacter.player_count == 2)
                    {
                        activate_start = true;
                    }
                    else
                    {
                        if (c3_pathHistory[c3_pathHistory.Count - 1] == "E")
                        {
                            activate_start = true;
                        }
                    }
                }
            }
        }
    }
}

