namespace Assets.Scripts.Agents
{
    using States;
    using UnityEngine;
    using System.Collections;

    public class Elsa : MonoBehaviour, IAgent<Elsa>
    {

        public float UpdateStep;

        public IStateMachine<Elsa> StateMachine
        {
            get;
            set;
        }

        public int NextValidId
        {
            get;
            set;
        }

        
        public bool Cooking
        {
            get;
            set;
        }

        #region [ Unity Monobehavior Events ]


        void Awake()
        {
            NextValidId = Random.Range(0, 100);
            StateMachine = new StateMachine<Elsa>(this);
        }

        // Use this for initialization
        void Start()
        {
            StateMachine.ChangeState(DoHousework<Elsa>.Instance);
            StateMachine.GlobalState = MinerGlobalState<Elsa>.Instance;
            StartCoroutine(PerformUpdate());
        }


        IEnumerator PerformUpdate()
        {
            while (true)
            {
                StateMachine.Update();

                yield return new WaitForSeconds(UpdateStep);
            }
        }

        #endregion

        public void ChangeState<T>(IState<T> newState)
        {
            StateMachine.ChangeState((IState<Elsa>)newState);
        }

    }
}
