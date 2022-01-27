using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChecker : MonoBehaviour
{
    GameObject cam;
    GameObject interactedObject;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, cam.transform.forward, out hit, 10f);
            interactedObject = hit.gameObject;
            interactedObject.GetComponent<Interactible>().Interact;
        }
    }
}
