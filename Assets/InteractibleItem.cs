using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleItem : MonoBehaviour
{
    public float interactionRange = 2f;
    [SerializeField] GameObject cam;

    // Update is called once per frame
    void Start()
    {
        cam = GameObject.FindWithTag("Camera");
    }
    void Update()
    {
        if(Vector3.Distance(cam.transform.position, transform.position) < interactionRange)
        {
           if(Input.GetKeyDown(KeyCode.E))
           {
               Destroy(gameObject);
           }
            
        }
    }
}
