using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentationManager : MonoBehaviour
{
    List<GameObject> presentationSlides = new List<GameObject>();

    private void Start()
    {
        foreach (Transform slide in transform)
        {
            presentationSlides.Add(slide.gameObject);
        }
    }
    public void turnThemAllOffButThis(GameObject activatedSlide) {

        foreach (var slide in presentationSlides)
        {
            if (activatedSlide!= slide)
            {
                slide.GetComponent<presentationSlide>().OnDeactivation();
                slide.GetComponent<presentationSlide>().isOnlyActive = false;
                slide.SetActive(false);
            }
        }
    }
}
