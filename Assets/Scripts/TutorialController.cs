using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private GameObject dragUI;
    [SerializeField] private GameObject loseUI;

    public void AdvanceTutorial()
    {
        if (dragUI.activeSelf)
        {
            dragUI.SetActive(false);
            loseUI.SetActive(true);
        }
        else if (loseUI.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
