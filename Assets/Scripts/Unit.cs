using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] public UnitState State = UnitState.None;

    public enum UnitState
    {
        None,
        Move
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>() != null ? GetComponent<NavMeshAgent>() : gameObject.AddComponent<NavMeshAgent>();
    }
}
