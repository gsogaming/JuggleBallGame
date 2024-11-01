using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public Color tabidle;
    public Color tabHover;
    public Color tabActive;
    public TabButton selectedTab;
    public List<GameObject> menusToSwap;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null)
        {
            tabButtons = new List<TabButton>();

        }

        tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }
    }

    public void OntTabExit(TabButton button)
    {
        ResetTabs();

    }

    public void OnTabSelected(TabButton button)
    {
        if (selectedTab != null)
        {
            selectedTab.DeSelect();
        }
        selectedTab = button;

        selectedTab.Select();

        ResetTabs();
        button.background.color = tabActive;
        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < menusToSwap.Count; i++)
        {
            if (i == index)
            {
                menusToSwap[i].SetActive(true);
            }
            else
            {
                menusToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if (selectedTab!= null && button == selectedTab)
            {
                continue;
            }
            button.background.color = tabidle;
        }
    }

   
}
