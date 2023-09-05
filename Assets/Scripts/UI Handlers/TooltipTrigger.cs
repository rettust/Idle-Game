using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{
    [TextArea(5,10)]
    public string content;
    public string header;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipManager.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Hide();
    }
}
