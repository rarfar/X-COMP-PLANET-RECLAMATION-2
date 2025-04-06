using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Labels : MonoBehaviour
{
    [SerializeField] GameObject[] items;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject pfLabel;
    private List<GameObject> labels = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {
            GameObject newLabel = Instantiate(pfLabel);
            newLabel.GetComponent<TextMeshProUGUI>().SetText(items[i].name);
            newLabel.transform.SetParent(canvas.transform, false);
            labels.Add(newLabel);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < items.Length; i++)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(items[i].transform.position);
            Vector2 canvasPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPos, null, out canvasPos);

            if (screenPos.z > 0)
            {
                labels[i].SetActive(true);
                labels[i].GetComponent<RectTransform>().anchoredPosition = canvasPos;
            }
            else
            {
                labels[i].SetActive(false);
            }
        }
    }
}
