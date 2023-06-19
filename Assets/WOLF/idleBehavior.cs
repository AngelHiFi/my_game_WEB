using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleBehavior : StateMachineBehaviour
{
	float timer; // счетчик времени
    Transform player;
    float chaseRange = 10;  // АГР, расстояние на котором начинается погоня за игроком

    // OnStateEnter вызывается, когда начинается переход, и конечный автомат начинает оценивать это состояние.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate вызывается в каждом кадре обновления между обратными вызовами OnStateEnter и OnStateExit
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;                   // увеличиваем на 1 секунду
		if(timer > 5)
			animator.SetBool("isPatruling", true); // переход на состояние патруль  (через 5 секунд после запуска этого состояния)

        float distance = Vector3.Distance(animator.transform.position, player.position);
        if (distance < chaseRange)
            animator.SetBool("isChaiz", true);    // переход на состояние погони ( если расстояние в зоне агра)
    }

    // OnStateExit вызывается, когда переход заканчивается, и конечный автомат завершает оценку этого состояния
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
