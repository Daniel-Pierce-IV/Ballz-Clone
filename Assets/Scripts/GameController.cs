using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private UnityEvent allBallsDeactivated;

    public void DeactivateBalls()
    {
        allBallsDeactivated.Invoke();
    }
}
