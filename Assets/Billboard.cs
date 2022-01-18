using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float visibleDistance;
    [SerializeField] private Vector3 defaultScale;
    [SerializeField] private float scaleSpeed;

    [SerializeField] private float maxSize;
    [SerializeField] private float minSize;
    private float scaleValue;
    private bool reversed;

    GameObject attatchedItem;

    // Update is called once per frame
    void Start()
    {
        cam = GameObject.FindWithTag("Camera");
        scaleValue = 1f;
        attatchedItem = transform.parent.gameObject;
        visibleDistance = attatchedItem.GetComponent<InteractibleItem>().interactionRange;
    }
    void LateUpdate()
    {
        if(Vector3.Distance(cam.transform.position, attatchedItem.transform.position) < visibleDistance)
        {
            if(scaleValue > maxSize)
            {
                reversed = true;
            }
            if(scaleValue < minSize)
            {
                reversed = false;
            }
            if(reversed == true)
            {
                scaleValue -= Time.deltaTime * scaleSpeed;
            }
            else
            {
                scaleValue += Time.deltaTime * scaleSpeed;
            }
        }
        else
        {
            scaleValue -= Time.deltaTime * scaleSpeed;
            scaleValue = Mathf.Clamp(scaleValue, 0f, maxSize);
        }

        gameObject.GetComponent<RectTransform>().localScale = defaultScale * scaleValue;
        transform.LookAt(cam.transform.position, Vector3.down);
    }
}
