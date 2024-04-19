using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Phát hiện bất cứ vật thể nào có thể tương tác
public class Interactable : MonoBehaviour
{
    public virtual void Interact(Character character){}
}
