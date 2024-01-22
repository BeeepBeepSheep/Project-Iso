using UnityEngine;
using UnityEngine.AI;

public class NPC_IdleState : NPCBaseState
{
    public NPCStateManager stateManager;
    public NavMeshAgent navAgent;
    public Transform thisNPC;
    public override void EnterState(NPCStateManager npcContext)
    {
        stateManager.currantStateStr = "Idle";

        stateManager.currantTargetDestination = null;//set destination to null in state manager

        stateManager.navAgent.SetDestination(stateManager.transform.position);

        stateManager.StartIdleDuration();

        //set variables
        thisNPC = stateManager.transform;
        navAgent = stateManager.navAgent;
    }

    public override void UpdateState(NPCStateManager npcContext)
    {

    }
}
