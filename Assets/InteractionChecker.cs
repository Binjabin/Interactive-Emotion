using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChecker : MonoBehaviour
{
    GameObject cam;
    Transform interactedObject;
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject keyShow;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindWithTag("Camera");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 5f, layerMask))
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                
                interactedObject = hit.transform;
                if(interactedObject != null)
                {
                    interactedObject.GetComponent<Interactible>().Interact();
                }

            }
            keyShow.SetActive(true);

        }
        else
        {
            keyShow.SetActive(false);
        }

        
    }
}
