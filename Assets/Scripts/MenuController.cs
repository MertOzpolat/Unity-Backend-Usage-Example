using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] List<MenuItem> menuItems;
    void Start()
    {
        OpenMenu(MenuType.Profile);
    }
    void OpenMenu(MenuType type)
    {
        menuItems.ForEach((x) =>
        {
            x.menuButton.enabled = !(x.type == type);
            x.menuPanel.SetActive(x.type == type);
        });
    }
    public void MenuButtonClickHandler(int order)
    {
        OpenMenu((MenuType)order);
    }

}