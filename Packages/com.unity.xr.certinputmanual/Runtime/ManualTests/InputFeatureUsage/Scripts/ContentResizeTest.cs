using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ContentResize))]
public class ContentResizeTest : MonoBehaviour
{
    public GameObject ThingForTheThing;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A");
            GetComponent<ContentResize>().AddContentItem(Instantiate(ThingForTheThing));
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("C");
            GetComponent<ContentResize>().ClearContentItems();
        }
    }
}
