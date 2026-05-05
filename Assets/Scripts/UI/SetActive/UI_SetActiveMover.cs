using UnityEngine;

public class UI_SetActiveMover : UI_A_SetActive
{
    [SerializeField] Vector3 StartPosition;
    [SerializeField] Vector3 EndPosition;
    [SerializeField] float moveTime = 1f;
    [SerializeField] bool startisDisable = false;

    RectTransform rect;
    bool active;
    float nowtime;

    void Start()
    {
        rect = transform as RectTransform;
        enabled = false;
        rect.anchoredPosition3D = StartPosition;
        if (startisDisable) { gameObject.SetActive(false); }
    }


    void Update()
    {
        nowtime += Time.unscaledDeltaTime;
        if (nowtime >= moveTime)
        {
            rect.anchoredPosition3D = active ? EndPosition : StartPosition;
            enabled = false;
            if (startisDisable && !active) { gameObject.SetActive(false); }
            return;
        }
        if (active)
        {
            rect.anchoredPosition3D = Vector3.Lerp(StartPosition, EndPosition, nowtime/moveTime);
        }
        else
        { 
            rect.anchoredPosition3D = Vector3.Lerp(EndPosition, StartPosition, nowtime/moveTime);
        }
    }

    public override void SetActive(bool v)
    {
        if (active == v) { return; }
        gameObject.SetActive(true);
        active = v;
        nowtime = 0;
        enabled = true;
    }
}
