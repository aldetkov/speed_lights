using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    static int score = 0;
    [SerializeField] float delayToDestroy = 2;
    bool isActive = false;
    MeshRenderer mesh;

    [SerializeField] private ParticleSystem takeVFX = null;
    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isActive)
        {
            isActive = true;
            StartCoroutine(DestroyObj());
            score++;
            if (mesh != null) mesh.enabled = false;
            SoundsManager.instance.takeCoinAudio.Play();
            UIManager.instance.OnUpdateScore(score);
        }
    }

    IEnumerator DestroyObj()
    {
        takeVFX.Play();
        yield return new WaitForSeconds(delayToDestroy);
    }
}
