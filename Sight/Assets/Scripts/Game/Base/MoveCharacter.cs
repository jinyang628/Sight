using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveCharacter : MonoBehaviour, IPointerDownHandler
{
    public GameObject Instruction_Canvas;
    public GameObject success_notification;
    public GameObject time_counter;
    public GameObject instruction;  
    public GameObject CharacterSelection;
    public GameObject InstantiateTime;
    public GameObject C1InstantiateTime;
    public GameObject C2InstantiateTime;
    public GameObject C3InstantiateTime;
    public Dictionary<string, List<string>> level_dict;
    public Dictionary<string, List<int>> tree_dict;
    public Dictionary<string, List<string>> teleport_dict;
    public int player_count;

    private CylinderChangeColor cylinderChangeColor;
    private float speed = 8f;

    GameObject character1;
    GameObject PathSelection;
    GameObject nextDestination;
    bool start_turn = false;
    bool trigger_1 = false;
    bool trigger_2 = false;
    bool trigger_3 = false;
    bool start_move_1;
    bool start_move_2;
    bool start_move_3;
    bool finish_move_1;
    bool finish_move_2;
    bool finish_move_3;
    bool next_level_delay = false;
    string inst_time_1;
    string inst_time_2;
    string inst_time_3;
    string scene_name;
    string cursor_number;
    string counter_character_name = "NOT SET";
    bool first_counter = true;
    int time_passed;
    int destroyed_counter = 0;
    int next_scene_load;
    int initial_time_restriction;
    int time_restriction;
    float time_delay = 2f;
    float nextUsage = 1000000f;
    AudioSource footsteps;

    private void Start()
    {
        cylinderChangeColor = GameObject.Find("Canvas").GetComponent<CylinderChangeColor>();
        character1 = GameObject.Find("Character1");
        footsteps = GameObject.Find("S").GetComponent<AudioSource>();
        scene_name = SceneManager.GetActiveScene().name;

        if (scene_name == "Level 1")
        {
            player_count = 1;

            initial_time_restriction = 4;
            time_restriction = 4;

            level_dict = new Dictionary<string, List<string>>()
            {
                {"S", new List<string> { "2" } },
                {"2", new List<string> { "3", "S" } },
                {"3", new List<string> { "4", "2" } },
                {"4", new List<string> { "E", "3" } }
            };

            tree_dict = new Dictionary<string, List<int>>()
            {
                {"2", new List<int>{0, 16} },
                {"3", new List<int>{16, 16} },
                {"4", new List<int>{16, 32} },
                {"E", new List<int>{16, 48} }
            };
        }
        else if (scene_name == "Level 2")
        {
            player_count = 1;

            initial_time_restriction = 5;
            time_restriction = 5;

            level_dict = new Dictionary<string, List<string>>()
            {
                {"S", new List<string> { "2", "6" } },
                {"2", new List<string> { "3", "S" } },
                {"3", new List<string> { "4", "2" } },
                {"4", new List<string> { "5", "3" } },
                {"5", new List<string> { "E", "4", "10"} },
                {"6", new List<string> { "S", "7" } },
                {"7", new List<string> { "6", "8" } },
                {"8", new List<string> { "7", "9" } },
                {"9", new List<string> { "8", "10" } },
                {"10", new List<string> { "9", "5" } }
            };

            tree_dict = new Dictionary<string, List<int>>()
            {
                {"2", new List<int>{0, -16} },
                {"3", new List<int>{-16, -16} },
                {"4", new List<int>{-16, 0} },
                {"5", new List<int>{-16, 16} },
                {"6", new List<int>{16, 0} },
                {"7", new List<int>{16, 16} },
                {"8", new List<int>{16, 32} },
                {"9", new List<int>{0, 32} },
                {"10", new List<int>{-16, 32} },
                {"E", new List<int>{0, 16} }
            };
        }
        else if (scene_name == "Level 3")
        {
            player_count = 2;

            initial_time_restriction = 5;
            time_restriction = 5;

            level_dict = new Dictionary<string, List<string>>()
            {
                {"S", new List<string> { "2", "3" } },
                {"2", new List<string> { "4", "S" } },
                {"3", new List<string> { "4", "S" } },
                {"4", new List<string> { "5", "6", "2", "3" } },
                {"5", new List<string> { "E", "4" } },
                {"6", new List<string> { "E", "4" } },
            };

            tree_dict = new Dictionary<string, List<int>>()
            {
                {"2", new List<int>{0, 16} },
                {"3", new List<int>{16, 0} },
                {"4", new List<int>{16, 16} },
                {"5", new List<int>{16, 32} },
                {"6", new List<int>{32, 16} },
                {"E", new List<int>{32, 32} }
            };

            PathSelection = GameObject.Find("PathSelection");
            Instruction_Canvas.SetActive(false);

        }
        else if (scene_name == "Level 4")
        {
            player_count = 3;

            initial_time_restriction = 7;
            time_restriction = 7;

            level_dict = new Dictionary<string, List<string>>()
            {
                {"S", new List<string> { "2" } },
                {"2", new List<string> { "3", "5" } },
                {"3", new List<string> { "2", "4", "6" } },
                {"4", new List<string> { "3", "7" } },
                {"5", new List<string> { "2", "6", "E" } },
                {"6", new List<string> { "3", "5", "7", "8"} },
                {"7", new List<string> { "4", "6", "9"} },
                {"8", new List<string> { "6", "9", "E"} },
                {"9", new List<string> { "7", "8"} },
            };

            tree_dict = new Dictionary<string, List<int>>()
            {
                {"2", new List<int>{0, 16} },
                {"3", new List<int>{16, 16} },
                {"4", new List<int>{32, 16} },
                {"5", new List<int>{0, 32} },
                {"6", new List<int>{16, 32} },
                {"7", new List<int>{32, 32} },
                {"8", new List<int>{16, 48} },
                {"9", new List<int>{32, 48} },
                {"E", new List<int>{0, 48} }
            };
        }
        else if (scene_name == "Level 5")
        {
            player_count = 3;

            initial_time_restriction = 7;
            time_restriction = 7;

            level_dict = new Dictionary<string, List<string>>()
            {
                {"S", new List<string> { "2", "3" } },
                {"2", new List<string> { "4", "5" } },
                {"3", new List<string> { "4" } },
                {"4", new List<string> { "2", "3" } },
                {"5", new List<string> { "6", "8", "9" } },
                {"6", new List<string> { "5", "7"} },
                {"7", new List<string> { "6", "8"} },
                {"8", new List<string> { "5", "7", "E"} },
                {"9", new List<string> { "5", "E"} },
            };

            tree_dict = new Dictionary<string, List<int>>()
            {
                {"2", new List<int>{0, -16} },
                {"3", new List<int>{-16, 0} },
                {"4", new List<int>{-16, -16} },
                {"5", new List<int>{0, -32} },
                {"6", new List<int>{-16, -32} },
                {"7", new List<int>{-16, -48} },
                {"8", new List<int>{0, -48} },
                {"9", new List<int>{16, -32} },
                {"E", new List<int>{16, -48} }
            };
        }
        else if (scene_name == "Level 6")
        {
            player_count = 2;

            initial_time_restriction = 5;
            time_restriction = 5;

            level_dict = new Dictionary<string, List<string>>()
            {
                {"S", new List<string> { "2", "4" } },
                {"2", new List<string> { "3T" } },
                {"3T", new List<string> { "6T" } },
                {"4", new List<string> { "3T", "5" } },
                {"5", new List<string> { "4", "6T", "7", "8" } },
                {"6T", new List<string> { "3T" } },
                {"7", new List<string> { "5", "E"} },
                {"8", new List<string> { "5", "E"} },
            };

            tree_dict = new Dictionary<string, List<int>>()
            {
                {"2", new List<int>{0, -16} },
                {"3T", new List<int>{-16, -16, -32, 16} },
                {"4", new List<int>{-16, 0} },
                {"5", new List<int>{-32, 0} },
                {"6T", new List<int>{-32, 16, -16, -16} },
                {"7", new List<int>{-48, 0} },
                {"8", new List<int>{-32, -16} },
                {"E", new List<int>{-48, -16} }
            };

            teleport_dict = new Dictionary<string, List<string>>()
            {
                {"3T", new List<string> { "2", "4" } },
                {"6T", new List<string> { "5" } }
            };
        }
        else if (scene_name == "Level 7")
        {
            player_count = 2;

            initial_time_restriction = 3;
            time_restriction = 3;

            level_dict = new Dictionary<string, List<string>>()
            {
                {"S", new List<string> { "2T", "3", "7" } },
                {"2T", new List<string> { "6T", "8T" } },
                {"3", new List<string> { "4" } },
                {"4", new List<string> { "3", "5"} },
                {"5", new List<string> { "4", "6T"} },
                {"6T", new List<string> { "2T", "8T"} },
                {"7", new List<string> { "8T" } },
                {"8T", new List<string> { "2T", "6T"} },
            };

            tree_dict = new Dictionary<string, List<int>>()
            {
                {"2T", new List<int>{0, -16} },
                {"3", new List<int>{-16, 0} },
                {"4", new List<int>{-16, 16} },
                {"5", new List<int>{0, 16} },
                {"6T", new List<int>{16, 16} },
                {"7", new List<int>{16, 0} },
                {"8T", new List<int>{32, 0} },
                {"E", new List<int>{32, 16} }
            };

            teleport_dict = new Dictionary<string, List<string>>()
            {
                {"2T", new List<string> { } },
                {"6T", new List<string> { "5", "E" } },
                {"8T", new List<string> { "7", "E" } }
            };
        }
        else if (scene_name == "Level 8")
        {
            player_count = 3;

            initial_time_restriction = 5;
            time_restriction = 5;

            level_dict = new Dictionary<string, List<string>>()
            {
                {"S", new List<string> { "2T" } },
                {"2T", new List<string> { "4T", "6T", "8T" } },
                {"3", new List<string> { "2T", "4T" } },
                {"4T", new List<string> { "2T", "6T", "8T" } },
                {"5", new List<string> { "4T", "6T", "7"} },
                {"6T", new List<string> { "2T", "4T", "8T" } },
                {"7", new List<string> { "5", "E"} },
                {"8T", new List<string> { "2T", "4T", "6T"} },
            };

            tree_dict = new Dictionary<string, List<int>>()
            {
                {"2T", new List<int>{0, -16} },
                {"3", new List<int>{16, -16} },
                {"4T", new List<int>{32, -16} },
                {"5", new List<int>{32, -32} },
                {"6T", new List<int>{32, -48} },
                {"7", new List<int>{16, -32} },
                {"8T", new List<int>{0, -48} },
                {"E", new List<int>{0, -32} }
            };

            teleport_dict = new Dictionary<string, List<string>>()
            {
                {"2T", new List<string> { "3" } },
                {"4T", new List<string> { "3", "5" } },
                {"6T", new List<string> { "5" } },
                {"8T", new List<string> { "E" } }
            };
        }
        else if (scene_name == "Level 9")
        {
            player_count = 3;

            initial_time_restriction = 5;
            time_restriction = 5;

            level_dict = new Dictionary<string, List<string>>()
            {
                {"S", new List<string> { "2", "11" } },
                {"2", new List<string> { "3T" } },
                {"3T", new List<string> { "6T", "10T" } },
                {"4", new List<string> { "3T", "5" } },
                {"5", new List<string> { "4", "E" } },
                {"6T", new List<string> { "3T", "10T" } },
                {"7", new List<string> { "8", "E"} },
                {"8", new List<string> { "7", "9" } },
                {"9", new List<string> { "8", "10T", "11" } },              
                {"10T", new List<string> { "3T", "6T" } },
                {"11", new List<string> { "9" } }
            };

            tree_dict = new Dictionary<string, List<int>>()
            {
                {"2", new List<int>{0, -16} },
                {"3T", new List<int>{0, -32} },
                {"4", new List<int>{16, -32} },
                {"5", new List<int>{16, -16} },
                {"6T", new List<int>{32, -32} },
                {"7", new List<int>{48, -16} },
                {"8", new List<int>{48, 0} },
                {"9", new List<int>{32, 0} },
                {"10T", new List<int>{32, 16} },
                {"11", new List<int>{16, 0} },
                {"E", new List<int>{32, -16} }
            };

            teleport_dict = new Dictionary<string, List<string>>()
            {
                {"3T", new List<string> { "2", "4" } },
                {"6T", new List<string> { "E" } },
                {"10T", new List<string> { "9" } },
            };
        }
        else if (scene_name == "Level 10")
        {
            player_count = 3;

            initial_time_restriction = 6;
            time_restriction = 6;

            level_dict = new Dictionary<string, List<string>>()
            {
                {"S", new List<string> { "2T", "4T" } },
                {"2T", new List<string> { "3T", "4T", "9T" } },
                {"3T", new List<string> { "2T", "4T", "9T" } },
                {"4T", new List<string> { "2T", "3T", "9T" } },
                {"5", new List<string> { "2T", "4T", "6" } },
                {"6", new List<string> { "3T", "5", "9T" } },
                {"7", new List<string> { "4T", "8", "E"} },
                {"8", new List<string> { "7", "9T" } },
                {"9T", new List<string> { "2T", "3T", "4T" } },
                {"10T", new List<string> { "3T", "6T" } }
            };

            tree_dict = new Dictionary<string, List<int>>()
            {
                {"2T", new List<int>{-16, 0} },
                {"3T", new List<int>{-32, 0} },
                {"4T", new List<int>{0, 16} },
                {"5", new List<int>{-16, 16} },
                {"6", new List<int>{-32, 16} },
                {"7", new List<int>{0, 32} },
                {"8", new List<int>{-16, 32} },
                {"9T", new List<int>{-32, 32} },
                {"E", new List<int>{0, 48} }
            };

            teleport_dict = new Dictionary<string, List<string>>()
            {
                {"2T", new List<string> { "3T", "5" } },
                {"3T", new List<string> { "2T", "6" } },
                {"4T", new List<string> { "5", "7" } },
                {"9T", new List<string> { "6", "8" } }
            };
        }

        time_passed = 0;
        time_counter.GetComponent<Text>().text = time_restriction.ToString();
        instruction.GetComponent<Text>().text = string.Format("Reach in {0} moves", time_restriction);
        next_scene_load = SceneManager.GetActiveScene().buildIndex + 1;

    }

    private void Update()
    {
        if (start_turn == true)
        {
            //trigger start of movement
            if (time_passed.ToString() == inst_time_1 && trigger_1 == false)
            {
                trigger_1 = true;
                cylinderChangeColor.c1_pathHistory.Remove(cylinderChangeColor.c1_pathHistory[0]);
                start_move_1 = true;         
            }
            if (time_passed.ToString() == inst_time_2 && trigger_2 == false)
            {
                trigger_2 = true;
                cylinderChangeColor.c2_pathHistory.Remove(cylinderChangeColor.c2_pathHistory[0]);
                start_move_2 = true;
            }
            if (time_passed.ToString() == inst_time_3 && trigger_3 == false)
            {
                trigger_3 = true;
                cylinderChangeColor.c3_pathHistory.Remove(cylinderChangeColor.c3_pathHistory[0]);
                start_move_3 = true;   
            }

            //fast forward time if no movement in particular unit of time 
            if (trigger_1 == false && trigger_2 == false && trigger_3 == false)
            {
                time_restriction -= 1;
                time_passed++;
                time_counter.GetComponent<Text>().text = time_restriction.ToString();
            }

            //detect sight 
            if (start_move_1 == true)
            {
                GameObject Character_1 = GameObject.Find("Character1");
                Transform Char1_transform = Character_1.transform;
                Vector3 Char1_position = Char1_transform.position;
                float angle_1 = Char1_transform.eulerAngles.y;
                if (start_move_2 == true)
                {
                    GameObject Character_2 = GameObject.Find("Character2");
                    Transform Char2_transform = Character_2.transform;
                    Vector3 Char2_position = Char2_transform.position;
                    float angle_2 = Char2_transform.eulerAngles.y;
                    if (Math.Abs(Char1_position.y - Char2_position.y) <= 2)
                    {
                        if ((85 < angle_1 && angle_1 < 95 && 85 < angle_2 && angle_2 < 95) || (265 < angle_1 && angle_1 < 275 && 265 < angle_2 && angle_2 < 275))
                        {
                            if (Math.Abs(Char1_position.z - Char2_position.z) <= 0.5)
                            {
                                SightFailed(Character_1, Character_2);
                            }
                        }
                        else if ((175 < angle_1 && angle_1 < 185 && 175 < angle_2 && angle_2 < 185) || (angle_1 < 5 && angle_2 < 5) || (angle_1 > 355 && angle_2 > 355))
                        {
                            if (Math.Abs(Char1_position.x - Char2_position.x) <= 0.5)
                            {
                                SightFailed(Character_1, Character_2);
                            }
                        }
                        else if (175 < Math.Abs(angle_1 - angle_2) && Math.Abs(angle_1 - angle_2) < 185)
                        {
                            if ((85 < angle_1 && angle_1 < 95) || (265 < angle_1 && angle_1 < 275))
                            {
                                if (Math.Abs(Char1_position.z - Char2_position.z) <= 0.5)
                                {
                                    SightFailed(Character_1, Character_2);
                                }
                            }
                            else
                            {
                                if (Math.Abs(Char1_position.x - Char2_position.x) <= 0.5)
                                {
                                    SightFailed(Character_1, Character_2);
                                }
                            }
                        }
                        else
                        {
                            if (time_passed >= 1)
                            {
                                    if (Math.Abs(Char1_position.x - Char2_position.x) < 2 && Math.Abs(Char1_position.z - Char2_position.z) < 2)
                                    {
                                        if (cylinderChangeColor.c1_pathHistory[0] != "E" || cylinderChangeColor.c2_pathHistory[0] != "E")
                                        {
                                            SightFailed(Character_1, Character_2);
                                        }
                                    }

                            }
                            else
                            {
                                if (Math.Abs(angle_1 - angle_2) <= 5)
                                {
                                    SightFailed(Character_1, Character_2);
                                }
                            }

                        }
                    }
                }

                if (start_move_3 == true)
                {
                    GameObject Character_3 = GameObject.Find("Character3");
                    Transform Char3_transform = Character_3.transform;
                    Vector3 Char3_position = Char3_transform.position;
                    float angle_3 = Char3_transform.eulerAngles.y;
                    if (Math.Abs(Char1_position.y - Char3_position.y) <= 2)
                    {
                        if ((85 < angle_1 && angle_1 < 95 && 85 < angle_3 && angle_3 < 95) || (265 < angle_1 && angle_1 < 275 && 265 < angle_3 && angle_3 < 275))
                        {
                            if (Math.Abs(Char1_position.z - Char3_position.z) <= 0.5)
                            {
                                Debug.Log("error arrives here");
                                SightFailed(Character_1, Character_3);
                            }
                        }
                        else if ((175 < angle_1 && angle_1 < 185 && 175 < angle_3 && angle_3 < 185) || (angle_1 < 5 && angle_3 < 5) || (angle_1 > 355 && angle_3 > 355))
                        {

                            if (Math.Abs(Char1_position.x - Char3_position.x) <= 0.5)
                            {
                                Debug.Log("error arrives here1");
                                SightFailed(Character_1, Character_3);
                            }
                        }
                        else if (175 < Math.Abs(angle_1 - angle_3) && Math.Abs(angle_1 - angle_3) < 185)
                        {

                            if ((85 < angle_1 && angle_1 < 95) || (265 < angle_1 && angle_1 < 275))
                            {
                                if (Math.Abs(Char1_position.z - Char3_position.z) <= 0.5)
                                {
                                    Debug.Log("error arrives here2");
                                    SightFailed(Character_1, Character_3);
                                }
                            }
                            else
                            {
                                if (Math.Abs(Char1_position.x - Char3_position.x) <= 0.5)
                                {
                                    Debug.Log("error arrives here3");
                                    SightFailed(Character_1, Character_3);
                                }
                            }
                        }
                        else
                        {
                            if (time_passed >= 1)
                            {
                                    if (Math.Abs(Char1_position.x - Char3_position.x) < 2 && Math.Abs(Char1_position.z - Char3_position.z) < 2)
                                    {
                                        if (cylinderChangeColor.c1_pathHistory[0] != "E" || cylinderChangeColor.c3_pathHistory[0] != "E")
                                        {
                                            Debug.Log("error arrives here4");
                                            SightFailed(Character_1, Character_3);
                                        }
                                    }
                            }
                            else
                            {
                                if (Math.Abs(angle_1 - angle_3) <= 5)
                                {
                                    SightFailed(Character_1, Character_3);
                                }
                            }

                        }
                    }
                }
            }
            if (start_move_2 == true && start_move_3 == true)
            {
                GameObject Character_2 = GameObject.Find("Character2");
                Transform Char2_transform = Character_2.transform;
                Vector3 Char2_position = Char2_transform.position;
                float angle_2 = Char2_transform.eulerAngles.y;

                GameObject Character_3 = GameObject.Find("Character3");
                Transform Char3_transform = Character_3.transform;
                Vector3 Char3_position = Char3_transform.position;
                float angle_3 = Char3_transform.eulerAngles.y;
                if (Math.Abs(Char2_position.y - Char3_position.y) <= 2)
                {
                    if ((85 < angle_2 && angle_2 < 95 && 85 < angle_3 && angle_3 < 95) || (265 < angle_2 && angle_2 < 275 && 265 < angle_3 && angle_3 < 275))
                    {
                        if (Math.Abs(Char2_position.z - Char3_position.z) <= 0.5)
                        {
                            SightFailed(Character_2, Character_3);
                        }
                    }
                    else if ((175 < angle_2 && angle_2 < 185 && 175 < angle_3 && angle_3 < 185) || (angle_2 < 5 && angle_3 < 5) || (angle_2 > 355 && angle_3 > 355))
                    {
                        if (Math.Abs(Char2_position.x - Char3_position.x) <= 0.5)
                        {
                            SightFailed(Character_2, Character_3);
                        }
                    }
                    else if (175 < Math.Abs(angle_2 - angle_3) && Math.Abs(angle_2 - angle_3) < 185)
                    {
                        if ((85 < angle_2 && angle_2 < 95) || (265 < angle_2 && angle_2 < 275))
                        {
                            if (Math.Abs(Char2_position.z - Char3_position.z) <= 0.5)
                            {
                                SightFailed(Character_2, Character_3);
                            }
                        }
                        else
                        {
                            if (Math.Abs(Char2_position.x - Char3_position.x) <= 0.5)
                            {
                                SightFailed(Character_2, Character_3);
                            }
                        }
                    }
                    else
                    {
                        if (time_passed >= 1)
                        {
                            if (Math.Abs(Char2_position.x - Char3_position.x) < 2 && Math.Abs(Char2_position.z - Char3_position.z) < 2)
                            {
                                if (cylinderChangeColor.c2_pathHistory[0] != "E" || cylinderChangeColor.c3_pathHistory[0] != "E")
                                {
                                    SightFailed(Character_2, Character_3);
                                }
                            }
                        }
                        else
                        {
                            if (Math.Abs(angle_2 - angle_3) <= 5)
                            {
                                SightFailed(Character_2, Character_3);
                            }
                        }

                    }
                }
            }
        }

        //reconfiguring next movement location per frame
        if (start_move_1 == true)
        {
            LoadRunAnimation("Character1");
            StartMove("Character1", cylinderChangeColor.c1_pathHistory);
        }
        if (start_move_2 == true)
        {
            LoadRunAnimation("Character2");
            StartMove("Character2", cylinderChangeColor.c2_pathHistory);
        }
        if (start_move_3 == true)
        {
            LoadRunAnimation("Character3");
            StartMove("Character3", cylinderChangeColor.c3_pathHistory);
        }

        //end movement
        if (finish_move_1 == true)
        {
            EndMove("Character1");          
        }
        else if (finish_move_2 == true)
        {
            EndMove("Character2");
        }
        else if (finish_move_3 == true)
        {
            EndMove("Character3");

        }

        //restart level upon failure
        if (Time.time > nextUsage)
        {
            if (GameObject.Find("Character1") != null)
            {
                GameObject Character1 = GameObject.Find("Character1");
                Character1.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Locomotion with Head");
                Character1.transform.position = new Vector3(0, 0, 0);       
                Character1.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (GameObject.Find("Character2") != null)
            {
                GameObject Character2 = GameObject.Find("Character2");
                Character2.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Locomotion with Head");
                Character2.transform.position = new Vector3(0, 0, 0);
                Character2.transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            if (GameObject.Find("Character3") != null)
            {
                GameObject Character3 = GameObject.Find("Character3");
                Character3.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Locomotion with Head");
                Character3.transform.position = new Vector3(0, 0, 0);
                Character3.transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            if (player_count > 1)
            {
                foreach (Transform child in CharacterSelection.transform)
                {
                    if (child.name.Contains("1"))
                    {
                        child.GetComponent<Image>().color = new Color(0.1083024f, 1, 0, 1); //green
                    }
                    else
                    {
                        child.GetComponent<Image>().color = new Color(1, 0.04129943f, 0, 1); //red
                    }
                }
            }

            cylinderChangeColor.pathHistory = new List<string>() { "S" };
            cylinderChangeColor.c1_pathHistory = new List<string>() { "S" };
            cylinderChangeColor.c2_pathHistory = new List<string>() { "S" };
            cylinderChangeColor.c3_pathHistory = new List<string>() { "S" };
            cylinderChangeColor.c1_path = true;
            cylinderChangeColor.c2_path = false;
            cylinderChangeColor.c3_path = false;
            success_notification.GetComponent<Text>().enabled = false;
            time_restriction = initial_time_restriction;
            time_counter.GetComponent<Text>().text = time_restriction.ToString();
            trigger_1 = false;
            trigger_2 = false;
            trigger_3 = false;
            start_move_1 = false;
            start_move_2 = false;
            start_move_3 = false;
            finish_move_1 = false;
            finish_move_2 = false;
            finish_move_3 = false;
            start_turn = false;
            cylinderChangeColor.activate_start = false;
            destroyed_counter = 0;
            counter_character_name = "NOT SET";
            first_counter = true;
            time_passed = 0;
            GameObject.Find("Main Camera").GetComponent<PhysicsRaycaster>().enabled = true;
            if (scene_name == "Level 3")
            {
                PathSelection.SetActive(true);
                Instruction_Canvas.SetActive(false);
            }

            if (player_count >= 2)
            {
                CharacterSelection.SetActive(true);
                InstantiateTime.SetActive(true);
                C1InstantiateTime.GetComponent<Text>().text = "0";
                C2InstantiateTime.GetComponent<Text>().text = "0";
                if (player_count == 3)
                {
                    C3InstantiateTime.GetComponent<Text>().text = "0";
                }
            }

            nextUsage = 10000000;
        }

        //successful attempt
        if (destroyed_counter == player_count)
        {
            footsteps.Stop();
            success_notification.GetComponent<Text>().text = "SUCCESS";
            success_notification.GetComponent<Text>().enabled = true;
            next_level_delay = true;
        }

        //loading next level
        if (next_level_delay == true)
        {
            SceneManager.LoadScene(next_scene_load);

            if (next_scene_load > PlayerPrefs.GetInt("levelAt"))
            {
                PlayerPrefs.SetInt("levelAt", next_scene_load);
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (cylinderChangeColor.activate_start == true)
        {
            if (start_turn == false)
            {
                if (player_count >= 2)
                {
                    CharacterSelection.SetActive(false);
                    InstantiateTime.SetActive(false);
                }
                    
                if (scene_name == "Level 1")
                {
                    Color j = GameObject.Find("CursorS").GetComponent<Image>().color;
                    GameObject.Find("CursorS").GetComponent<Image>().color = new Color(j.r, j.g, j.b, 0);
                }
                else if (scene_name == "Level 3")
                {
                    try
                    {
                        GameObject.Find("TimeStart").SetActive(false);
                        Instruction_Canvas.SetActive(true);
                    }
                    catch (NullReferenceException)
                    {
                        Debug.Log("User followed the rules");
                    }     
                }

                if (player_count == 1)
                {
                    GameObject Character1 = GameObject.Find("Character1");
                    Character1.transform.position = new Vector3(0, 0, 0);
                    inst_time_1 = "0";
                }
                else
                {
                    GameObject Character1 = GameObject.Find("Character1");
                    Character1.transform.position = new Vector3(0, 0, 0);
                    GameObject Character2 = GameObject.Find("Character2");
                    Character2.transform.position = new Vector3(0, 0, 0);
                    inst_time_1 = C1InstantiateTime.GetComponent<Text>().text;
                    inst_time_2 = C2InstantiateTime.GetComponent<Text>().text;
                }

                if (player_count == 3)
                {
                    GameObject Character3 = GameObject.Find("Character3");
                    Character3.transform.position = new Vector3(0, 0, 0);
                    inst_time_3 = C3InstantiateTime.GetComponent<Text>().text;
                }

                DestroyAll("Tree");
                footsteps.Play();
                GameObject.Find("Main Camera").GetComponent<PhysicsRaycaster>().enabled = false;
                start_turn = true;
            }       
        }
        else
        {
            if (scene_name == "Level 1")
            {
                Color z = GameObject.Find("Cursor2").GetComponent<Image>().color;
                GameObject.Find("Cursor2").GetComponent<Image>().color = new Color(z.r, z.g, z.b, 1);
            }

            for (int i = 1; i < cylinderChangeColor.pathHistory.Count; i++)
            {
                var x = cylinderChangeColor.pathHistory[i];
            
                if (player_count > 1)
                {
                    GameObject.Find(x).transform.Find("Canvas").transform.Find("Text").gameObject.GetComponent<Text>().text = null;
                }

                if (x == "E")
                {
                    Destroy(GameObject.Find("ETree"));
                    cylinderChangeColor.activate_start = false;
                }
                else
                {
                    Destroy(GameObject.Find(string.Format("{0}Tree", x)));
                }
                cylinderChangeColor.removeList.Add(x);
            }

            if (scene_name == "Level 1")
            {
                cursor_number = (cylinderChangeColor.pathHistory.Count + 1).ToString();

                Color y = GameObject.Find(string.Format("Cursor{0}", cursor_number)).GetComponent<Image>().color;
                GameObject.Find(string.Format("Cursor{0}", cursor_number)).GetComponent<Image>().color = new Color(y.r, y.g, y.b, 0);
            }

            foreach (string cylinder in cylinderChangeColor.removeList)
            {
                cylinderChangeColor.pathHistory.Remove(cylinder);
                if (cylinderChangeColor.c1_path == true)
                {
                    cylinderChangeColor.c1_pathHistory.Remove(cylinder);
                }
                else if (cylinderChangeColor.c2_path == true)
                {
                    cylinderChangeColor.c2_pathHistory.Remove(cylinder);
                }
                else
                {
                    cylinderChangeColor.c3_pathHistory.Remove(cylinder);
                }
            }

            cylinderChangeColor.removeList = new List<string>();
        }
    }

    void DestroyAll(string tag)
    {
        GameObject[] trees = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < trees.Length; i++)
        {
            Destroy(trees[i]);
        }
    }

    void StartMove(string character_name, List<string> walking_path)
    {
        nextDestination = GameObject.Find(walking_path[0]);
        GameObject character = GameObject.Find(character_name);
        character.transform.LookAt(new Vector3(nextDestination.transform.position.x, character.transform.position.y, nextDestination.transform.position.z));
        character.transform.position = Vector3.MoveTowards(character.transform.position, nextDestination.transform.position, speed * Time.deltaTime);
        if (Math.Abs(character.transform.position.x - nextDestination.transform.position.x) <= 1 && Math.Abs(character.transform.position.z - nextDestination.transform.position.z) <= 1)
        {
            if (counter_character_name == "NOT SET")
            {
                if (first_counter == false)
                {
                    time_restriction += 1;
                    time_passed--;
                    time_counter.GetComponent<Text>().text = time_restriction.ToString();
                }
                first_counter = false;
                counter_character_name = character.name;
            }

            if (walking_path.Count > 1) //still have destinations left in path
            {
                if (counter_character_name == character.name)
                {
                    Debug.Log(character.name);
                    time_restriction -= 1;
                    time_passed++;
                    time_counter.GetComponent<Text>().text = time_restriction.ToString();
                }

                if (time_restriction <= 0) //time out
                {
                    if (character.name == "Character1")
                    {
                        start_move_1 = false;
                    }
                    else if (character.name == "Character2")
                    {
                        start_move_2 = false;
                    }
                    else
                    {
                        start_move_3 = false;
                    }
                    success_notification.GetComponent<Text>().text = "FAILED (TIME)";
                    success_notification.GetComponent<Text>().enabled = true;
                    nextUsage = Time.time + time_delay;
                    character.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("BasicMotions@Jump");
                    footsteps.Stop();
                }
                else //still got time left 
                {
                    string c_name = character.name;
                    string last_destination = walking_path[0];
                    if (c_name == "Character1")
                    {
                        if (last_destination.Substring(last_destination.Length - 1) == "T")
                        {
                            Transform new_destination = GameObject.Find(walking_path[1]).transform;
                            Vector3 new_coord = new Vector3(new_destination.position.x, 5, new_destination.position.z);
                            character.transform.position = new_coord;
                            character.transform.eulerAngles = new Vector3(0, 45, 0);
                            cylinderChangeColor.c1_pathHistory.Remove(walking_path[1]);
                        }
                        cylinderChangeColor.c1_pathHistory.Remove(last_destination);
                        character.transform.eulerAngles = new Vector3(0, 45, 0);
                        nextDestination = GameObject.Find(cylinderChangeColor.c1_pathHistory[0]);
                    }
                    else if (c_name == "Character2")
                    {
                        if (last_destination.Substring(last_destination.Length - 1) == "T")
                        {
                            Transform new_destination = GameObject.Find(walking_path[1]).transform;
                            Vector3 new_coord = new Vector3(new_destination.position.x, 5, new_destination.position.z);
                            character.transform.position = new_coord;
                            character.transform.eulerAngles = new Vector3(0, 45, 0);
                            cylinderChangeColor.c2_pathHistory.Remove(walking_path[1]);
                        }
                        cylinderChangeColor.c2_pathHistory.Remove(last_destination);
                        character.transform.eulerAngles = new Vector3(0, 45, 0);
                        nextDestination = GameObject.Find(cylinderChangeColor.c2_pathHistory[0]);
                    }
                    else
                    {
                        if (last_destination.Substring(last_destination.Length - 1) == "T")
                        {
                            Transform new_destination = GameObject.Find(walking_path[1]).transform;
                            Vector3 new_coord = new Vector3(new_destination.position.x, 5, new_destination.position.z);
                            character.transform.position = new_coord; 
                            character.transform.eulerAngles = new Vector3(0, 45, 0);
                            cylinderChangeColor.c3_pathHistory.Remove(walking_path[1]);
                        }
                        cylinderChangeColor.c3_pathHistory.Remove(last_destination);
                        character.transform.eulerAngles = new Vector3(0, 45, 0);
                        nextDestination = GameObject.Find(cylinderChangeColor.c3_pathHistory[0]);
                    }

                    character.transform.LookAt(new Vector3(nextDestination.transform.position.x, character.transform.position.y, nextDestination.transform.position.z));
                }
            }
            else //reached final destination in path
            {
                if (character.name == "Character1")
                {
                    start_move_1 = false;
                    finish_move_1 = true;
                }
                else if (character.name == "Character2")
                {
                    start_move_2 = false;
                    finish_move_2 = true;
                }
                else
                {
                    start_move_3 = false;
                    finish_move_3 = true;
                }

                if (counter_character_name == character.name)
                {
                    time_restriction -= 1;
                    time_passed++;
                    time_counter.GetComponent<Text>().text = time_restriction.ToString();
                    counter_character_name = "NOT SET";
                }
                
            }  
        }
    }

    void EndMove(string character_name)
    {
        GameObject character_done = GameObject.Find(character_name);
        character_done.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Locomotion with Head");        
        destroyed_counter++;
        if (character_name == "Character1")
        {
            finish_move_1 = false;
        }
        if (character_name == "Character2")
        {
            finish_move_2 = false;
        }
        else
        {
            finish_move_3 = false;
        }

        character_done.transform.position = new Vector3(0, -100, 0);
    }

    void LoadRunAnimation(string character_name)
    {
        GameObject.Find(character_name).GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("BasicMotions@Walk");
    }

    void SightFailed(GameObject char_1, GameObject char_2)
    {
        success_notification.GetComponent<Text>().text = "FAILED (SIGHT)";
        success_notification.GetComponent<Text>().enabled = true;
        nextUsage = Time.time + time_delay;
        char_1.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("BasicMotions@Jump");
        char_2.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("BasicMotions@Jump");
        footsteps.Stop();
        start_move_1 = false;
        start_move_2 = false;
        start_move_3 = false;
    }
}