using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    [SerializeField] protected private GameObject canvas;
    [SerializeField] protected private GameObject canvasOptions;
    [SerializeField] protected private GameObject canvasRecord;
    [SerializeField] protected private TMP_InputField playerName;
    [SerializeField] protected private Slider sliderMusic;
    [SerializeField] protected private Slider sliderEffects;
    [SerializeField] protected private TextMeshProUGUI topPlayerText;

    private void Start()     
    {
        LoadNameAndScore(); // abstract
    }

    public void SaveName()
    {
        MainManager.Instance.PlayerName = playerName.text;
        MainManager.Instance.VolumeMusic = sliderMusic.value;
        MainManager.Instance.VolumeEffects = sliderEffects.value;
        MainManager.Instance.SaveNameAndScore();
    }
    public void LoadNameAndScore()
    {
        MainManager.Instance.LoadNameAndScore();
        sliderMusic.value = MainManager.Instance.VolumeMusic;
        sliderEffects.value = MainManager.Instance.VolumeEffects;
        playerName.text = MainManager.Instance.PlayerName;
        topPlayerText.text = MainManager.Instance.PlayerName +": " + MainManager.Instance.Wave + " score";
    }

    public void GoToRecord()      // переход в окно Рекордов
    {
        canvas.gameObject.SetActive(false);
        canvasRecord.gameObject.SetActive(true);
    }

    public void RecordBackToMenu()  // выход из окна Рекордов
    {
        canvasRecord.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
    }

    public void GoToOptions()     // переход в окно настроеек
    {
        canvas.gameObject.SetActive(false);
        canvasOptions.gameObject.SetActive(true);
    }
    public void BackToMenu()     //  выход из окна настроеек
    {
        SaveName();
        canvasOptions.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
    }
    public void StartNew()  
    {
        SceneManager.LoadScene(1);  // загрузка сцены 1

    }

    public void Exit()
    {
        SaveName();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
