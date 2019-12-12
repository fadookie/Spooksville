using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wobble : MonoBehaviour
{
    private Vector3 originalPosition;
    private Vector3 endPosition;

    private float sequenceTime;
    private bool isInSequence;
    private bool isReturningToOrginalPosition;

    public float force = 25.0f;
    public float speed = 0.5f;
    public float time = 0.95f;

    private void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        InitializeSequence();

        transform.position = Vector3.Lerp(transform.position, endPosition, Time.deltaTime * speed);

        sequenceTime += Time.deltaTime;

        if (sequenceTime > time)
        {
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
