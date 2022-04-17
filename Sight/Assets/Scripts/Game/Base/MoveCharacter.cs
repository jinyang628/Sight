using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCharacter : MonoBehaviour, IPointerDownHandler
{
    Animator animator;
    public GameObject character;
    private CylinderChangeColor cylinderChangeColor;

    bool start_move;

    private void Start()
    {
        cylinderChangeColor = GameObject.Find("Canvas").GetComponent<CylinderChangeColor>();
        animator = character.GetComponent<Animator>();
    }

    private void Update()
    {
        if (start_move == true)
        {
            character.transform.position = Vector3.MoveTowards(transform.position, GameObject.Find(cylinderChangeColor.pathHistory[cylinderChangeColor.pathHistory.Count - 1]).transform.position, (float)1);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (cylinderChangeColor.activate_start == true)
        {
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("BasicMotions@Walk");
            character.transform.LookAt(GameObject.Find(cylinderChangeColor.pathHistory[cylinderChangeColor.pathHistory.Count - 1]).transform.position);
            start_move = true;        
        }
    }
}