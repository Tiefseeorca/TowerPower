using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public EnemyPathNode Target;
    private Vector3 _directionToTarget;
    public float Speed;
    public float TurnSpeed = 720;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if (Target) {
            transform.LookAt(Target.transform.position);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!Target) return;
        transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
        _rotate();
        _reachedTarget();
        if (Vector3.Distance(transform.position, Target.transform.position) <= 0.1f) {
            if (Target.IsEnd) {
                _onDespawn();
                return;
            }
            Target = Target.Next;
            //transform.LookAt(Target.transform.position);
        }
    }

    private void _rotate() {
        _directionToTarget = Target.transform.position - transform.position;
        _directionToTarget.Normalize();
        float rightDot = Vector3.Dot(transform.right, _directionToTarget);
        if (Mathf.Abs(rightDot) < 0.1f) {
            transform.LookAt(Target.transform.position);
        }
        else {
            float sign = rightDot > 1 ? 1 : -1;
            transform.Rotate(0, sign * TurnSpeed * Time.deltaTime, 0);
        }
    }

    private bool _reachedTarget() {
        float dot = Vector3.Dot(Vector3.forward, _directionToTarget);
        Debug.Log(dot);
        return dot < 0;
    }

    private void _onDespawn() {
        Destroy(gameObject);
    }
}
