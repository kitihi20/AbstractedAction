using UnityEngine;

public class UI_SetActiveObjects : UI_A_SetActive
{
    [SerializeField] UI_A_SetActive[] objects;

    bool active;

    void Start()
    {
        active = false;
    }

    public override void SetActive(bool v)
    {
        if (active == v) { return; }
        active = v;
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(active);
        }
    }

}
