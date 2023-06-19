using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FootstepManager : MonoBehaviour
{
    public List<AudioClip> grassSteps = new List<AudioClip>();  // лист со звуками травы
    public List<AudioClip> waterSteps = new List<AudioClip>();  // воды
    public List<AudioClip> caveSteps = new List<AudioClip>();   // пещеры
	public List<AudioClip> woodSteps = new List<AudioClip>();   // дерево
    public List<AudioClip> metalSteps = new List<AudioClip>();   // металл

    private enum Surface { grass, water, cave, wood, metal};                 // возможные поверхности
    private Surface surface;

    private List<AudioClip> currentList;                        // текущий лист

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();            
    }

    // функция озвучки
    public void PlayStep ()
    {
        AudioClip clip = currentList[Random.Range(0, currentList.Count)]; // выбор рандома из листа
        source.PlayOneShot(clip);
    }

    // выбор листа, в зависимости от поверхности
    private void SelectStepList ()
    {
        switch (surface)
        {
            case Surface.grass:
                currentList = grassSteps;
                break;
            case Surface.water:
                currentList = waterSteps;
                break;
            case Surface.cave:
                currentList = caveSteps;
                break;
			case Surface.wood:
			    currentList = woodSteps;
				break;
            case Surface.metal:
                currentList = metalSteps;
                break;
        }
    }

    // проверка , по какой поверхности идем
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Grass")
        {
            surface = Surface.grass;
        }

        if (hit.transform.tag == "Water")
        {
            surface = Surface.water;
        }

        if (hit.transform.tag == "Cave")
        {
            surface = Surface.cave;
        }
		
		if (hit.transform.tag == "Wood")
		{
			surface = Surface.wood;
		}

        if (hit.transform.tag == "Metal")
        {
            surface = Surface.metal;
        }

        SelectStepList();  // передача в функцию выбоа листа
        
    }

}
