using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    public EnemyStateMachine stateMachine;
    public EnemyStateID initialState;
    public EnemyAgentConfig config;

    public Transform player;
    //chase player state variables
    public NavMeshAgent navMeshAgent;

    //dead state variables
    public EnemyRagdoll enemyRagdoll;
    public UIHealthBar healthBarUI;
    public BoxCollider hitDetectorBoxCollider;
    public CapsuleCollider ragdollCapsuleCollider; // this capsule turns on when entering ragdoll state, used to get pushed by abilities when in ragdoll state



    void Start()
    {
        player = GameObject.Find("Main Camera").transform;

        //get component of required variables
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyRagdoll = GetComponent<EnemyRagdoll>();
        //had to loop like this because GetCHild() doesnt work cuz it doenst have transform, it has RecTransform
        foreach (Transform child in gameObject.transform.GetChild(2).transform)
        {
            healthBarUI = child.GetComponent<UIHealthBar>();
        }
        hitDetectorBoxCollider = GetComponent<BoxCollider>();
        ragdollCapsuleCollider = gameObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<CapsuleCollider>();

        stateMachine = new EnemyStateMachine(this);

        //states
        stateMachine.RegisterState(new EnemyChasePlayerState());
        stateMachine.RegisterState(new EnemyDeathState());
        stateMachine.RegisterState(new EnemyIdleState());
        stateMachine.ChangeState(initialState);

    }

    void Update()
    {
        stateMachine.Update();
    }
}
