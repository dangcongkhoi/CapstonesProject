using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider colider)
    {
        if (colider.tag != "Player")
            return;

        PickUp(colider.transform);
    }

    public virtual void OnPickUp(Transform item)
    {
        
    }

    void PickUp(Transform item)
    {

        OnPickUp(item);
    }
}
