using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SelectedWhenActivated : MonoBehaviour
{
    EventSystem ev;

    [SerializeField] GameObject enableTarget;
    [SerializeField] GameObject disableTarget;

    void EventSystemGetter()
    {
        if(ev){ return; }
        ev = EventSystem.current;
    }

    private void OnEnable() 
    {
        EventSystemGetter();
        if(!enableTarget) { return; }
        ev.SetSelectedGameObject(enableTarget);
    }

    private void OnDisable() 
    {
        EventSystemGetter();
        if(!disableTarget) { return; }
        ev.SetSelectedGameObject(disableTarget);
    }
}
