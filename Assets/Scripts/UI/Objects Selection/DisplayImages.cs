using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Eduardo Worked on this Script

public class DisplayImages : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objImage;

    private void Start()
    {
        objImage.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        objImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        objImage.SetActive(false);
    }
}
