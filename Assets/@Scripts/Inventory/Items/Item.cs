using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public ItemData ItemData {get; protected set;}

    public Item(ItemData itemData)
    {
        ItemData = itemData;
    }
}
