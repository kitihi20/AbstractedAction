using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] RectTransform moverrect;
    [SerializeField] float afterScale = 1.3f;

    bool isPointerStay;

    Vector3 startScale;

    void Start()
    {
        startScale = moverrect.sizeDelta;
    }

    void Update()
    {
        if(isPointerStay)
        {
            moverrect.sizeDelta = Vector3.Lerp(moverrect.sizeDelta,startScale*afterScale,Time.deltaTime*8);
        }else
        {
            moverrect.sizeDelta = Vector3.Lerp(moverrect.sizeDelta,startScale,Time.deltaTime*8);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerStay = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerStay = false;
    }
}
