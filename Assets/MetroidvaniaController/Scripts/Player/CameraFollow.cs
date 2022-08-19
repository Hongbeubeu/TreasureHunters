using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform Target;
    private CharacterGround ground;

    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.1f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    private float preHeight = -99f;

    void Awake()
    {
        Cursor.visible = false;
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }

        ground = Target.transform.GetComponent<CharacterGround>();
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        preHeight = Target.position.y;
    }

    private void Update()
    {
        var newPosition = Target.position;
        newPosition.z = -10;
        
//        if (ground.GetOnGround())
//        {
//            preHeight = Target.transform.position.y;
//        }
//        else
//        {
//            newPosition.y = preHeight;
//        }

        transform.position = Vector3.Lerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);

        if (!(shakeDuration > 0)) return;
        camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

        shakeDuration -= Time.deltaTime * decreaseFactor;
    }

    public void ShakeCamera()
    {
        originalPos = camTransform.localPosition;
        shakeDuration = 0.2f;
    }
}