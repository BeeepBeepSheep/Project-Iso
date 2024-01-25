using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_AgroState : NPCBaseState
{
    public NPCStateManager stateManager;
    public NavMeshAgent navAgent;

    public Transform targetPlayer;
    public Transform thisNpc;

    public override void EnterState(NPCStateManager npcContext)//works as start
    {
        stateManager = npcContext;

        stateManager.currantStateStr = "Agro";
        stateManager.canChangeState = false;
        navAgent = stateManager.navAgent;

        //set variables
        thisNpc = stateManager.transform;

        targetPlayer = stateManager.aIEnemy.target.transform;// set player as destination
        stateManager.currantTargetDestination = targetPlayer;//set destination on manager

        try// try set destination on navmesh
        {
            navAgent.destination = targetPlayer.position;// set new destination
        }
        catch// if nav agent not on navmesh, an error normally happens
        {
            RaycastHit hit;
            if (Physics.Raycast(thisNpc.position, Vector3.down, out hit, 3/*ray distance*/))
            {
                // set the position of the capsule to be on top of the hit point
                thisNpc.position = hit.point + Vector3.up * thisNpc.localScale.y / 2;
            }
        }
    }

    public override void UpdateState(NPCStateManager npcContext)//works as start
    {
        if (!navAgent.pathPending)
        {
            navAgent.destination = targetPlayer.position;//set destination to player
        }
    }
}
