using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallUIManager : MonoBehaviour
{
    [SerializeField] private Text ballText;
    [SerializeField] private float yOffset = -1f;

    public void EnableBallText(bool enable)
    {
        ballText.enabled = enable;
    }

    public void RepositionBallText(Vector3 launchPosition)
    {
        launchPosition.y += yOffset;
        Vector3 ballTextPosition = Camera.main.WorldToScreenPoint(launchPosition);
        ballText.rectTransform.position = ballTextPosition;
    }

    public void UpdateBallText(int numOfBalls)
    {
        ballText.text = numOfBalls + "x";
    }
}
