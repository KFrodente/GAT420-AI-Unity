using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AiStateAgent : AIAgent
{
    public Animator animator;
    public AIPerception enemyPerception;
    public AIPerception friendlyPerception;

    //parameters
    public ValueRef<float> health = new ValueRef<float>(); // -> memory
    public ValueRef<float> timer = new ValueRef<float>(); // -> memory
    public ValueRef<float> destinationDistance = new ValueRef<float>();

    public ValueRef<bool> enemySeen = new ValueRef<bool>();
    public ValueRef<float> enemyDistance = new ValueRef<float>();
    public ValueRef<float> enemyHealth = new ValueRef<float>();
    public AiStateAgent enemy {  get; private set; }


    public ValueRef<int> friendliesSeen = new ValueRef<int>();

    public AIStateMachine stateMachine = new AIStateMachine();

    private void Start()
    {
        stateMachine.AddState(nameof(AIIdleState), new AIIdleState(this));
        stateMachine.AddState(nameof(AIDeathState), new AIDeathState(this));
        stateMachine.AddState(nameof(AIPatrolState), new AIPatrolState(this));
        stateMachine.AddState(nameof(AIAttackState), new AIAttackState(this));
        stateMachine.AddState(nameof(AIChaseState), new AIChaseState(this));
        stateMachine.AddState(nameof(AIGroupDance), new AIGroupDance(this));
        stateMachine.AddState(nameof(AISoloDance), new AISoloDance(this));
        stateMachine.AddState(nameof(AIHitState), new AIHitState(this));

        stateMachine.SetState(nameof(AIIdleState));
    }

    private void Update()
    {
        //update parameters
        timer.value -= Time.deltaTime;
        destinationDistance.value = Vector3.Distance(transform.position, movement.Destination);

        friendliesSeen.value = friendlyPerception.GetGameObjects().Length;


        var enemies = enemyPerception.GetGameObjects();
        enemySeen.value = (enemies.Length > 0); 

        if (enemySeen)
        {
            enemy = enemies[0].TryGetComponent(out AiStateAgent stateAgent) ? stateAgent : null;
            enemyDistance.value = Vector3.Distance(transform.position, enemy.transform.position);
            enemyHealth.value = enemy.health;
        }

        //from any state (health -> death)
        if (health.value <= 0) stateMachine.SetState(nameof(AIDeathState));

        animator?.SetFloat("Speed", movement.Velocity.magnitude);

        //check for transitions
        foreach (var transition in stateMachine.currentState.transitions)
        {
            if (transition.ToTransition())
            {
                stateMachine.SetState(transition.nextState);
                break;
            }
        }

        stateMachine.Update();
    }

    private void OnGUI()
    {
        // draw label of current state above agent
        GUI.backgroundColor = Color.black;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        Rect rect = new Rect(0, 0, 100, 20);
        // get point above agent
        Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
        rect.x = point.x - (rect.width / 2);
        rect.y = Screen.height - point.y - rect.height - 20;
        // draw label with current state name
        GUI.Label(rect, stateMachine.currentState.name);
    }

    public void Attack()
    {
        Debug.Log("Attack");
        // check for collision with surroundings
        var colliders = Physics.OverlapSphere(transform.position, 1);
        foreach (var collider in colliders)
        {
            // don't hit self or objects with the same tag
            if (collider.gameObject == gameObject || collider.gameObject.CompareTag(gameObject.tag)) continue;

            // check if collider object is a state agent, reduce health
            if (collider.gameObject.TryGetComponent<AiStateAgent>(out var stateAgent))
            {
                stateAgent.ApplyDamage(Random.Range(20, 50));
            }
        }
    }

    public void ApplyDamage(float damage)
    {
        health.value -= damage;
        if (health > 0) stateMachine.SetState(nameof(AIHitState));
    }
}
