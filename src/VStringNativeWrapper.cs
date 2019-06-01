using System;
using System.Reflection;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Colin
{
    /// <summary>
    /// a quote of a managed string,when you finish using,you should realease it by call Dispose method.if not,that may cause memory leak.
    /// </summary>
    public unsafe struct VStringNativeWrapper : IDisposable
    {
        public static readonly int m_CharPtrOffset = UnsafeUtility.GetFieldOffset(typeof(string).GetField("m_firstChar", BindingFlags.Instance | BindingFlags.NonPublic));

        public VStringNativeWrapper(string aStr)
        {
            m_Ptr = (char*)(((byte*)UnsafeUtility.PinGCObjectAndGetAddress(aStr, out m_Handle)) + m_CharPtrOffset);
            m_Length = aStr.Length;
        }
        [NativeDisableUnsafePtrRestriction]
        private char* m_Ptr;
        private int m_Length;
        private ulong m_Handle;

        public char this[int aIndex]
        {
            get
            {
                if (aIndex > -1 && aIndex < m_Length)
                    return m_Ptr[aIndex];
                else
                    throw new ArgumentOutOfRangeException();
            }
        }

        public int Length
        { get { return m_Length; } }

        public int IndexOf(char aValue)
        {
            for (int i = 0; i < m_Length; ++i)
            {
                if (m_Ptr[i] == aValue)
                    return i;
            }
            return -1;
        }
        public int IndexOf(int aIndex,int aCount, char aValue)
        {
            if (aIndex < 0 || aCount < 0 || aIndex + aCount > m_Length)
                throw new ArgumentOutOfRangeException();

            aCount += aIndex;
            for(int i=aIndex;i<aCount;++i)
            {
                if (m_Ptr[i] == aValue)
                    return i;
            }
            return -1;
        }
        public int IndexOf(string aKey)
        {
            return IndexOf(0, m_Length,aKey);
        }
        public int IndexOf(int aIndex, int aCount,string aKey)
        {
            if (aIndex < 0 || aCount < 0 || aIndex + aCount > m_Length)
                throw new ArgumentNullException();

            int aKeyLen = aKey.Length;
            if (aKeyLen == 0)
                return aIndex;

            if (aKeyLen > aCount)
                return -1;
            else
                aCount = aIndex + aCount - aKeyLen + 1;

            char firstChar = aKey[0];
            for (int i = aIndex; i < aCount; ++i)
            {
                if (m_Ptr[i] == firstChar)
                {
                    int o1 = i, o2 = 0;
                    while (++o2 < aKeyLen && m_Ptr[++o1] == aKey[o2])
                    {

                    }
                    if (o2 == aKeyLen)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public int IndexOf(NativeArray<char> aKey)
        {
            return IndexOf(0,m_Length, aKey);
        }
        public int IndexOf(int aIndex, int aCount,NativeArray<char> aKey)
        {
            if (aIndex < 0 || aCount < 0 || aIndex + aCount > m_Length)
                throw new ArgumentNullException();

            int aKeyLen = aKey.Length;
            if (aKeyLen == 0)
                return aIndex;

            if (aKeyLen > aCount)
                return -1;
            else
                aCount = aIndex + aCount - aKeyLen + 1;
  
            char firstChar = aKey[0];
            for (int i = aIndex; i < aCount; ++i)
            {
                if (m_Ptr[i] == firstChar)
                {
                    int o1 = i, o2 = 0;
                    while (++o2 < aKeyLen && m_Ptr[++o1] == aKey[o2])
                    {

                    }
                    if (o2 == aKeyLen)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public int IndexOf(VStringNativeWrapper aKey)
        {
            return IndexOf(0,m_Length ,aKey);
        }
        public int IndexOf(int aIndex, int aCount,VStringNativeWrapper aKey )
        {
            if (aIndex < 0 || aCount < 0 || aIndex + aCount > m_Length)
                throw new ArgumentNullException();

            int aKeyLen = aKey.m_Length;
            if (aKeyLen == 0)
                return aIndex;

            if (aKeyLen > aCount)
                return -1;
            else
                aCount = aIndex + aCount - aKeyLen + 1;

            char firstChar = aKey[0];
            for (int i = aIndex; i < aCount; ++i)
            {
                if (m_Ptr[i] == firstChar)
                {
                    int o1 = i, o2 = 0;
                    while (++o2 < aKeyLen && m_Ptr[++o1] == aKey[o2])
                    {

                    }
                    if (o2 == aKeyLen)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        public int LastIndexOf(char aValue)
        {
            for (int i = m_Length - 1; i > -1; --i)
            {
                if (m_Ptr[i] == aValue)
                    return i;
            }
            return -1;
        }
        public int LastIndexOf(int aIndex, int aCount,char aValue )
        {
            if (aIndex < 0 || aCount < 0 || aIndex + aCount > m_Length)
                throw new ArgumentOutOfRangeException();

            aCount += aIndex;
            for (int i = aCount - 1; i >= aIndex; --i)
            {
                if (m_Ptr[i] == aValue)
                    return i;
            }
            return -1;
        }
        public int LastIndexOf(int aIndex, int aCount,string aKey)
        {
            if (aIndex < 0 || aCount < 0 || aIndex + aCount > m_Length)
                throw new ArgumentOutOfRangeException();

            int aKeyLen = aKey.Length;
            if (aKeyLen == 0)
                return aIndex;

            if (aKeyLen > aCount)
                return -1;
            else
                aCount = aIndex + aCount - aKeyLen;

            char firstChar = aKey[0];
            for (int i = aCount; i >= aIndex; --i)
            {
                if (m_Ptr[i] == firstChar)
                {
                    int o1 = i, o2 = 0;
                    while (++o2 < aKeyLen && m_Ptr[++o1] == aKey[o2])
                    {

                    }
                    if (o2 == aKeyLen)
                    {
                        return i;
                    }
                }
            }
            return -1;

        }
        public int LastIndexOf(int aIndex, int aCount,NativeArray<char> aKey )
        {
            if (aIndex < 0 || aCount < 0 || aIndex + aCount > m_Length)
                throw new ArgumentOutOfRangeException();

            int aKeyLen = aKey.Length;
            if (aKeyLen == 0)
                return aIndex;

            if (aKeyLen > aCount)
                return -1;
            else
                aCount = aIndex + aCount - aKeyLen;

            char firstChar = aKey[0];
            for (int i = aCount; i >= aIndex; --i)
            {
                if (m_Ptr[i] == firstChar)
                {
                    int o1 = i, o2 = 0;
                    while (++o2 < aKeyLen && m_Ptr[++o1] == aKey[o2])
                    {

                    }
                    if (o2 == aKeyLen)
                    {
                        return i;
                    }
                }
            }
            return -1;

        }
        public int LastIndexOf(int aIndex, int aCount,VStringNativeWrapper aKey)
        {
            if (aIndex < 0 || aCount < 0 || aIndex + aCount > m_Length)
                throw new ArgumentOutOfRangeException();

            int aKeyLen = aKey.Length;
            if (aKeyLen == 0)
                return aIndex;

            if (aKeyLen > aCount)
                return -1;
            else
                aCount = aIndex + aCount - aKeyLen;

            char firstChar = aKey[0];
            for (int i = aCount; i >= aIndex; --i)
            {
                if (m_Ptr[i] == firstChar)
                {
                    int o1 = i, o2 = 0;
                    while (++o2 < aKeyLen && m_Ptr[++o1] == aKey[o2])
                    {

                    }
                    if (o2 == aKeyLen)
                    {
                        return i;
                    }
                }
            }
            return -1;

        }

        public bool StartWith(char aValue)
        {
            return m_Length != 0 && m_Ptr[0] == aValue;
        }

        public bool StartWith(string aValue)
        {
            if (m_Length < aValue.Length)
                return false;
            for (int i = aValue.Length - 1; i > -1; --i)
            {
                if (aValue[i] != m_Ptr[i])
                    return false;
            }
            return true;
        }

        public bool StartWith(NativeArray<char> aValue)
        {
            if (m_Length < aValue.Length)
                return false;
            for (int i = aValue.Length - 1; i > -1; --i)
            {
                if (aValue[i] != m_Ptr[i])
                    return false;
            }
            return true;
        }
        public bool StartWith(VStringNativeWrapper aValue)
        {
            if (m_Length < aValue.Length)
                return false;
            for (int i = aValue.Length - 1; i > -1; --i)
            {
                if (aValue[i] != m_Ptr[i])
                    return false;
            }
            return true;
        }

        public bool EndWith(char aValue)
        {
            return m_Length != 0 && m_Ptr[m_Length-1] == aValue;
        }
        public bool EndWith(string aValue)
        {
            int aOffset = m_Length - aValue.Length;
            if (aOffset < 0)
                return false;

            for (int i = aValue.Length - 1; i > -1; --i)
            {
                if (m_Ptr[aOffset + i] != aValue[i])
                    return false;
            }
            return true;
        }
        public bool EndWith(NativeArray<char> aValue)
        {
            int aOffset = m_Length - aValue.Length;
            if (aOffset < 0)
                return false;

            for (int i = aValue.Length - 1; i > -1; --i)
            {
                if (m_Ptr[aOffset + i] != aValue[i])
                    return false;
            }
            return true;
        }

        public bool EndWith(VStringNativeWrapper aValue)
        {
            int aOffset = m_Length - aValue.Length;
            if (aOffset < 0)
                return false;

            for (int i = aValue.Length - 1; i > -1; --i)
            {
                if (m_Ptr[aOffset + i] != aValue[i])
                    return false;
            }
            return true;
        }

        public void Dispose()
        {
            if (m_Handle == 0)
                return;
            UnsafeUtility.ReleaseGCObject(m_Handle);
            m_Length = 0;
            m_Handle = 0;
        }

        public void CopyTo (int aSrcIndex,NativeArray<char> aDst,int aDstIndex,int aCount)
        {
            if (aSrcIndex < 0 || aCount < 0 || aDstIndex < 0 || aSrcIndex + aCount > m_Length || aDstIndex + aCount > aDst.Length)
                throw new ArgumentOutOfRangeException();

            var aDstPtr = (char*)NativeArrayUnsafeUtility.GetUnsafePtr(aDst);
            aCount += aSrcIndex;
            while(aSrcIndex<aCount)
            {
                aDstPtr[aDstIndex++] = m_Ptr[aSrcIndex++];
            }
        }

        public override string ToString()
        {
            return new string(m_Ptr, 0,m_Length);
        }

        public int CompareTo(VStringNativeWrapper aStrB)
        {
            int aCount = m_Length > aStrB.m_Length ? aStrB.m_Length : m_Length;

            for (int i = 0; i < aCount; ++i)
            {
                int aValue = m_Ptr[i] - aStrB.m_Ptr[i];
                if (aValue != 0)
                    return aValue;
            }
            return m_Length - aStrB.m_Length;
        }

        public int CompareTo(NativeArray<char> aStrB)
        {
            int aCount = m_Length > aStrB.Length ? aStrB.Length : m_Length;

            for (int i = 0; i < aCount; ++i)
            {
                int aValue = m_Ptr[i] - aStrB[i];
                if (aValue != 0)
                    return aValue;
            }
            return m_Length - aStrB.Length;
        }
    }


    public static class CStringNativeWrapper
    {
        public static VStringNativeWrapper NewNativeWrapper(this string aValue)
        {
            return new VStringNativeWrapper(aValue);
        }
    }
}
