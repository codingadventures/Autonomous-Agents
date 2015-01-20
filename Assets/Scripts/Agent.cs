
using System.Collections;
using UnityEngine;

public abstract class Agent : MonoBehaviour {

	public StateMachine<Agent> StateMachine;
 	

	public abstract void Update();

}
