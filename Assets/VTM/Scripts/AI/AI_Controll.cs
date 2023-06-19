using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// новый скрипт

public class AI_Controll : MonoBehaviour
{
    private bool isLive;    // враг жив
	private bool isPlayer;  // игрок жив
	
	private bool isTP = true;                       // задержка для метода Атака

    


    [SerializeField] public ShotEnemy shotEnemy;   // класс атаки для Врага

    [Range(0, 360)] public float ViewAngle = 90f;  
    public float ViewDistance = 15f;               // расстояние -  Враг перестает преследование
    public float DetectionDistance = 7f;           // расстояние - Враг начинает преследование
    public Transform EnemyEye;                     // как бы глаза врага 
    public Transform Target;                       // сюды префаб игрока закидываем

    private UnityEngine.AI.NavMeshAgent agent;                 
    private float rotationSpeed;                  
    private Transform agentTransform;

    private Animator animator;
	
	[SerializeField] private AudioSource playerAudio;
	[SerializeField] public AudioClip dead;                  // звук Dead
	[SerializeField] public AudioClip takingDamage;          // звук получение урона (боль) 
	[SerializeField] public AudioClip attackSound;           // звук нанесения урона
	
	private Rigidbody rb;
 
    private void Start()
    {
        isLive = true;
		isPlayer = true;

        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.updateRotation = false;
        rotationSpeed = agent.angularSpeed;
        agentTransform = agent.transform;
		
		rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        // проверка расстония до Игрока
        float distanceToPlayer = Vector3.Distance(Target.transform.position, agent.transform.position);

        // Игрок в доступности ИЛИ пошел на отрыв
        if (distanceToPlayer <= DetectionDistance & distanceToPlayer > 3 || IsInView())
        {
			if(isPlayer)
			{
		      RotateToTarget(); // поворот к Игроку
              MoveToTarget();   // бег к Игроку (сближение)
			}

        }
		
        DrawViewState();

        // при отрыве Враг переходит в idle
        if (distanceToPlayer > DetectionDistance)
        {
            IdleEnemy();
        }


        // при такой разнице - Враг атакует
        if (distanceToPlayer <= 3 & isPlayer)
        {
			agent.enabled = false;
			animator.SetBool("isMove", false);
				
			if(isTP)
			{
				Attack();
				isTP = false;
				StartCoroutine(TPtime());
			}
		}

    }
	
    // задержка для начала другой Атаки
	IEnumerator TPtime()
	{
		yield return new WaitForSeconds(3); 
		isTP = true;
    }
	


    // враг умер
    public void Dead()
    {
        isLive = false;
        animator.SetTrigger("dead");
       // Debug.Log("враг умер");
        agent.enabled = false;
		playerAudio.PlayOneShot(dead);                            
        Destroy(gameObject, 5.0f);               // удаление трупа
    }
	
	// игрок умер
	public void DeadPlayer()
	{
		isPlayer = false;
		Debug.Log("Игрок умер");
		// agent.enabled = true;
		// rb.AddForce(Vector3.forward * 55.0f); // и так бежит, нав меш работает.
        // animator.SetBool("walk", true);
	}

    // Проверка рей кастом - видно ли цель
    private bool IsInView() // true - видно
    {
        float realAngle = Vector3.Angle(EnemyEye.forward, Target.position - EnemyEye.position);
        RaycastHit hit;
        if (Physics.Raycast(EnemyEye.transform.position, Target.position - EnemyEye.position, out hit, ViewDistance))
        {
            if (realAngle < ViewAngle / 2f && Vector3.Distance(EnemyEye.position, Target.position) <= ViewDistance && hit.transform == Target.transform)
            {
                return true;
            }
        }
        return false;
    }
    private void RotateToTarget() // поворот к цели
    {
        if(isLive)
        {
            Vector3 lookVector = Target.position - agentTransform.position;
            lookVector.y = 0;
            if (lookVector == Vector3.zero) return;
            agentTransform.rotation = Quaternion.RotateTowards
                (
                    agentTransform.rotation,
                    Quaternion.LookRotation(lookVector, Vector3.up),
                    rotationSpeed * Time.deltaTime
                );
        }
    }



    // враг бежит к цели
    private void MoveToTarget()
    {
        if (isLive)
        {
            agent.enabled = true;
            agent.SetDestination(Target.position);
            animator.SetBool("isMove", true);
			//Debug.Log("мы в MoveToTarget");
        }
    }

    // враг атакует
    private void Attack()
    {
		if(isLive)
			{
                animator.SetTrigger("attack_1");
				//playerAudio.PlayOneShot(attackSound);  //
				//shotEnemy.EnemyMageAttack();           // сообщение на выдачу урона
            }
	}

    //метод дл Аниматора
    private void Attack_Animator()
    {
        playerAudio.PlayOneShot(attackSound);
        shotEnemy.EnemyMageAttack();
    }
	



    // получение урона (звук боли, анимация)
    public void TakeDamage()
    {
        bool isYes = true;  // чтобы убрать зацикленность
        

        if (isYes)
        {
            if (isLive)
            {
                agent.enabled = false;
                animator.SetBool("isMove", false);
                animator.SetTrigger("hit");
               // Debug.Log("попадание");
                isYes = false;
				playerAudio.PlayOneShot(takingDamage);
            }
        }
    }

    // враг потерял АГР
    public void IdleEnemy()
    {
        if(isLive)
        {
            
            animator.SetBool("isMove", false);
			// Debug.Log("мы в IdleEnemy");
            
        }
    }



    private void DrawViewState()
    {
        Vector3 left = EnemyEye.position + Quaternion.Euler(new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);
        Vector3 right = EnemyEye.position + Quaternion.Euler(-new Vector3(0, ViewAngle / 2f, 0)) * (EnemyEye.forward * ViewDistance);
        Debug.DrawLine(EnemyEye.position, left, Color.yellow);
        Debug.DrawLine(EnemyEye.position, right, Color.yellow);
    }

}
