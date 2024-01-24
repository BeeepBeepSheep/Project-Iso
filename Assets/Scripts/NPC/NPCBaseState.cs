using UnityEngine;
public abstract class NPCBaseState
{//script acts as a base for npc state machine
    public abstract void EnterState(NPCStateManager npcContext);
    public abstract void UpdateState(NPCStateManager npcContext);
}
