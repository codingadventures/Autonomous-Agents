using UnityEngine;
using System.Collections;

public class StateMachine<T> where T : Agent 
{
	public State<T> CurrentState{ get; private set; }

	public State<T> GlobalState{ get; private set; }

	public State<T> PreviousState{ get; private set; }

	public void Update(Agent agent)
	{
		PreviousState = CurrentState;

		CurrentState = CurrentState.Execute ();
	}

}
