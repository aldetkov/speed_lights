using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Скорость персонажа")]
    [SerializeField] private float speed = 100;
    [SerializeField] private float speedDeviation = 10;

    [Header("Отступ от краёв недоступный для движения")]
    [SerializeField] private float distanceCheckDown = 1;

    [Header("Сила и время прыжка")]
    [SerializeField] private float forceJump = 20;
    [SerializeField] private float timeJump = 20;

    [Header("Гравитация")]
    [SerializeField] private float forceGravity = 10;

    [Header("Эффекты")]
    [SerializeField] private ParticleSystem deathVFX = null;

    [Header("Начало финиша")]
    [SerializeField] private Transform pointFinish;

    bool isFinish = false;

    // параметры поворота
    bool isTurn = false;
    Turn turn;
    float step;
    float t;

    Transform _cashTransform;
    bool isJump = false;
    bool isDead = false;

    float distanceToFinish = 0;

    Animator animator;
    CharacterController cc;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        _cashTransform = transform;
        distanceToFinish = Vector3.Distance(_cashTransform.position, pointFinish.position);
    }

    void FixedUpdate()
    {
        Vector3 targetMove = Vector3.zero;

        // Перемещение и прыжок
        if (!isDead && !isFinish)
        {
            targetMove += _cashTransform.forward.normalized * speed;
            float x = Input.GetAxisRaw("Horizontal");

            //Проверяем есть ли в стороне движения земля
            if (x != 0 && Physics.Raycast(_cashTransform.position + x * _cashTransform.right * distanceCheckDown + Vector3.up, Vector3.down * 100f))
                targetMove += x * _cashTransform.right * speedDeviation;


            if (Input.GetKey(KeyCode.Space) && !isJump && cc.isGrounded) StartCoroutine(Jump());
        }

        // гравитация
        if (!cc.isGrounded && !isJump) targetMove -= _cashTransform.up * forceGravity;

        cc.Move(targetMove * Time.fixedDeltaTime);

        if (isTurn) Turn();

        CameraController.instance.UpdatePosCamera();
        UpdateUIProgression();
    }

    private void OnTriggerEnter(Collider other)
    {
 
        if (other.gameObject.CompareTag("Turn"))
        {
            t = 0;
            turn = other.gameObject.GetComponent<Turn>();
            step = speed / turn.GetDistanceTurn() * Time.fixedDeltaTime;
            isTurn = true;

        }
    }

    public void AddSpeed (float speedAdd)
    {
        speed += speedAdd;
    }
    public void Death()
    {
        isDead = true;
        SoundsManager.instance.deathAudio.Play();
        deathVFX.Play();
        animator.SetTrigger("Death");
    }

    public void Finish ()
    {
        StartCoroutine(Jump());
        isFinish = true;
        StartCoroutine(FinishSlowMotion());
    }
    IEnumerator FinishSlowMotion()
    {

        Time.timeScale = 0.5f;
        while (true)
        {   
            Vector3 targetMove = Vector3.forward * speed + Vector3.down * 0.05f; // корректировка гравитации, чтобы верно считывал приземление
            cc.Move(targetMove * Time.fixedDeltaTime);
            if (cc.isGrounded)
            {
                break;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1f;
        UIManager.instance.OnOpenGameOverMenu("Вы прошли игру :)");

    }
    IEnumerator Jump ()
    {
        isJump = true;
        animator.SetTrigger("Jump");
        SoundsManager.instance.jumpAudio.Play();
        for (int i = 0; i < timeJump; i++)
        {
            cc.Move(Vector3.up * forceJump/timeJump*Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        isJump = false;
    }

    public void Turn ()
    {
            t += step;
            if (t >= 1)
            {
                isTurn = false;
                t = 1;
            }
            // Поворачиваем игрока в сторону поворота кривой Безье
            _cashTransform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(turn.bezierPos[0].position, turn.bezierPos[1].position,
                 turn.bezierPos[2].position, turn.bezierPos[3].position, t));
    }

    void UpdateUIProgression()
    {
        float distanceRate = (distanceToFinish - Vector3.Distance(_cashTransform.position, pointFinish.position))/distanceToFinish;
        if (distanceRate > 1) distanceRate = 1;
        else if (distanceRate < 0) distanceRate = 0;
        UIManager.instance.OnUpdateSliderProgress(distanceRate);
    }
}
