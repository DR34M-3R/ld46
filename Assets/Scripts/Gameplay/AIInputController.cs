using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Events;
using Gameplay;
using UnityEngine;

public class AIInputController : MonoBehaviour
{
    private Vector3 _startPoint;
    private List<AITask> _taskList;

    void Start()
    {
        var taskContext = new TaskContext();

        taskContext.GameObject = gameObject;
        _startPoint = transform.position;
        taskContext.StartPoint = _startPoint;

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
        lastAttackTime = 3f;
    }

    public override void Stop()
    {
    }

    public override void Process()
    {
        lastAttackTime += Time.deltaTime;

        if (lastAttackTime > 3f)
        {
            Attack();
        }
    }
    
    private void Attack()
    {
        _context.GameObject.GetComponent<EventSystem>().Dispatch(ActionEvent.ATTACK);
        lastAttackTime = 0;
    }
    
    private GameObject GetEnemy(float distance)
    {
        var dir = new Vector2( _context.GameObject.GetComponent<MovementController>().GetDirection(), 0);
        var hit = Physics2D.Raycast((_context.GameObject.transform.position + new Vector3(dir.x * 1.5f,0,0)), dir, distance);
        Debug.DrawRay((_context.GameObject.transform.position + new Vector3(dir.x * 1.5f,0,0)), dir * distance, Color.green, 0.2f);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out HPController hpController))
            {
                Debug.DrawRay(_context.GameObject.transform.position, dir * distance, Color.red, 0.3f);
                return hit.collider.gameObject;
            }
        }

        return null;
    }

    public override bool CanStart()
    {
        return GetEnemy(2f) != null;
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
        _enemy = GetEnemy(10);
        
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
        if (!IsEnemyOnMyTerritoty(_enemy) || Vector3.Distance(_enemy.transform.position, _context.GameObject.transform.position) < 2.5f)
        {
            InvokeStop();
        }
    }

    public override bool CanStart()
    {
        var enemy = GetEnemy(10);
        return enemy != null && IsEnemyOnMyTerritoty(enemy);
    }
    
    private GameObject GetEnemy(float distance)
    {
        var dir = new Vector2( _context.GameObject.GetComponent<MovementController>().GetDirection(), 0);
        var hit = Physics2D.Raycast((_context.GameObject.transform.position + new Vector3(dir.x * 1.5f,0,0)), dir, distance);
        Debug.DrawRay((_context.GameObject.transform.position + new Vector3(dir.x * 1.5f,0,0)), dir * distance, Color.green, 0.2f);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out HPController hpController))
            {
                Debug.DrawRay(_context.GameObject.transform.position, dir * distance, Color.red, 0.3f);
                return hit.collider.gameObject;
            }
        }

        return null;
    }

    private bool IsEnemyOnMyTerritoty(GameObject enemy)
    {
        return Vector3.Distance(enemy.transform.position, _context.StartPoint) < 10f;
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
        _wayPoint.x +=  -10;
        
        if (Vector3.Distance(_context.GameObject.transform.position, _wayPoint) < 10f)
        {
            _wayPoint.x += 20;
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

    public TaskContext()
    {
    }
}