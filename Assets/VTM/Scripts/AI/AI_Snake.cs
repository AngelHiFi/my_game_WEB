using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



// ЭТО - Улучшенный скрипт AI_Controll

public class AI_Snake : MonoBehaviour
{
    [SerializeField] private float agrEnemies = 15;     // расстояния, на котором враг будет агриться
    [SerializeField] private float distanceAttack = 3;  // расстояния, на котором враг будет атаковть
    [SerializeField] private bool  isDelay;             // задержка для Атаки

    //private Transform player;                           // на кого будет агриться враг (плеер)
    private GameObject player;
    private Transform myTransform;                       // координаты врага
    public Transform target;                             // глаза врага!
    private int rotationSpeed = 3;                       // скорость поворота

    private Animator animator;
    private Rigidbody rb;
    NavMeshAgent agent;

    private bool isLive;                                  // проверка, жив ли - Враг
    private bool isPlayer;                                // проверка, жив ли - Игрок


    // ЗВУКИ
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] public AudioClip attackSound;
    [SerializeField] public AudioClip takeDamage;
    [SerializeField] public AudioClip dead;


    [SerializeField] public ShotEnemy shotEnemy;          // класс атаки для Врага


   // float timer;                                     // Патруль № 1, счетчик для состояния перехода в режим патрулирования
   // List<Transform> points = new List<Transform>();  // точки патрулирования




    private void Awake()
    {
        myTransform = transform;                               // текущее положение врага
        player = GameObject.FindGameObjectWithTag("Player");   // поиск плеера
        target = player.transform;
        
    }



    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        //agent = animator.GetComponent<NavMeshAgent>();
        agent = GetComponent<NavMeshAgent>();
        playerAudio = GetComponent<AudioSource>();

        isDelay = true;

        isLive = true;
        isPlayer = true;

        //timer = 0;                                                                      // Патруль № 2, устанавливаем счетчик в ноль
        //Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;  // поиск точек патрулирования
        //foreach (Transform t in pointsObject)
        //points.Add(t);
        //agent.SetDestination(points[0].position);

    }

  
    private void FixedUpdate()
    {

        //if (agent.remainingDistance <= agent.stoppingDistance)
        //   agent.SetDestination(points[Random.Range(0, points.Count)].position);


        if (player !=null  && isLive && isPlayer)
        {
            // num = расстояние между игроком и врагом
            float num = Vector3.Distance(transform.position, player.transform.position);    //
            
            //float num = Vector3.Distance(animator.transform.position, player.position);

            // игрок в зоне агра, враг бежит. атаки нет.
            if (num < agrEnemies && num > distanceAttack)
            
            {
                //animator.transform.LookAt(player);
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
                myTransform.position += myTransform.forward * (rotationSpeed * Time.deltaTime);

                agent.speed = 4;
                animator.SetBool("isRun", true);
                animator.SetBool("isAttack", false);
                animator.SetBool("isWalk", false);
                //timer = 0;
            }


            // игрок вне зоны агра, Бездействие Врага
            if(num > agrEnemies)
            {
                animator.SetBool("isAttack", false);
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", false);
                //timer += Time.deltaTime;              // Патруль № 3, запускаем счетчик
            }

            // Патруль № 4, запускаем патрулирование
            //if( timer > 5)
            //{
                //agent.speed = 2;
                //animator.SetBool("isWalk", true);   // враг, не видя игрока больше 5 сек, начинает анимацию хотьбы
                //animator.SetBool("isRun", false);
                //animator.SetBool("isAttack", false);
            //}

            // Пауза в Патрулировании
            //if(timer > 10)
            //{
               // animator.SetBool("isWalk", false); 
                //animator.SetBool("isRun", false);
               // animator.SetBool("isAttack", false);
            //}

            // враг атакует 
            if(num <= distanceAttack)
            {
                animator.SetBool("isWalk", false);
                animator.SetBool("isRun", false);
                animator.SetBool("isAttack", false);   // атаки нет, пока пауза не пройдет

                if (isDelay)
                {
                    StartCoroutine(DelayAttack());
                    animator.SetBool("isAttack", true);
                    isDelay = false;
                    // Attack_Snake();                        // переход к атаке отсюда. Неточный.
                }
            }
        }


        
    }

    // задержка атаки врага
    IEnumerator DelayAttack()    
    {
        yield return new WaitForSeconds(1);
        isDelay = true;
    }


    // метод Атака подключается через анимацию. Для более точного момента атаки!
    private void Attack_Snake()
    {
        playerAudio.PlayOneShot(attackSound);
        shotEnemy.EnemyMageAttack();              // вызов технической атаки
    }

    // Враг получает - урон (звук боли, анимация)
    public void TakeDamage()
    {
        animator.SetBool("isWalk", false);
        animator.SetBool("isRun", false);
        animator.SetTrigger("Take_Damage");
        playerAudio.PlayOneShot(takeDamage);
    }


    // Смерть Врага. В этот метод попадаем из Healt_Enemy
    public void Dead()
    {
        isLive = false;
        Debug.Log("Dead");
        animator.SetBool("isWalk", false);
        animator.SetBool("isRun", false);
        animator.SetBool("isAttack", false);
        animator.SetTrigger("Dead");
        playerAudio.PlayOneShot(dead);
        Destroy(gameObject, 5.0f);               // удаление трупа через х секунд
    }

    public void DeadPlayer()
    {
        isPlayer = false;
    }

}
