using System.Collections.Generic;
using UnityEngine;

public class InventoryAdapter<T> : TabLayoutAdapter<T>, OnListItemClikedListener where T : class
{
    private int numOfPages;
    private int numOfItemPerPage;
    private int currentPage;

    public int CurrentPage { get => currentPage; set => currentPage = value; }

    public InventoryAdapter(ViewElement<T> viewPrefab, RectTransform listRoot, List<T> data
        , int numOfPages, int numOfItemPerPage, int currentPage = 1,
        OnListItemClikedListener onListItemClikedListener = null) :
        base(viewPrefab, listRoot, data, onListItemClikedListener)
    {
        this.overridededClickListener = onListItemClikedListener;
        this.onListItemClikedListener = this;
        this.numOfPages = numOfPages;
        this.numOfItemPerPage = numOfItemPerPage;
        this.currentPage = currentPage;
    }

    public override void CreateViews()
    {
        for (int i = 0; i < numOfItemPerPage; i++)
        {
            ViewElement<T> viewElement = GetElementView(i);
            if (data.Count > i)
                viewElement.Bind(data[i], i, onListItemClikedListener);
            else
                viewElement.Bind(null, i, onListItemClikedListener);
        }
        OnCreatedSucess();
    } 

    public override void UpdateViews()
    {
        int currentPageStartIndex = (currentPage - 1) * numOfItemPerPage;
        for (int i = 0; i < viewElements.Count; i++)
        {
            ViewElement<T> viewElement = viewElements[i];
            int dataIndex = currentPageStartIndex + i;
            if (dataIndex < data.Count)
                viewElement.UpdateView(data[dataIndex]);
            else
            {
                viewElement.UpdateView(null);
            }
        }
    }

    public int OnNextPageClicked()
    {
        if (currentPage < numOfPages)
        {
            currentPage++;
            UpdateViews();
        }
        return currentPage;
    }

    public int OnPrevuisPageClicked()
    {
        if (currentPage > 1)
        {
            currentPage--;
            UpdateViews();
        }
        return currentPage;
    }

    public override void OnListClicked(int index)
    {
        int slotIndexToDataIndex = (currentPage - 1) * numOfItemPerPage + index;


        for (int i = 0; i < viewElements.Count; i++)
        {
            ((SelectableViewElement<T>)viewElements[i]).UnSelect();
        }

        ((SelectableViewElement<T>)viewElements[slotIndexToDataIndex]).Select();

        overridededClickListener.OnListClicked(slotIndexToDataIndex);
    }
}