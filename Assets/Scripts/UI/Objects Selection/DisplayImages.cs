using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayImages : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject charImage;

    private void Start()
    {
        charImage.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        charImage.SetActive(true);
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        charImage.SetActive(false);
    }
}
