using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TabLayoutAdapter<T> : ListAdapter<T>,OnListItemClikedListener
{
    protected OnListItemClikedListener overridededClickListener;

    public TabLayoutAdapter(ViewElement<T> viewPrefab, RectTransform listRoot, List<T> data, OnListItemClikedListener onListItemClikedListener = null) : base(viewPrefab, listRoot, data, onListItemClikedListener)
    {

       overridededClickListener = onListItemClikedListener;
       this.onListItemClikedListener = this;     
    }

    protected override void OnCreatedSucess()
    {
        if (viewElements.Count == 0) return;

            //unselect all
            for (int i = 0; i < viewElements.Count; i++)
            {
                ((SelectableViewElement<T>)viewElements[i]).UnSelect();
            }


        //select default one (index = 0)
        ((SelectableViewElement<T>)viewElements[0]).Select();
    }


    public virtual void OnListClicked(int index)
    {

        for (int i = 0; i < viewElements.Count; i++) {          
           ((SelectableViewElement<T>)viewElements[i]).UnSelect();
        }

        ((SelectableViewElement<T>)viewElements[index]).Select();

        overridededClickListener.OnListClicked(index);
    }
}
