using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;

    [SerializeField] float startTimeScale = 1;
    [SerializeField] AnimationCurve dodgeTimeScaleCurve;

    float nowTimeScale;
    float targetTimeScale;
    float dodgeTimeScale;
    float dodgeRealtime;
    float nowDodgeRealtime;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = startTimeScale;
        nowTimeScale = startTimeScale;
        targetTimeScale = startTimeScale;

        dodgeTimeScale = startTimeScale;
        nowDodgeRealtime = 0;
    }

    void Update()
    {
        bool mainTimeScaleIsChanged = Math.Abs(targetTimeScale - startTimeScale) > 0.001f;
        bool dodgeTimeScaleIsChanged = nowDodgeRealtime > 0;

        if(dodgeTimeScaleIsChanged)
        {
            nowDodgeRealtime -= Time.unscaledDeltaTime;
            if (Mathf.Abs(nowTimeScale - dodgeTimeScale) > 0.001f)
            {
                nowTimeScale = Mathf.Lerp(targetTimeScale,dodgeTimeScale,dodgeTimeScaleCurve.Evaluate(1-nowDodgeRealtime/dodgeRealtime));
            }
            if(nowDodgeRealtime <= 0)
            {
                nowTimeScale = targetTimeScale;
            }
        }

        if(mainTimeScaleIsChanged)
        {
            if (Mathf.Abs(nowTimeScale - targetTimeScale) > 0.001f)
            {
                nowTimeScale = Mathf.Lerp(nowTimeScale, targetTimeScale, Time.unscaledDeltaTime * 4);
            }
        }

        if(mainTimeScaleIsChanged || dodgeTimeScaleIsChanged)
        {
            Time.timeScale = nowTimeScale;
        }
    }

    public void SetTimeScale(float value)
    {
        targetTimeScale = value;
    }

    public void SetTemporaryTimeScale(float value, float realtime)
    {
        dodgeTimeScale = value;
        dodgeRealtime = realtime;
        nowDodgeRealtime = realtime;
    }

    public void ForceReset()
    {
        Time.timeScale = 1;
        targetTimeScale = 1;
        dodgeTimeScale = 1;
        dodgeRealtime = 0;
        nowDodgeRealtime = 0;
    }
}
