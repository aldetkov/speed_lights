using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    bool isFinish = false;
    int countTap = 0;
    [SerializeField] private float speedForTap = 1;
    private void Update()
    {
        if (isFinish && Input.GetKeyDown(KeyCode.Mouse0)) {
            PlayerController.instance.AddSpeed(speedForTap);
            countTap++;
            UIManager.instance.OnUpdateTapInterface(countTap);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFinish = true;
            Debug.Log("Open");
            UIManager.instance.OnOpenTapInterface();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isFinish = false;
            PlayerController.instance.Finish();
        }
    }
}
