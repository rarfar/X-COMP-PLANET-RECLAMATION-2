using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

public class HoverTextManager : MonoBehaviour
{
    // Written by Kelly

    public TextMeshProUGUI tip;
    public RectTransform tipBox;

    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLeave;

    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLeave += HideTip;
    }

    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLeave -= HideTip;
    }

    private void Start()
    {
        HideTip();
    }
    private void ShowTip (string text, Vector2 mousePosition)
    {
        tip.text = text;
        tipBox.sizeDelta = new Vector2(150, tip.preferredHeight);
        tipBox.gameObject.SetActive(true);
    }
    private void HideTip()
    {
        tip.text = default;
        tipBox.gameObject.SetActive(false);
    }
}
