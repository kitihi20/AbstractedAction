using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;

    [SerializeField] float startTimeScale = 1;

    float nowTimeScale;
    float targetTimeScale;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Time.timeScale = startTimeScale;
        nowTimeScale = startTimeScale;
        targetTimeScale = startTimeScale;
    }

    void Update()
    {
        if (Mathf.Abs(nowTimeScale - targetTimeScale) > 0.001f)
        {
            nowTimeScale = Mathf.Lerp(nowTimeScale, targetTimeScale, Time.unscaledDeltaTime * 4);
            Time.timeScale = nowTimeScale;
        }
    }

    public void SetTimeScale(float value)
    {
        targetTimeScale = value;
    }
}
