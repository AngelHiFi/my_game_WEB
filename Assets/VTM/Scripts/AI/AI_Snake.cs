using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



// ��� - ���������� ������ AI_Controll

public class AI_Snake : MonoBehaviour
{
    [SerializeField] private float agrEnemies = 15;     // ����������, �� ������� ���� ����� ��������
    [SerializeField] private float distanceAttack = 3;  // ����������, �� ������� ���� ����� ��������
    [SerializeField] private bool  isDelay;             // �������� ��� �����

    //private Transform player;                           // �� ���� ����� �������� ���� (�����)
    private GameObject player;
    private Transform myTransform;                       // ���������� �����
    public Transform target;                             // ����� �����!
    private int rotationSpeed = 3;                       // �������� ��������

    private Animator animator;
    private Rigidbody rb;
    NavMeshAgent agent;

    private bool isLive;                                  // ��������, ��� �� - ����
    private bool isPlayer;                                // ��������, ��� �� - �����


    // �����
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] public AudioClip attackSound;
    [SerializeField] public AudioClip takeDamage;
    [SerializeField] public AudioClip dead;


    [SerializeField] public ShotEnemy shotEnemy;          // ����� ����� ��� �����


   // float timer;                                     // ������� � 1, ������� ��� ��������� �������� � ����� ��������������
   // List<Transform> points = new List<Transform>();  // ����� ��������������




    private void Awake()
    {
        myTransform = transform;                               // ������� ��������� �����
        player = GameObject.FindGameObjectWithTag("Player");   // ����� ������
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

        //timer = 0;                                                                      // ������� � 2, ������������� ������� � ����
        //Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;  // ����� ����� ��������������
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
            // num = ���������� ����� ������� � ������
            float num = Vector3.Distance(transform.position, player.transform.position);    //
            
            //float num = Vector3.Distance(animator.transform.position, player.position);

            // ����� � ���� ����, ���� �����. ����� ���.
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


            // ����� ��� ���� ����, ����������� �����
            if(num > agrEnemies)
            {
                animator.SetBool("isAttack", false);
                animator.SetBool("isRun", false);
                animator.SetBool("isWalk", false);
                //timer += Time.deltaTime;              // ������� � 3, ��������� �������
            }

            // ������� � 4, ��������� ��������������
            //if( timer > 5)
            //{
                //agent.speed = 2;
                //animator.SetBool("isWalk", true);   // ����, �� ���� ������ ������ 5 ���, �������� �������� ������
                //animator.SetBool("isRun", false);
                //animator.SetBool("isAttack", false);
            //}

            // ����� � ��������������
            //if(timer > 10)
            //{
               // animator.SetBool("isWalk", false); 
                //animator.SetBool("isRun", false);
               // animator.SetBool("isAttack", false);
            //}

            // ���� ������� 
            if(num <= distanceAttack)
            {
                animator.SetBool("isWalk", false);
                animator.SetBool("isRun", false);
                animator.SetBool("isAttack", false);   // ����� ���, ���� ����� �� �������

                if (isDelay)
                {
                    StartCoroutine(DelayAttack());
                    animator.SetBool("isAttack", true);
                    isDelay = false;
                    // Attack_Snake();                        // ������� � ����� ������. ��������.
                }
            }
        }


        
    }

    // �������� ����� �����
    IEnumerator DelayAttack()    
    {
        yield return new WaitForSeconds(1);
        isDelay = true;
    }


    // ����� ����� ������������ ����� ��������. ��� ����� ������� ������� �����!
    private void Attack_Snake()
    {
        playerAudio.PlayOneShot(attackSound);
        shotEnemy.EnemyMageAttack();              // ����� ����������� �����
    }

    // ���� �������� - ���� (���� ����, ��������)
    public void TakeDamage()
    {
        animator.SetBool("isWalk", false);
        animator.SetBool("isRun", false);
        animator.SetTrigger("Take_Damage");
        playerAudio.PlayOneShot(takeDamage);
    }


    // ������ �����. � ���� ����� �������� �� Healt_Enemy
    public void Dead()
    {
        isLive = false;
        Debug.Log("Dead");
        animator.SetBool("isWalk", false);
        animator.SetBool("isRun", false);
        animator.SetBool("isAttack", false);
        animator.SetTrigger("Dead");
        playerAudio.PlayOneShot(dead);
        Destroy(gameObject, 5.0f);               // �������� ����� ����� � ������
    }

    public void DeadPlayer()
    {
        isPlayer = false;
    }

}
