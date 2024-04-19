using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolHit : MonoBehaviour
{
    // Khi bị công cụ tác động vào
    public virtual void Hit() {}
    public virtual bool CanBeHit(List<ResourceNodeType> canBeHitBy)
    {
        return true;
    }
}
