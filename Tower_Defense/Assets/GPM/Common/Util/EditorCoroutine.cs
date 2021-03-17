using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gpm.Common.Util
{
    public class EditorCoroutine
    {
        private readonly IEnumerator routine;

        public static EditorCoroutine Start(IEnumerator enumerator)
        {
            EditorCoroutine coroutine = new EditorCoroutine(enumerator);
            coroutine.Start();
            return coroutine;
        }

        public EditorCoroutine(IEnumerator enumerator)
        {
            routine = enumerator;
        }

        private void Start()
        {
#if UNITY_EDITOR
            EditorApplication.update += Update;
#endif
        }

        public void Stop()
        {
#if UNITY_EDITOR
            EditorApplication.update -= Update;
#endif
        }

        private void Update()
        {
            if (routine.MoveNext() == false)
            {
                Stop();
            }
        }
    }
}