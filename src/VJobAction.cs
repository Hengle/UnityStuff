/*
  This file belong to UnityStuff project,link address [https://github.com/ColinGao225/UnityStuff]
  Written by Colin Gao at 7th/6/2019  
  These have a MIT Lisence.That means you can use and  modify the code at all,in the condition that you specify the refferences in your project. 
 */

/// <summary>
/// 在Job System 内部可以使用该回调结构访问到引用类与对象,但是不可使用将失去对象锁.使用完毕请及时释放资源,否则会造成泄露.
/// JobCallBack can be used to access managed classes and object.Release memory after useing, otherwise it will cause memory leaks.
/// </summary>
using Unity.Collections.LowLevel.Unsafe;
using System;
public unsafe struct VJobAction :IDisposable
{
    [NativeDisableUnsafePtrRestriction]
    private void* m_Ptr;
    private ulong m_Handle;

    public VJobAction(Action aCallback)
    {
        var aArr = new Action[] { aCallback };
        m_Ptr = UnsafeUtility.PinGCArrayAndGetDataAddress(aArr, out m_Handle);
    }

    public void Invoke()
    {
        Callback?.Invoke();
    }

    /// <summary>
    /// 回调函数.
    /// </summary>
    public Action Callback
    {
        get
        {
            return UnsafeUtility.ReadArrayElement<Action>(m_Ptr, 0);
        }
        set
        {
            Dispose();

            var aArr = new Action[] { value };
            m_Ptr = UnsafeUtility.PinGCArrayAndGetDataAddress(aArr, out m_Handle);
        }
    }

    public void Dispose()
    {
        if (m_Handle != 0)
        {
            UnsafeUtility.ReleaseGCObject(m_Handle);
            m_Ptr = (byte*)0;
            m_Handle = 0;
        }
    }

    public static implicit operator Action (VJobAction aCallback)
    {
        return aCallback.Callback;
    }

    public static explicit operator VJobAction( Action aCallback)
    {
        return new VJobAction(aCallback);
    }
}
