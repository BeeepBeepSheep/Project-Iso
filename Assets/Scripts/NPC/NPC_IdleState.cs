using UnityEngine;
using UnityEngine.AI;

public class NPC_IdleState : NPCBaseState
{
    public NPCStateManager stateManager;
    public NavMeshAgent navAgent;
    public Transform thisNPC;
    public override void EnterState(NPCStateManager npcContext)
    {
        stateManager = npcContext;

        stateManager.currantStateStr = "Idle";

        stateManager.currantTargetDestination = null;//set destination to null in state manager

        thisNPC = stateManager.transform;
        navAgent = stateManager.navAgent;

        stateManager.navAgent.SetDestination(thisNPC.transform.position);

        stateManager.StartIdleDuration();

        stateManager.currentIdleDuration = stateManager.fullIdleDuration;
        //set variables
    }

    public override void UpdateState(NPCStateManager npcContext)
    {

    }
}
