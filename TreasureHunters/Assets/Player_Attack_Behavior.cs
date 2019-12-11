using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Behavior : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       // animator.gameObject.transform.Find("Sword").GetComponent<BoxCollider2D>().; //active le box collider de sword
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.gameObject.transform.Find("Sword").GetComponent<BoxCollider2D>().; //desactive le box collider de sword
    }
}
