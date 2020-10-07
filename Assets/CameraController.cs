using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [SerializeField] private Transform targetT = null;

    Vector3 distance;
    Transform _cashTransform;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        _cashTransform = transform;
    }
    private void Start()
    {
       // Cursor.lockState = CursorLockMode.Locked; // отключение курсора

        distance = _cashTransform.position - targetT.position;
    }
    public void UpdatePosCamera()
    {
        Quaternion rotation = Quaternion.Euler(0, targetT.eulerAngles.y, 0);
        _cashTransform.position = targetT.position + (rotation * distance);
        _cashTransform.eulerAngles = new Vector3(_cashTransform.eulerAngles.x, targetT.eulerAngles.y, _cashTransform.eulerAngles.z);
    }



}
