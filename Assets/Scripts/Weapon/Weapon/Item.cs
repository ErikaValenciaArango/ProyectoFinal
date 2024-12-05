using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item: ScriptableObject
{
    public string title;
    public Sprite icon;
    public string description;


    public virtual void Use()
    {
        Debug.Log($"{title} was used.");
    }
}
