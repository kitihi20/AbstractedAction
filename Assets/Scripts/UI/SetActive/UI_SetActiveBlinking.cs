using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_SetActiveBlinking : UI_A_SetActive
{
    [SerializeField] Image[] targetImages;
    [SerializeField] TMP_Text[] targetTexts;
    [SerializeField] float blinkingTime = 0.2f;
    [SerializeField] float maxBlinkTime = 0.04f;
    [SerializeField] AnimationCurve blinkSpeedCurve = new AnimationCurve(new Keyframe(0,0),new Keyframe(1,1));
    [SerializeField] bool startIsActive = false;


    bool active;
    bool rendereractivated;
    float nowtime;
    float nowblinktime;

    void Start()
    {
        active = false;
        enabled = false;

        rendereractivated = active ^ startIsActive;
        for (int i = 0; i < targetImages.Length; i++)
        {
            targetImages[i].enabled = rendereractivated;
        }
        for (int i = 0; i < targetTexts.Length; i++)
        { 
            targetTexts[i].enabled = rendereractivated;
        }
    }


    void Update()
    {
        nowtime += Time.unscaledDeltaTime;
        nowblinktime += Time.unscaledDeltaTime;
        //end
        if (nowtime >= blinkingTime)
        {
            bool res = active ^ startIsActive;
            for (int i = 0; i < targetImages.Length; i++)
            {
                targetImages[i].enabled = res;
            }
            for (int i = 0; i < targetTexts.Length; i++)
            {
                targetTexts[i].enabled = res;
            }
            enabled = false;
            return;
        }
        //blink
        if (nowblinktime >= blinkSpeedCurve.Evaluate(nowtime / blinkingTime) * maxBlinkTime)
        {
            nowblinktime = 0;
            rendereractivated = !rendereractivated;
            for (int i = 0; i < targetImages.Length; i++)
            {
                targetImages[i].enabled = rendereractivated;
            }
            for (int i = 0; i < targetTexts.Length; i++)
            {
                targetTexts[i].enabled = rendereractivated;
            }
        }
    }

    public override void SetActive(bool v)
    {
        if (active == v) { return; }
        active = v;
        nowtime = 0;
        nowblinktime = 0;
        rendereractivated = active ^ startIsActive;
        enabled = true;
    }

}
