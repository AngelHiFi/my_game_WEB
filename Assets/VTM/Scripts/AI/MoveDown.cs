using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private AudioSource playerAudio;
	public AudioClip deth;             // ���� ������   
    public float speed = 6.0f;         // �������� �����
    private float zDestroy = - 20.0f;  // ������� ����������� ��������
    private Rigidbody objectRb;
	private GameManager gameManager;   // 
	private int pointValue = 1;        // ���� ������� ���������, ����� ������� ���� ���� ���� ���������
	
	public GameObject explosionFx;

   
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
		playerAudio = GetComponent<AudioSource>();
		
		// ���� ������ Game Manager (��������) � ����������� ������ 
		gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    void Update()
    {
        objectRb.AddForce(Vector3.forward * -speed);

        if(transform.position.z < zDestroy)
        {
            Destroy(gameObject);
			Debug.Log("���� ������");
			gameManager.GameOver();  // ���� ��� ������, ���� ����������� (��������)
        }
        
    }
	
		// ��� ������������ � ��������� (�����)
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Bullet"))
        {
		    Explode();                            // ������� ��������
			playerAudio.PlayOneShot(deth, 1.0f);
			Debug.Log("���� ������");
            gameManager.UpdateScore(pointValue);  // �������� �������� � ��������
		    Destroy(other.gameObject);            // ���������� ����
		    Destroy(gameObject);                  // ���������� ������ (�����)
        }
    }
	
	void Explode()
	{
		Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);
	}
}
