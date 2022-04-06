using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenClose : MonoBehaviour
{
    [SerializedField] private Animator myDoor = null;

    [SerializedField] private bool openTrigger = false;
    [SerializedField] private bool closeTrigger = false;

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            if (openTrigger)
            {
                myDoor.Play("TowerDoorOpen", 0, 0.0f);
                gameObject.SetActive(false);
            }
            
            else if (closeTrigger)
            {
                myDoor.Play("TowerDoorClose", 0, 0.0f);
                gameObject.SetActive(false);
            }
        }
    }
}
