using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCStateManager : MonoBehaviour
{
    public string currantStateStr = "tbd";//var for keeping track of currant state in editor

    [Header("Is Enemy")]
    public bool isEnemy = true;

    [Header("State Machine")]
    public bool canChangeState = true;
    public NPCBaseState current_State;
    public NPC_WanderState wander_State = new NPC_WanderState();
    public NPC_IdleState idle_State = new NPC_IdleState();
    public NPC_AgroState agro_State = new NPC_AgroState();

    public AIEnemy aIEnemy;

    public Transform player;

    [Header("Nav agent")]
    public NavMeshAgent navAgent;
    public PointManager pointManager;
    public float navAgentSpeed = 0.8f;
    public float speed;

    public float fullIdleDuration = 3.5f;
    public float currentIdleDuration = 3.5f;

    public Transform npcMesh;

    [Header("Wander")]
    public Transform currantTargetDestination;
    public float minDestinationDistance = 0.5f;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = navAgentSpeed;
    }
    void Start()
    {
        SetState(RandomState());
    }

   /* void Update()
    {
        current_State.UpdateState(this);
    }*/
    public void SetState(NPCBaseState state)//takes in a provided state (script of type "NPCBaseState")
    {
        //set state logic
        if (canChangeState)
        {
            current_State = state;
            current_State.EnterState(this);

            npcMesh.rotation = Quaternion.identity;//reset rotation
        }
    }
    public NPCBaseState RandomState()//returns a random state
    {
        int index = Random.RandomRange(0, 1);

        if (index == 0)
        {
            current_State = wander_State;
        }
        else
        {
            current_State = idle_State;
        }

        return current_State;
    }
    public void StartIdleDuration()
    {
        StartCoroutine(IdleDuration());
    }
    private IEnumerator IdleDuration()
    {
        yield return new WaitForSeconds(currentIdleDuration);
        SetState(RandomState());
    }
    public void TargetPlayer()
    {
        aIEnemy.TargetPlayer();

        if(current_State == idle_State)
        {
            currentIdleDuration = 0f;
        }

        SetState(agro_State);
    }
}
