using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ButtonMover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject toActivateObj;

    bool isPointerStay;

    Vector3 startPosition;
    RectTransform rect;

    void Start()
    {
        rect = transform as RectTransform;
        startPosition = rect.anchoredPosition;
        toActivateObj.SetActive(false);
    }

    void Update()
    {
        if(isPointerStay)
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition,startPosition+new Vector3(20,0,0),Time.deltaTime*8);
        }else
        {
            rect.anchoredPosition = Vector3.Lerp(rect.anchoredPosition,startPosition,Time.deltaTime*8);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerStay = true;
        toActivateObj.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerStay = false;
        toActivateObj.SetActive(false);
    }
}
