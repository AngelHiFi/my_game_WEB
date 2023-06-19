using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	
	private bool isLive;

	private GameManager gameManager;    // 
	

	private float playerDestroy = -30.0f; // минимальная позиция игрока по высоте, ниже смерть.

	// чтобы сохранить  перезарядку и осечку (без патронов) из старого скрипта
	private int curAmmo;              // количество патронов текущее
    private int curAmmoMax = 30;      // количество паторонов максимальное
	
	[SerializeField] public ShotMy myShot;

    [SerializeField] private AudioSource playerAudio;
    [SerializeField] public AudioClip jumpAudio;                  // звук Прыжок
	[SerializeField] public AudioClip bomb;                       // звук Выстрел
	[SerializeField] public AudioClip staff;                      // звук Посох
	[SerializeField] public AudioClip dead;                       // звук Dead
	[SerializeField] public AudioClip takingDamageMin;            // звук урона слабый
	[SerializeField] public AudioClip takingDamageMax;            // звук урона сильный 
    [SerializeField] public AudioClip takingDamageMidle;          // звук урона средний	
	
	[SerializeField] public AudioClip reload;                     // перезарядка
	[SerializeField] public AudioClip misс;                       // осечка (пуль нет)

    [SerializeField] public float walkSpeed = 2.5f;
    [SerializeField] public float runSpeed = 3.5f;
    [SerializeField] public float gravity = -9.8f;

    [Space]
    [SerializeField] public float smoothTime = 0.1f;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float currentVelocitySpeed;

    [SerializeField] private float currentVelocityRotate;

    [Space]
    [SerializeField] public float jumpForce = 1.35f;
    [SerializeField] private float jumpVelocity = 0;

    [SerializeField] private CharacterController characterController;
	[SerializeField] private PlayerMove playerMove;
	[SerializeField] private Input_2 input_2;
	

    [SerializeField] private Animator animator;
    [SerializeField] private new Transform camera;
	
	

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
		playerMove = GetComponent<PlayerMove>();
		input_2 = GetComponent<Input_2>();
		
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        camera = Camera.main.transform;
		
		curAmmo = curAmmoMax;
		
		isLive = true;

		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
	
	private void Update()
	{
		// смерть игроку, ниже заданной позиции
		if(transform.position.y < playerDestroy)
        {
			Dead();
        }


		// выстрел ѓаубицы
        if (Input.GetMouseButtonDown(1))
        {
            Attack_1(); 
        }
		
		 // удар Џосохом
        if (Input.GetMouseButtonDown(0))
		{
			Attack_2(); 
        }
		
		 // перезарЯдка
        if (Input.GetKeyDown(KeyCode.R) & curAmmo < 1)
        {
            playerAudio.PlayOneShot(reload);
            curAmmo = curAmmoMax;
        }
	}
	
	public void Dead()
	{
		isLive = false;
		animator.SetTrigger("dead");                                 // анимация смерти
		characterController.enabled = !characterController.enabled;  // отключение контроллеров
		playerMove.enabled = false;
		input_2.enabled = false;
		playerAudio.PlayOneShot(dead);                               // звук
        gameManager.GameOver();                 // передача, чтобы игра закончилась
	}
	
	
	public void Damage(int midleDamage)
	{
		if(isLive)
		{
			
		    if(midleDamage <=14)
			{
				animator.SetTrigger("take_damage_min");         // самый малый (без звука) (0 - 14)
			}
			
			if(midleDamage >= 15 && midleDamage <=19)
			{
				animator.SetTrigger("take_damage_min");          // малый урон (15- 19)
				playerAudio.PlayOneShot(takingDamageMin);
			}
		
			if(midleDamage >= 20 && midleDamage <=24)  
			{
				animator.SetTrigger("take_damage_nidle");          // средний урон (20-24)
				playerAudio.PlayOneShot(takingDamageMidle);
			}
		
			if(midleDamage >=25)
			{
				animator.SetTrigger("take_damage_max");       // большой урон (25-30)
				playerAudio.PlayOneShot(takingDamageMax);
			}
		

		}
	}
	
	// Гаубица
	private void Attack_1()
	{
		if(curAmmo > 0)
			{
				myShot.BombAtack();           // передача в скрит Урона
				animator.SetTrigger("attack_1");         
				playerAudio.PlayOneShot(bomb);
				curAmmo = curAmmo - 1;
			}
		
		if (curAmmo < 1)
			{
				playerAudio.PlayOneShot(misс);    // звук осечки
			}
	}
	
	// Посох
	private void Attack_2()
	{
		myShot.MeleeAtack();
		animator.SetTrigger("attack_2");                    
		playerAudio.PlayOneShot(staff); 
	}


    // основная функция Движения ГГ
    public void Move(Vector2 _input, bool _isAcceleration)
    {
        if (_input != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg + camera.eulerAngles.y;  // перевод радиан в градусы
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentVelocityRotate, smoothTime);
        }

        float targetSpeed = (_isAcceleration ? runSpeed : walkSpeed) * _input.magnitude;                      // целевая скорость: targetSpeed - бег или ходьба
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocitySpeed, smoothTime);     // текущая скорость : сглаживание

        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * gravity + Vector3.up * jumpVelocity;

        characterController.Move(velocity * Time.deltaTime);

        if (jumpVelocity > 0)
            jumpVelocity += gravity * Time.deltaTime;
        else if (jumpVelocity < 0)
            jumpVelocity = 0;

        // когда targetSpeed не равна 0, параметр bool isMove (в аниматоре) срабатывает. И запускается переход на анимацию walk
        animator.SetBool("isMove", targetSpeed != 0);

        // анимация та  же (шаг), но скорость увеличивается
        animator.SetFloat("movementSpeed", targetSpeed * 0.5f);

        // ГГ стоит на земле
        animator.SetBool("isGround", characterController.isGrounded);

        if (characterController.isGrounded)
            animator.SetFloat("groundDistance", GetGroundDistance());
    }

    public void Jump()
    {
        if (characterController.isGrounded)           // если ГГ стоит на земле
        {
            jumpVelocity = -gravity * jumpForce;
            animator.SetTrigger("jump");              // запуск анимации через триггер 
            playerAudio.PlayOneShot(jumpAudio);       // звук прыжка
        }
    }

    // считаем расстояние до земли
    private float GetGroundDistance()
    {
        float downPoint = transform.position.y + characterController.center.y - characterController.height * 0.5f;
        Vector3 startPoint = transform.position;
        startPoint.y = downPoint + 0.1f;

        if (Physics.Raycast(startPoint, Vector3.down, out RaycastHit hit))
        {
            return (float)System.Math.Round(hit.distance - 0.1f, 2);
        }

        return float.MaxValue;
    }
}

