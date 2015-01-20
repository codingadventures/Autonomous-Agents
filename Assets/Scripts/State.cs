using UnityEngine;
using System.Collections;
using AssemblyCSharp;
using System;

public abstract class State<T>  where T : Agent
{
	public abstract State<T> Execute();

	public abstract void Update(Agent agent);

	protected event EventHandler<AgentEventArgs> Enter;
	protected event EventHandler<AgentEventArgs> Exit;

	protected  virtual void OnEnter(AgentEventArgs e)
	{
		var handler = Enter;
		if (handler != null)
		{
			handler(this, e);
		}
	}

	protected  virtual void OnExit(AgentEventArgs e)
	{
		var handler = Exit;
		if (handler != null)
		{
			handler(this, e);
		}
	}
}