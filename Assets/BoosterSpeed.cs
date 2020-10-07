using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSpeed : MonoBehaviour
{
    [SerializeField] private float boostSpeed = 1;
    [SerializeField] float delayToDestroy = 2;
    bool isActive = false;
    MeshRenderer mesh;
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActive)
        {
            isActive = true;
            PlayerController.instance.AddSpeed(boostSpeed); 
            StartCoroutine(DestroyObj());
            if (mesh != null) mesh.enabled = false;
        }
    }

    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(delayToDestroy);
    }
}
