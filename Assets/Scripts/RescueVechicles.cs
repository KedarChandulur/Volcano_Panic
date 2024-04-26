using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RescueVechicles : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        //this.agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(234.26f, 0.0f, 142.58f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
