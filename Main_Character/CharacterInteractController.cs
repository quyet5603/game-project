using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Quản lý việc tương tác của Player với Interatable Items
public class CharacterInteractController : MonoBehaviour
{
    CharacterController2D characterController;   // Xét Character có đối mặt với Items hay không
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    Character character;
    [SerializeReference] HighlightController highlightController;
    // [SerializeField] float delayUse = 0.5f;
    // private float delay = 0f;
    private void Awake()
    {
        characterController = GetComponent<CharacterController2D>();
        rgbd2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        Check();
        if(Input.GetMouseButtonDown(1))
        {
            Interact();
        }
        // if (delay > 0)
        // {
        //     delay -= Time.deltaTime;
        //     return;
        // }

        // if (Input.GetMouseButton(1))
        // {
        //     Interact();
        //     delay = delayUse;
        // }    
    }

    
    

    private void Check()
    {
        Vector2 position = rgbd2d.position + characterController.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                highlightController.Highlight(hit.gameObject);
                // Nếu tồn tại item thì highlight và thoát
                return;
            }
        }
        // Nếu không xuất hiện item nào thì ẩn highlighter
        highlightController.Hide();
    }

    private void Interact()
    {
        Vector2 position = rgbd2d.position + characterController.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(character);
                break;
            }
        }
    }
}
