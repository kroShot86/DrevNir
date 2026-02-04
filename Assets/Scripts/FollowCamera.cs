using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 needPosition = target.position + offset;
        Vector3 resPosition = Vector3.Lerp(transform.position, needPosition, smoothSpeed);
        transform.position = resPosition;
    }
}
