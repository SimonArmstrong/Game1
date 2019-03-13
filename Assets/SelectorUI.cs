using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorUI : MonoBehaviour {

    public bool movingToSelection = false;

    Vector3 startPos, endPos;
    float startWidth, startHeight, curTime, endTime, normalizedValue;

    public RectTransform nextRect;
    RectTransform curRect;

    void Start()
    {
        curTime = 0;
        endTime = 3;
        curRect = GetComponent<RectTransform>();
        Select(nextRect);
    }

    public void InitSelect(RectTransform nextRct)
    {
        if (curRect != null) return;
        
        //Init behaviour

        curRect = nextRct;

        Select(nextRct);
    }

    public void Select(RectTransform nextRct)
    {
        nextRect = nextRct;

        startPos = curRect.transform.position;
        endPos = nextRect.transform.position;

        startWidth = curRect.rect.width;
        startHeight = curRect.rect.height;

        movingToSelection = true;
        StartCoroutine(MoveSelector());
    }

    IEnumerator MoveSelector()
    {
        while(curTime < endTime)
        {

        curTime += Time.deltaTime;

        normalizedValue = curTime/endTime;

        Debug.Log(Vector3.Lerp(startPos, endPos, normalizedValue));

        curRect.transform.position = Vector3.Lerp(startPos, endPos, normalizedValue);

        //HandleRectSize();

        yield return null;
        }
            movingToSelection = false;
            curRect = nextRect;
            StopCoroutine(MoveSelector());
    }

    void HandleRectSize()
    {
        float scaledValue = Vector3.Distance(endPos, curRect.anchoredPosition) / Vector3.Distance(endPos, startPos);

        if (startWidth < nextRect.rect.width)
        {
            float value = (scaledValue * (nextRect.rect.width - startWidth)) + startWidth;
            curRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
        }
        if (startWidth > nextRect.rect.width)
        {
            float value = (scaledValue * (startWidth - nextRect.rect.width)) + nextRect.rect.width;
            curRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, value);
        }

        if (startHeight < nextRect.rect.height)
        {
            float value = (scaledValue * (nextRect.rect.height - startHeight)) + startHeight;
            curRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value);
        }
        if (startHeight > nextRect.rect.height)
        {
            float value = (scaledValue * (startHeight - nextRect.rect.height)) + nextRect.rect.height;
            curRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, value);
        }

    }
}
