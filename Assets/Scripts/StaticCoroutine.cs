
namespace Assets.Scripts
{
    using System.Collections;
    using UnityEngine;

    public class StaticCoroutine : MonoBehaviour
    {

        private static StaticCoroutine _instance;

        private static StaticCoroutine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<StaticCoroutine>();

                    if (_instance == null)
                    {
                        _instance = new GameObject("StaticCoroutine").AddComponent<StaticCoroutine>();
                    }
                }
                return _instance;
            }
        }

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        IEnumerator Perform(IEnumerator coroutine)
        {
            yield return StartCoroutine(coroutine);
            Die();
        }

        /// <summary>
        /// Place your lovely static IEnumerator in here and witness magic!
        /// </summary>
        /// <param name="coroutine">Static IEnumerator</param>
        public static void DoCoroutine(IEnumerator coroutine)
        {
            Instance.StartCoroutine(Instance.Perform(coroutine)); //this will launch the coroutine on our instance
        }

        void Die()
        {
            _instance = null;
            Destroy(gameObject);
        }

        void OnApplicationQuit()
        {
            _instance = null;
        }
    }
}