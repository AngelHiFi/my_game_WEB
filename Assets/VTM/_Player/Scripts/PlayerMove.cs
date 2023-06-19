using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	
	private bool isLive;

	private GameManager gameManager;    // 
	

	private float playerDestroy = -30.0f; // ����������� ������� ������ �� ������, ���� ������.

	// ����� ���������  ����������� � ������ (��� ��������) �� ������� �������
	private int curAmmo;              // ���������� �������� �������
    private int curAmmoMax = 30;      // ���������� ��������� ������������
	
	[SerializeField] public ShotMy myShot;

    [SerializeField] private AudioSource playerAudio;
    [SerializeField] public AudioClip jumpAudio;                  // ���� ������
	[SerializeField] public AudioClip bomb;                       // ���� �������
	[SerializeField] public AudioClip staff;                      // ���� �����
	[SerializeField] public AudioClip dead;                       // ���� Dead
	[SerializeField] public AudioClip takingDamageMin;            // ���� ����� ������
	[SerializeField] public AudioClip takingDamageMax;            // ���� ����� ������� 
    [SerializeField] public AudioClip takingDamageMidle;          // ���� ����� �������	
	
	[SerializeField] public AudioClip reload;                     // �����������
	[SerializeField] public AudioClip mis�;                       // ������ (���� ���)

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
		// ������ ������, ���� �������� �������
		if(transform.position.y < playerDestroy)
        {
			Dead();
        }


		// ������� �������
        if (Input.GetMouseButtonDown(1))
        {
            Attack_1(); 
        }
		
		 // ���� �������
        if (Input.GetMouseButtonDown(0))
		{
			Attack_2(); 
        }
		
		 // �����������
        if (Input.GetKeyDown(KeyCode.R) & curAmmo < 1)
        {
            playerAudio.PlayOneShot(reload);
            curAmmo = curAmmoMax;
        }
	}
	
	public void Dead()
	{
		isLive = false;
		animator.SetTrigger("dead");                                 // �������� ������
		characterController.enabled = !characterController.enabled;  // ���������� ������������
		playerMove.enabled = false;
		input_2.enabled = false;
		playerAudio.PlayOneShot(dead);                               // ����
        gameManager.GameOver();                 // ��������, ����� ���� �����������
	}
	
	
	public void Damage(int midleDamage)
	{
		if(isLive)
		{
			
		    if(midleDamage <=14)
			{
				animator.SetTrigger("take_damage_min");         // ����� ����� (��� �����) (0 - 14)
			}
			
			if(midleDamage >= 15 && midleDamage <=19)
			{
				animator.SetTrigger("take_damage_min");          // ����� ���� (15- 19)
				playerAudio.PlayOneShot(takingDamageMin);
			}
		
			if(midleDamage >= 20 && midleDamage <=24)  
			{
				animator.SetTrigger("take_damage_nidle");          // ������� ���� (20-24)
				playerAudio.PlayOneShot(takingDamageMidle);
			}
		
			if(midleDamage >=25)
			{
				animator.SetTrigger("take_damage_max");       // ������� ���� (25-30)
				playerAudio.PlayOneShot(takingDamageMax);
			}
		

		}
	}
	
	// �������
	private void Attack_1()
	{
		if(curAmmo > 0)
			{
				myShot.BombAtack();           // �������� � ����� �����
				animator.SetTrigger("attack_1");         
				playerAudio.PlayOneShot(bomb);
				curAmmo = curAmmo - 1;
			}
		
		if (curAmmo < 1)
			{
				playerAudio.PlayOneShot(mis�);    // ���� ������
			}
	}
	
	// �����
	private void Attack_2()
	{
		myShot.MeleeAtack();
		animator.SetTrigger("attack_2");                    
		playerAudio.PlayOneShot(staff); 
	}


    // �������� ������� �������� ��
    public void Move(Vector2 _input, bool _isAcceleration)
    {
        if (_input != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(_input.x, _input.y) * Mathf.Rad2Deg + camera.eulerAngles.y;  // ������� ������ � �������
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref currentVelocityRotate, smoothTime);
        }

        float targetSpeed = (_isAcceleration ? runSpeed : walkSpeed) * _input.magnitude;                      // ������� ��������: targetSpeed - ��� ��� ������
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocitySpeed, smoothTime);     // ������� �������� : �����������

        Vector3 velocity = transform.forward * currentSpeed + Vector3.up * gravity + Vector3.up * jumpVelocity;

        characterController.Move(velocity * Time.deltaTime);

        if (jumpVelocity > 0)
            jumpVelocity += gravity * Time.deltaTime;
        else if (jumpVelocity < 0)
            jumpVelocity = 0;

        // ����� targetSpeed �� ����� 0, �������� bool isMove (� ���������) �����������. � ����������� ������� �� �������� walk
        animator.SetBool("isMove", targetSpeed != 0);

        // �������� ��  �� (���), �� �������� �������������
        animator.SetFloat("movementSpeed", targetSpeed * 0.5f);

        // �� ����� �� �����
        animator.SetBool("isGround", characterController.isGrounded);

        if (characterController.isGrounded)
            animator.SetFloat("groundDistance", GetGroundDistance());
    }

    public void Jump()
    {
        if (characterController.isGrounded)           // ���� �� ����� �� �����
        {
            jumpVelocity = -gravity * jumpForce;
            animator.SetTrigger("jump");              // ������ �������� ����� ������� 
            playerAudio.PlayOneShot(jumpAudio);       // ���� ������
        }
    }

    // ������� ���������� �� �����
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

