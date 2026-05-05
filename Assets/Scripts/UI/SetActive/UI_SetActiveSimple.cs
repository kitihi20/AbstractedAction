using UnityEngine;

public class UI_SetActiveSimple : UI_A_SetActive
{
    [SerializeField] bool startisDisable = false;

    bool active;

    void Start()
    {
        gameObject.SetActive(!startisDisable);
    }

    public override void SetActive(bool v)
    {
        if (active == v) { return; }
        active = v;
        gameObject.SetActive(startisDisable && active);
    }
}
