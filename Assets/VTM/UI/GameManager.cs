using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField] Button pauseButton;                      // кнопка пауза. вызывает канвас паузы. Позже надо паузу через еск вызывать.
	[SerializeField] GameObject pauseCanvas;                  // кнопкИ в режиме паузы
	[SerializeField] GameObject gameOverCanvas;               // канвас конец игры
	[SerializeField] TextMeshProUGUI nameAndScore;            // итоги
    [SerializeField] public TextMeshProUGUI scoreText;        // выводим на экран, число убитых врагов в режиме реального времени
	

	[SerializeField] public static bool gameOver;

	private AudioSource playerAudio;
	public AudioClip win;                   // звук победы
	public AudioClip gameOFF;               // звук проигрыша

	[SerializeField] GameObject winText;               // сюды кидаем - канвас Победы


	[SerializeField] protected private float waveNumber { get; private set; }   // encapsulation - 
	[SerializeField] protected private string namePlayer { get; private set; }  // инкапсуляция

	protected private void Awake()
	{
		//LoadNameAndScore();      // Чтобы сцену проверить, надо комментить строку эту
	}


	void Start()
    {
		Time.timeScale = 1;
		waveNumber = 0;
        UpdateScore(0);
		playerAudio = GetComponent<AudioSource>();
	}
	


    // подсчет   вывод на экран
    public void UpdateScore(int scoreToAdd)
	{
		waveNumber += scoreToAdd;
		scoreText.text = "Score: " + waveNumber;

		if (waveNumber >= 5)
			Win();
	}

	// Победа
	public void Win()
    {
		playerAudio.PlayOneShot(win);           // фанфары
		winText.gameObject.SetActive(true);     // Поздравительная надпись
		Destroy(winText.gameObject, 3.5f);      // тупо удаляем  саму надпись
    }

	// сохранение имени и очков
	public void SaveNameAndScore()
	{
		MainManager.Instance.PlayerName = namePlayer;
		MainManager.Instance.Wave = waveNumber;
        MainManager.Instance.SaveNameAndScore();
	}

	// загрузка имени и очков
	public void LoadNameAndScore()
	{
		MainManager.Instance.LoadNameAndScore();
		namePlayer = MainManager.Instance.PlayerName;
    }

	public void Pause()                             // привязывается к кнопке Пауза (нам паузу надо бы через ЕСК вызывать)
	{
		Time.timeScale = 0;
		pauseCanvas.gameObject.SetActive(true);      // активируется канвас паузы
		pauseButton.gameObject.SetActive(false);     // убирается кнопка Пауза
	}


	// привязка к кнопке Обратно в Игру
	public void UnPause()                           //abstaction 
	{
		Time.timeScale = 1;
		pauseCanvas.gameObject.SetActive(false);
		pauseButton.gameObject.SetActive(true);
	}

	public void BackToMenu()                        // abstaction      
	{
		SaveNameAndScore();                         // сохранить имя и очки
		SceneManager.LoadScene(0);                  // загрузить главное меню
	}


	
	public void GameOver()
    {
		playerAudio.PlayOneShot(gameOFF);           
		Time.timeScale = 0;
		nameAndScore.text = namePlayer + "  score:" + waveNumber; // финальный результат
		pauseButton.gameObject.SetActive(false);
		gameOverCanvas.gameObject.SetActive(true);
		SaveNameAndScore();

	}	

    public void RestartGame()
    {
		gameOver = false;
		gameOverCanvas.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}	
}
