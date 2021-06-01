using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private UnityEvent allBallsDeactivated;

    void Update()
    {
        // TODO this is for manual testing level phases. Remove later
        if(Input.GetKeyDown(KeyCode.Space)) DeactivateBalls();
    }

    public void DeactivateBalls()
    {
        allBallsDeactivated.Invoke();
    }
}
