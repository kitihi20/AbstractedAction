using UnityEngine;

public class UI_SetActiveSizeControll : UI_A_SetActive
{
    [SerializeField] Vector2 StartSize;
    [SerializeField] Vector2 EndSize;
    [SerializeField] float moveTime = 1f;
    [SerializeField] bool startisDisable = false;

    RectTransform rect;
    bool active;
    float nowtime;

    void Start()
    {
        rect = transform as RectTransform;
        active = false;
        enabled = false;
        rect.sizeDelta = StartSize;
        if (startisDisable) { gameObject.SetActive(false); }
    }


    void Update()
    {
        nowtime += Time.unscaledDeltaTime;
        if (nowtime >= moveTime)
        {
            rect.sizeDelta = active ? EndSize : StartSize;
            enabled = false;
            if (startisDisable && !active) { gameObject.SetActive(false); }
            return;
        }
        if (active)
        {
            rect.sizeDelta = Vector3.Lerp(StartSize, EndSize, nowtime / moveTime);
        }
        else
        {
            rect.sizeDelta = Vector3.Lerp(EndSize, StartSize, nowtime / moveTime);
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

    [ContextMenu("GetNowSizeDelta")]
    void NowSizeDelta()
    {
        RectTransform r = transform as RectTransform;
        Debug.LogFormat("SizeDelta: {0}", r.sizeDelta);
    }
}
