using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Start is called before the first frame update
    public List<InteractibleItem> items = new List<InteractibleItem>();

    // Update is called once per frame
    public void AddItem(InteractibleItem item)
    {
        items.Add(item);
    }
    public void RemoveItem(InteractibleItem item)
    {
        items.Remove(item);
    }
}
