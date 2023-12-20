using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListClickedListener : OnListItemClikedListener
{

    Action<int> onclicked;

    public ListClickedListener(Action<int> onclicked)
    {
        this.onclicked = onclicked;
    }




    // Start is called before the first frame update
    public void OnListClicked(int index)
    {
        onclicked?.Invoke(index);  
    }
}
