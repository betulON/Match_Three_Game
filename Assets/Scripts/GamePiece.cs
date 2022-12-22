using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class GamePiece : MonoBehaviour
{
    int xIndex;
    int yIndex;

    public InterpolationType interpolation = InterpolationType.SmootherStep;

    public enum InterpolationType
    {
        Linear,
        EaseOut,
        EaseIn,
        SmoothStep,
        SmootherStep
    }

    public void setCoordinates(int x, int y)
    {
        xIndex = x;
        yIndex = y;
    }

    public void Move(int destX, int destY, float timeToMove)
    {
        StartCoroutine(MoveCoroutine(new Vector3(destX, destY, 0), timeToMove));
    }

    IEnumerator MoveCoroutine(Vector3 destination, float timeToMove)
    {
        bool destinationReached = false;
        float elapsedTime = 0f;
        Vector3 startPosition = transform.position;

        while (!destinationReached)
        {
            if (Vector3.Distance(destination, transform.position) < Mathf.Epsilon)
            {
                destinationReached = true;
                transform.position = destination;
                setCoordinates((int)destination.x, (int)destination.y);
            }

            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp(elapsedTime / timeToMove, 0f, 1f);
            
            switch (interpolation)
            {
                case InterpolationType.Linear:
                    break;
                case InterpolationType.EaseOut:
                    t = Mathf.Sin(t * Mathf.PI * 0.5f);
                    break;
                case InterpolationType.EaseIn:
                    t = 1 - Mathf.Cos(t * Mathf.PI * 0.5f);
                    break;
                case InterpolationType.SmoothStep:
                    t = t*t*(3 - 2*t);
                    break;
                case InterpolationType.SmootherStep:
                    t =  t*t*t*(t*(t*6 - 15) + 10);
                    break;
            }
            transform.position = Vector3.Lerp(startPosition, destination, t);
        }
        yield return null;
    }
}
