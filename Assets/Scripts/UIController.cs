using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField] List<Section> sections;
    [SerializeField] GameObject menu;

    public void OpenSection(SectionType typ)
    {
        sections.ForEach((x) => x.sectionObject.SetActive(false));
        if (sections.Exists(x => x.sectionType == typ))
        {
            sections.Find(x => x.sectionType == typ).sectionObject.SetActive(true);
            menu.SetActive(typ != SectionType.Auth);
        }
    }
}