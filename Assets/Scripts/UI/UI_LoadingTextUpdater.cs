using UnityEngine;
using TMPro;

public class UI_LoadingTextUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text targetText;

    int index;
    float timecounter;

    readonly static string loadingstr = "̻▫▪■□■▪▫";

    void Start()
    {
        timecounter = 0;

        index = 0;
        targetText.text = ""+loadingstr[index];//より良い方法がある気がする
    }

    void Update()
    {
        timecounter += Time.deltaTime;
        if (timecounter > 0.3f)
        {
            timecounter = 0;
            index = (index + 1) % loadingstr.Length;
            targetText.text = ""+loadingstr[index];
        }
    }
}
