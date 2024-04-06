using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

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

    private int maxHP = 100;
    public int hp = 100;
    public EnemyAiState aiState = EnemyAiState.RandomMove;

    public List<GameObject> targetObjects = new List<GameObject>();
    public GameObject attackTarget;

    private float time = 0f;
    public float limitTime = 5f;

    private GameDirector gameDirector;


    public bool isAttack = false;

    private LevelUpController _levelUpController;
    Animator animator;

    private GameObject _canvas;
    private Image _hpImage;
    private bool isDamage = false;
    private bool isDeath = false;
    void Start()
    {
        _canvas = Instantiate((GameObject)Resources.Load("HPBar"));
        _canvas.transform.parent = this.transform;
        _canvas.transform.localPosition = new Vector3(0, 2.5f, 0);
        _hpImage = _canvas.GetComponentInChildren<Image>();
        agent = GetComponent<NavMeshAgent>();
        //gameDirector = GameObject.Find("GameDirector").GetComponent<GameDirector>();
        animator = this.GetComponent<Animator>();
        _levelUpController = GameObject.Find("LevelUpDirector").GetComponent<LevelUpController>();
    }

    // Update is called once per frame
    void Update()
    {
        //_hpImage.fillAmount = (float)hp / maxHP;


        if (hp < 0 && !isDeath)
        {
            Death();
        }
        if (targetObjects.Count > 0)
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
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
        animator.SetBool("Attack", isAttack);
    }
    public void RandomMove()
    {
        time += Time.deltaTime;
        if (time >= limitTime)
        {
            time = 0;
            agent.SetDestination(new Vector3(Random.RandomRange(-100, 100), 0, Random.RandomRange(-100, 100)));

        }
    }
    public void Chase()
    {
        if (agent.pathStatus != NavMeshPathStatus.PathInvalid)
        {
            
            agent.SetDestination(targetObjects[0].transform.position);
        }

    }

    public async void Damage(int value)
    {
        // ここに具体的なダメージ処理
        hp -= value;
        isDamage = true;
        await HpGaugeAnimation();
    }

    public async UniTask<bool> HpGaugeAnimation()
    {
        if(_canvas.GetComponent<CanvasGroup>().alpha < 1)
        {
            _canvas.GetComponent<CanvasGroup>().alpha = 1;
        }
        
        while (true)
        {
            _hpImage.fillAmount -= 0.05f;
            await UniTask.Delay(100);
            if(_hpImage.fillAmount <= (float)hp / maxHP)
            {
                break;
            }
        }
        isDamage = false;
        await UniTask.Delay(2500);
        if (!isDamage)
        {
            _canvas.GetComponent<CanvasGroup>().alpha = 0;
        }
        
        return true;
        
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
    public async void Death()
    {
        // ここに具体的な死亡処理
        isDeath = true;
        animator.SetBool("Death", true);
        await UniTask.Delay(1000);
        this.gameObject.SetActive(false);
        //gameDirector.AllCharacterLevelUP();
        _levelUpController.AllCharacterLevelUP();


    }

}
