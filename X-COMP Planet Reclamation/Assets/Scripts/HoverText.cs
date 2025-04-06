using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;


public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string text;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(DelayTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverTextManager.OnMouseLeave();
    }

    private void ShowText()
    {
        HoverTextManager.OnMouseHover(text, Input.mousePosition);

    }
    private IEnumerator DelayTimer()
    {
        yield return new WaitForSeconds(0.5f);
        ShowText();
    }
}
