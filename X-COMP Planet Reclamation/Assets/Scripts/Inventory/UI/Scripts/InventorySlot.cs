using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;

public class InventorySlot : MonoBehaviour,IDropHandler
{
    //Drag and Drop
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0 ){
            GameObject dropped = eventData.pointerDrag;
            InventoryItem item = dropped.GetComponent<InventoryItem>();
            item.parentAfterDrag = transform;
        }
    }
}