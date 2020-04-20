using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events;
using Gameplay;
using UnityEngine;

public class AIInputController : MonoBehaviour
{
    public float VisionDistance = 10f;
    public bool CanContinueChasing;
    public float IntervalBetweenAttacks = 3f;
    public float AttackDistance = 2f;
    public float PatrolZoneRadius = 10f;
    public string EnemyTag = "Player";
    private Vector3 _startPoint;
    private List<AITask> _taskList;

    void Start()
    {
        var taskContext = new TaskContext();

        _startPoint = transform.position;
        taskContext.GameObject = gameObject;
        taskContext.StartPoint = _startPoint;
        taskContext.IntervalBetweenAttacks = IntervalBetweenAttacks;
        taskContext.AttackDistance = AttackDistance;
        taskContext.PatrolZoneRadius = PatrolZoneRadius;
        taskContext.VisionDistance = VisionDistance;
        taskContext.CanContinueChasing = CanContinueChasing;
        taskContext.EnemyTag = EnemyTag;

        _taskList = new List<AITask>()
        {
            new AttackAITask(taskContext),
            new GotoEnemyAITask(taskContext),
            new GotoWayPointAITask(taskContext),
        };
    }

    private AITask GetActualTask()
    {
        return _taskList.FirstOrDefault(task => task.Active || task.CanStart());
    }

    // Update is called once per frame
    void Update()
    {
        var actualTask = GetActualTask();

        foreach (var task in _taskList)
        {
            if (!task.Equals(actualTask))
            {
                task.InvokeStop();
            }
        }

        actualTask.InvokeStart();
        actualTask.InvokeProcess();
    }
}

public class AITask
{
    public bool Active;
    protected TaskContext _context;

    public AITask(TaskContext taskContext)
    {
        _context = taskContext;
    }

    public virtual void Start()
    {
    }

    public virtual void Stop()
    {
    }

    public virtual void Process()
    {
    }

    public void InvokeStart()
    {
        if (!Active)
        {
            Active = true;
            Start();
        }
    }

    public void InvokeStop()
    {
        if (Active)
        {
            Active = false;
            Stop();
        }
    }

    public void InvokeProcess()
    {
        if (Active)
        {
            Process();
        }
    }

    public virtual bool CanStart()
    {
        return false;
    }
}

public class AttackAITask : AITask
{
    private float lastAttackTime;

    public AttackAITask(TaskContext taskContext) : base(taskContext)
    {
    }

    public override void Start()
    {
        lastAttackTime = 0;
    }

    public override void Stop()
    {
    }

    public override void Process()
    {
        if (!CanAttack())
        {
            InvokeStop();
            return;
        }
        
        if (lastAttackTime <= 0)
        {
            Attack();
        }

        lastAttackTime -= Time.deltaTime;
    }

    private void Attack()
    {
        _context.GameObject.GetComponent<EventSystem>().Dispatch(ActionEvent.ATTACK);
        lastAttackTime = _context.IntervalBetweenAttacks;
    }

    private GameObject GetEnemy(float distance)
    {
        var dir = new Vector2(_context.GameObject.GetComponent<MovementController>().GetDirection(), 0);
        var hit = Physics2D.Raycast((_context.GameObject.transform.position + new Vector3(dir.x * 1.5f, 0, 0)), dir,
            distance);
        Debug.DrawRay((_context.GameObject.transform.position + new Vector3(dir.x * 1.5f, 0, 0)), dir * distance,
            Color.green, 0.2f);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out HPController hpController) && hit.collider.gameObject.CompareTag(_context.EnemyTag))
            {
                Debug.DrawRay(_context.GameObject.transform.position, dir * distance, Color.red, 0.3f);
                return hit.collider.gameObject;
            }
        }

        return null;
    }

    private bool CanAttack()
    {
        return GetEnemy(_context.AttackDistance) != null;
    }

    public override bool CanStart()
    {
        return CanAttack();
    }
}

public class GotoEnemyAITask : AITask
{
    private GameObject _enemy;

    public GotoEnemyAITask(TaskContext taskContext) : base(taskContext)
    {
    }

    public override void Start()
    {
        _enemy = GetEnemy(_context.VisionDistance);

        _context.GameObject.GetComponent<EventSystem>().Dispatch(
            (_enemy.transform.position.x < _context.GameObject.transform.position.x)
                ? MovementEvent.MOVING_LEFT_STARTED
                : MovementEvent.MOVING_RIGHT_STARTED);
    }

    public override void Stop()
    {
        _context.GameObject.GetComponent<EventSystem>().Dispatch(MovementEvent.MOVING_STOPPED);
    }

    public override void Process()
    {
        if (!IsEnemyOnMyTerritoty(_enemy) && !_context.CanContinueChasing ||
            Vector3.Distance(_enemy.transform.position, _context.GameObject.transform.position) < 2.5f)
        {
            InvokeStop();
        }
    }

    public override bool CanStart()
    {
        var enemy = GetEnemy(_context.VisionDistance);
        return enemy != null && _context.CanContinueChasing || enemy != null && IsEnemyOnMyTerritoty(enemy) && !_context.CanContinueChasing;
    }

    private GameObject GetEnemy(float distance)
    {
        var dir = new Vector2(_context.GameObject.GetComponent<MovementController>().GetDirection(), 0);
        var hit = Physics2D.Raycast((_context.GameObject.transform.position + new Vector3(dir.x * 1.5f, 0, 0)), dir,
            distance);
        Debug.DrawRay((_context.GameObject.transform.position + new Vector3(dir.x * 1.5f, 0, 0)), dir * distance,
            Color.green, 0.2f);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out HPController hpController) && hit.collider.gameObject.CompareTag(_context.EnemyTag))
            {
                Debug.DrawRay(_context.GameObject.transform.position, dir * distance, Color.red, 0.3f);
                return hit.collider.gameObject;
            }
        }

        return null;
    }

    private bool IsEnemyOnMyTerritoty(GameObject enemy)
    {
        return Vector3.Distance(enemy.transform.position, _context.StartPoint) < _context.PatrolZoneRadius;
    }
}

public class GotoWayPointAITask : AITask
{
    private Vector3 _wayPoint;

    public GotoWayPointAITask(TaskContext taskContext) : base(taskContext)
    {
    }

    public override void Start()
    {
        _wayPoint = _context.StartPoint;
        _wayPoint.x += -_context.PatrolZoneRadius;

        if (Vector3.Distance(_context.GameObject.transform.position, _wayPoint) < _context.PatrolZoneRadius)
        {
            _wayPoint.x += _context.PatrolZoneRadius * 2;
        }

        _context.GameObject.GetComponent<EventSystem>().Dispatch(
            (_wayPoint.x < _context.StartPoint.x)
                ? MovementEvent.MOVING_LEFT_STARTED
                : MovementEvent.MOVING_RIGHT_STARTED);
    }

    public override void Stop()
    {
        _context.GameObject.GetComponent<EventSystem>().Dispatch(MovementEvent.MOVING_STOPPED);
    }

    public override void Process()
    {
        if (Vector3.Distance(_context.GameObject.transform.position, _wayPoint) < 5f)
        {
            InvokeStop();
        }
    }

    public override bool CanStart()
    {
        return true;
    }
}

public class TaskContext
{
    public GameObject GameObject;
    public Vector3 StartPoint;
    public float AttackDistance;
    public float IntervalBetweenAttacks;
    public float PatrolZoneRadius;
    public float VisionDistance;
    public bool CanContinueChasing;
    public string EnemyTag;

    public TaskContext()
    {
    }
}