using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftOneOnClickListApapter<T> : ListAdapter<T>, OnListItemClikedListener where T : class
{
    private int numOfPages;
    private int numOfItemPerPage;
    private int currentPage;
    private OnListItemClikedListener overridededClickListener;

    public int CurrentPage { get => currentPage; set => currentPage = value; }

    public ShiftOneOnClickListApapter(ViewElement<T> viewPrefab, RectTransform listRoot, List<T> data
       , int numOfItemPerPage , int numOfSlots , int currentPage = 1,
        OnListItemClikedListener onListItemClikedListener = null) :
        base(viewPrefab, listRoot, data, onListItemClikedListener)
    {
        this.overridededClickListener = onListItemClikedListener;
        this.onListItemClikedListener = this;
        this.numOfPages = numOfSlots + 1 - numOfItemPerPage;
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
        OnStartUpdatingViews?.Invoke();

        int currentPageStartIndex = currentPage - 1;
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

        OnFinishUpdatingViews?.Invoke();
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
        Debug.Log(currentPage);

        if (currentPage > 1)
        {
            currentPage--;
            UpdateViews();
        }
        return currentPage;
    }

    public void OnListClicked(int index)
    {
        int indexFromSlotToData = (currentPage - 1) + index;
        overridededClickListener.OnListClicked(indexFromSlotToData);
    }
}