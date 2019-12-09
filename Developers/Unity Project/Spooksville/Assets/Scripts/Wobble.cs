using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private float sequenceTime;
    private bool isInSequence;
    private bool isReturningToOrginalPosition;

    public float force = 1.0f;

    private void Start()
    {
        originalPosition = transform.position;
        startPosition = transform.position;
    }

    void Update()
    {
        InitializeSequence();

        transform.position = Vector3.Lerp(startPosition, endPosition, sequenceTime);

        sequenceTime += Time.deltaTime;

        if (sequenceTime > 0.98f)
        {
            startPosition = transform.position;
            isReturningToOrginalPosition = !isReturningToOrginalPosition;
            isInSequence = false;
        }
    }

    private void InitializeSequence()
    {
        if (isInSequence) return;
        isInSequence = true;

        sequenceTime = 0f;

        if (isReturningToOrginalPosition)
        {
            endPosition = originalPosition;
            return;
        }

        var randomLerpDirection = Random.insideUnitCircle.normalized * force;
        endPosition = originalPosition + (Vector3)randomLerpDirection;
    }
}
