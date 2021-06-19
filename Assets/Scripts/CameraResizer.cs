using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraResizer : MonoBehaviour
{
    [SerializeField] private Transform levelBoundaryLeft;
    [SerializeField] private Transform levelBoundaryRight;

    // Start is called before the first frame update
    void Start()
    {
        float levelWidth = Mathf.Abs(levelBoundaryLeft.position.x
            - levelBoundaryRight.position.x);

        Camera.main.orthographicSize = levelWidth / Camera.main.aspect / 2;
    }
}
