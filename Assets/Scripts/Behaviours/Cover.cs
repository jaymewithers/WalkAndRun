﻿using UnityEngine;
using UnityEngine.Events;

public class Cover : MonoBehaviour
{
    public bool canUse, canLetGo;
    public FloatData coverData;
    public Transform player, obj;
    public Vector3 offset, restOffset;
    public UnityEvent disableEvent, enableEvent;
    
    private void Start()
    {
        coverData.value = 0f;
        canUse = false;
        canLetGo = false;
    }

    private void Update()
    {
        if (canUse && Input.GetKeyDown(KeyCode.X))
        {
            coverData.value = 1f;
            disableEvent.Invoke();
            obj.transform.position = player.position + offset;
            obj.parent = player;
            canLetGo = true;
        }

        if (!Input.GetKeyUp(KeyCode.X) || !canLetGo) return;
        coverData.value = 0f;
        obj.parent = null;
        obj.position += restOffset;
    }
    
    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player")) 
        {
            canUse = true;
        } 
    }
    
    private void OnTriggerExit(Collider other)
    {
        enableEvent.Invoke();
        canUse = false;
        canLetGo = false;
    }
}