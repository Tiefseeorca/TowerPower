using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour {
    public EnemyPathNode Target;
    private Vector3 _directionToTarget;
    public float TurnSpeed = 3 * 360;
    public float DistanceToGoal { get; private set; }
    [SerializeField] private int _hp = 0;
    public EnemyStats Stats;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (Target) {
            transform.LookAt(Target.transform.position);
            DistanceToGoal = Target.GetDistanceToGoal();
            _hp = Stats.MaxHp;
        }
        else {
            _onDespawn();
        }
    }

    // Update is called once per frame
    void Update() {
        if (!Target) return;
        float travelDist = Stats.Speed * Time.deltaTime;
        DistanceToGoal -= travelDist;
        //transform.Translate(Vector3.forward * travelDist);
        Vector3 tmp = Vector3.Normalize(Target.transform.position - transform.position);
        Vector3 newPos = transform.position + (travelDist * tmp);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.MovePosition(newPos);
        if (_reachedTarget()) {
            if (Target.IsEnd) {
                _onDespawn();
                return;
            }
            Target = Target.Next;
            //transform.LookAt(Target.transform.position);
        }
        _rotate();
    }

    private void _rotate() {
        _directionToTarget = Target.transform.position - transform.position;
        _directionToTarget.Normalize();
        float rightDot = Vector3.Dot(transform.right, _directionToTarget);
        if (Mathf.Abs(rightDot) < 0.05f) {
            transform.LookAt(Target.transform.position);
        }
        else {
            float sign = rightDot > 0 ? 1 : -1;
            transform.Rotate(0, sign * TurnSpeed * Time.deltaTime, 0);
        }
    }

    private bool _reachedTarget() {
        float dot = Vector3.Dot(Vector3.forward, _directionToTarget);
        float dist = Vector3.Distance(transform.position, Target.transform.position);
        return (dot < 0) && (dist <= 0.5f) || dist == 0;
    }

    public void GetHit(int damage) {
        _hp -= damage;
        if(_hp <= 0) _onDefeat();
    }

    private void _onDefeat() {
        Bank.AddMoney.Invoke(Stats.Reward);
        _onDespawn();
    }

    private void _onDespawn() {
        Destroy(gameObject);
    }

    public bool isPreferable(Enemy prev, Tower.TargetingOption targeting) {
        if (!prev) {
            return true;
        }
        switch (targeting) {
            case Tower.TargetingOption.First:
                return this.DistanceToGoal < prev.DistanceToGoal;
            case Tower.TargetingOption.Last:
                return this.DistanceToGoal > prev.DistanceToGoal;
            case Tower.TargetingOption.Strongest:
                return this.Stats.Strength > prev.Stats.Strength;
            case Tower.TargetingOption.Weakest:
                return this.Stats.Strength < prev.Stats.Strength;
            case Tower.TargetingOption.LowestHp:
                return this._hp < prev._hp;
            default:
                return false;
        }
    }
}
