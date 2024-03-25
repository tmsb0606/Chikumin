using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update
    public enum EnemyAiState
    {
        Wait,
        RandomMove,
        Chase,
        Attack,

    }
    NavMeshAgent agent;
    public int hp = 100;
    public EnemyAiState aiState = EnemyAiState.RandomMove;

    public List<GameObject> targetObjects = new List<GameObject>();
    public GameObject attackTarget;

    private float time =0f;
    public float limitTime = 5f;

    private GameDirector gameDirector;


    public bool isAttack = false;

    private LevelUpController _levelUpController;
    Animator animator;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        animator = this.GetComponent<Animator>();
        _levelUpController = GameObject.Find("LevelUpDirector").GetComponent<LevelUpController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp < 0)
        {
            Death();
        }
        if(targetObjects.Count > 0)
        {
            aiState = EnemyAiState.Chase;
        }
        else
        {
            aiState = EnemyAiState.RandomMove;
        }
        switch (aiState)
        {
            case EnemyAiState.RandomMove:
                RandomMove();
                break;
            case EnemyAiState.Chase:
                Chase();
                break;
        }
        animator.SetFloat("Speed",agent.velocity.sqrMagnitude);
        animator.SetBool("Attack", isAttack);
    }
    public void RandomMove()
    {
        time += Time.deltaTime;
        if(time >= limitTime)
        {
            time = 0;
            agent.SetDestination(new Vector3(Random.RandomRange(-100,100),0, Random.RandomRange(-100, 100)));

        }
    }
    public void Chase()
    {
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            agent.SetDestination(targetObjects[0].transform.position);
        }
        
    }

    public void Damage(int value)
    {
        // ここに具体的なダメージ処理
        hp -= value;
    }

    public void Attack(GameObject gameObject)
    {

    }
    public void AttackDamage()
    {
        if (attackTarget.GetComponent<ChikuminBase>())
        {
            attackTarget.GetComponent<IDamageable>().Damage(1);
            isAttack = false;
        }
    }
    public void Death()
    {
        // ここに具体的な死亡処理
        this.gameObject.SetActive(false);
        //gameDirector.AllCharacterLevelUP();
        _levelUpController.AllCharacterLevelUP();


    }

}
