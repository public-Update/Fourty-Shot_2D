using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    private Image joystick;
    private Image Handle;
    Vector2 ImputVector;

    void Start()
    {
        joystick = GetComponent<Image>();
        Handle = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 poss;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystick.rectTransform, eventData.position, null, out poss))
        {
            poss.x = (poss.x / joystick.rectTransform.sizeDelta.x);
            poss.y = (poss.y / joystick.rectTransform.sizeDelta.y);
            ImputVector = new Vector2(poss.x, poss.y);
            ImputVector = (ImputVector.magnitude > 1f) ? ImputVector.normalized : ImputVector;

            Handle.rectTransform.anchoredPosition = new Vector2
                (ImputVector.x * (joystick.rectTransform.sizeDelta.x / 2), 
                ImputVector.y * (joystick.rectTransform.sizeDelta.y / 2));

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ImputVector = Vector2.zero;
        Handle.rectTransform.anchoredPosition = Vector2.zero;
    }

    public float Horizontal()
    {
        if (ImputVector.x != 0)
            return ImputVector.x;
        else
            return Input.GetAxisRaw("Horizontal");
    }
    
    public float Vertical()
    {
        if (ImputVector.y != 0)
            return ImputVector.y;
        else
            return Input.GetAxisRaw("Vertical");
    }

}
