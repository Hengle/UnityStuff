using System;
using System.Reflection;
using Unity.Collections.LowLevel.Unsafe;

namespace Colin
{
    /// <summary>
    /// a quote of a managed string,when you finish using,you should realease it by call Dispose method.if not,that may cause memory leak.
    /// </summary>
    public unsafe struct CNativeString : IDisposable
    {
        public static readonly int m_CharPtrOffset = UnsafeUtility.GetFieldOffset(typeof(string).GetField("m_firstChar", BindingFlags.Instance | BindingFlags.NonPublic));

        public CNativeString(string aStr)
        {
            m_Ptr = (char*)(((byte*)UnsafeUtility.PinGCObjectAndGetAddress(aStr, out m_Handle)) + m_CharPtrOffset);
            m_Count = aStr.Length;
        }
        [NativeDisableUnsafePtrRestriction]
        private char* m_Ptr;
        private int m_Count;
        private ulong m_Handle;

        public char this[int aIndex]
        {
            get
            {
                if (aIndex > -1 && aIndex < m_Count)
                    return m_Ptr[aIndex];
                else
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int Count
        { get { return m_Count; } }


        public void Dispose()
        {
            if (m_Handle == 0)
                return;
            UnsafeUtility.ReleaseGCObject(m_Handle);
            m_Count = 0;
            m_Handle = 0;
        }
    }
}
