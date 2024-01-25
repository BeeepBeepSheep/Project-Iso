using UnityEngine;
using UnityEngine.AI;

public class NPC_WanderState : NPCBaseState
{
    public NPCStateManager stateManager;
    public NavMeshAgent navAgent;
    public Transform thisNPC;

    public Transform currantTargetDestination;
    private float minDestinationDistance = 0.5f;

    public override void EnterState(NPCStateManager npcContext)
    {
        stateManager = npcContext;

        stateManager.currantStateStr = "Wander";

        //set variables
        minDestinationDistance = stateManager.minDestinationDistance;
        thisNPC = stateManager.transform;
        navAgent = stateManager.navAgent;

        currantTargetDestination = stateManager.pointManager.RandomPoint();// gets random deestination

        stateManager.currantTargetDestination = currantTargetDestination;//set destination on manager

        try// try set destination on navmesh
        {
            navAgent.destination = currantTargetDestination.position;// set new destination
        }
        catch// if nav agent not on navmesh, an error normally happens
        {
            RaycastHit hit;
            if (Physics.Raycast(thisNPC.position, Vector3.down, out hit, 3/*ray distance*/))
            {
                // set the position of the capsule to be on top of the hit point
                thisNPC.position = hit.point + Vector3.up * thisNPC.localScale.y / 2;
            }
        }
    }
    public override void UpdateState(NPCStateManager npcContext)
    {
        if (!navAgent.pathPending)
        {
            // calculate the distance between the two objects
            float distance = Vector3.Distance(thisNPC.position, currantTargetDestination.position);

            // check if the distance is below the threshold
            if (distance <= minDestinationDistance)// if yes
            {
                stateManager.SetState(stateManager.idle_State);// set state to idle
            }

            if (navAgent.speed < 0.5f)// if walking to destination there is nav error
            {
                stateManager.SetState(stateManager.idle_State);// set state to idle
            }
        }
    }
}
