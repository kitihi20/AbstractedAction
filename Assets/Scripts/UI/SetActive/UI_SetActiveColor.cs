using UnityEngine;
using UnityEngine.UI;

public class UI_SetActiveColor : UI_A_SetActive
{
    [SerializeField] Image targetImage;
    [SerializeField] Color startColor = Color.black;
    [SerializeField] Color endColor = Color.red;
    [SerializeField] float transitionTime = 1f;

    bool active;
    float nowtime;

    void Start()
    {
        enabled = false;
        targetImage.color = startColor;
        gameObject.SetActive(false);
    }


    void Update()
    {
        nowtime += Time.unscaledDeltaTime;
        if (nowtime >= transitionTime)
        {
            targetImage.color = active ? endColor : startColor;
            enabled = false;
            if (!active) { gameObject.SetActive(false); }
            return;
        }
        if (active)
        {
            targetImage.color = Color.Lerp(startColor, endColor, nowtime/transitionTime);
        }
        else
        { 
            targetImage.color = Color.Lerp(endColor, startColor, nowtime/transitionTime);
        }
    }

    public override void SetActive(bool v)
    {
        if (active == v) { return; }
        active = v;
        nowtime = 0;
        gameObject.SetActive(true);
        enabled = true;
    }
}
