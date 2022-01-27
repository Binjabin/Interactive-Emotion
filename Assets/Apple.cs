using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : Interactible
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        quest.GainApple();
        Debug.Log("Pickup Apple");
    }
}
