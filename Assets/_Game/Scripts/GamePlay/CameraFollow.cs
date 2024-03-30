using System;
using UnityEngine;
public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField] private Transform tf;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 offsetMax;
    [SerializeField] private Vector3 offsetMin;
    [SerializeField] private Transform[] offsets;
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] private Transform target;
    public Camera cam;
    private CameraState state;
    [SerializeField]private Vector3 targetOffset;
    private Quaternion targetRotate;

    private void Awake() {
        cam = Camera.main;
    }
    
    public void SetRateOffset(float rate) {
        if (state != CameraState.Gameplay)  return;
        targetOffset = Vector3.Lerp(offsetMin, offsetMax, rate);
    }

    public void ChangeState(CameraState state) {
        this.state = state;
        targetOffset = offsets[(int)state].localPosition;
        targetRotate = offsets[(int)state].localRotation;
    }
    private void FixedUpdate() {
        offset = Vector3.Lerp(offset, targetOffset, Time.fixedDeltaTime * moveSpeed);
        tf.rotation = Quaternion.Lerp(tf.rotation, targetRotate, Time.fixedDeltaTime * moveSpeed);
        tf.position = Vector3.Lerp(tf.position, target.position + targetOffset, Time.fixedDeltaTime * moveSpeed);
    }
}