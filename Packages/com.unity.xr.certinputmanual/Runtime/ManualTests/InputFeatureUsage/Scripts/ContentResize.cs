using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class ContentResize : MonoBehaviour
{
    public float itemHeight = 100f;
    public float itemSpacing = 10f;

    RectTransform MyRectTransform;
    List<GameObject> ContentItems;

    private void Start()
    {
        MyRectTransform = GetComponent<RectTransform>();
        ClearContentItems();
    }

    public void AddContentItem(GameObject NewContentItem)
    {
        RectTransform newRectTrans = NewContentItem.GetComponent<RectTransform>();

        newRectTrans.SetParent(MyRectTransform);
        newRectTrans.localRotation = Quaternion.identity;
        newRectTrans.localScale = Vector3.one;

        ContentItems.Add(NewContentItem);

        float NewItemY = (itemSpacing + (itemHeight / 2))
            + ((ContentItems.Count - 1) * (itemHeight + itemSpacing));
        newRectTrans.localPosition = new Vector3(MyRectTransform.sizeDelta.x/2.0f, -NewItemY, 0);

        RefreshContentSize();
    }

    public void ClearContentItems()
    {
        if (ContentItems != null)
            foreach (GameObject item in ContentItems)
                Destroy(item);
        ContentItems = new List<GameObject>();
        RefreshContentSize();
    }

    private void RefreshContentSize()
    {
        float NewContentHeight = (itemHeight * ContentItems.Count) + (itemSpacing * (1 + ContentItems.Count));
        MyRectTransform.sizeDelta = new Vector2(MyRectTransform.sizeDelta.x, NewContentHeight);
    }
}
