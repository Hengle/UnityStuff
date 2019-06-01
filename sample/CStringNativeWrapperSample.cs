 
using UnityEngine;
using UnityEditor;
using Colin;
using Unity.Jobs;

[ExecuteInEditMode]
public class CTest : MonoBehaviour
{

    [MenuItem("Editor/TestCoroutine")]
    public static unsafe void Done()
    {
        var aJob = new CJob() { m_Str = "Hello world".NewNativeWrapper()};
        try
        {
            aJob.Schedule().Complete();
        }
        finally
        {
            aJob.m_Str.Dispose();
        }
    }

    public struct CJob:IJob
    {
        public VStringNativeWrapper m_Str;

        public void Execute()
        {
            Debug.Log(m_Str.IndexOf("world"));
        }
    }

}
 