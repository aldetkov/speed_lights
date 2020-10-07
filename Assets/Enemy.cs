using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum EnemyType
    {
        Killer,
        Slower
    }

    [SerializeField] private float slowPlayer = 2;

    [SerializeField] private EnemyType enemyType = EnemyType.Killer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch(enemyType)
            {
                case EnemyType.Killer:
                    PlayerController.instance.Death();
                    break;
                case EnemyType.Slower:
                    PlayerController.instance.AddSpeed(-slowPlayer);
                    break;
            }
        }
    }
}
