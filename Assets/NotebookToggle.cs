using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookToggle : MonoBehaviour
{
    public bool active = false;
    [SerializeField] GameObject book;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("a");
            active = !active;
            
        }
        book.SetActive(active);
    }
}
