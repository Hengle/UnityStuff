# 日志

## StringNativeWrapper说明

Unity的job多线程并非严格要求Blittable数据。
基本数据类型char和bool其实也是可以的。
虽然没在其他平台测试过，但PC和Android平台完全没问题。
另外job多线程是可以用unsafe指针访问到托管类的数据，
只是没办法控制多线程读写安全，
但如果像字符串这类只读数据就不存在这类问题。
完全不需要像ECS那么麻烦的把字符串再拷贝一遍到非托管区。
StringNativeWrapper旨在包装一个字符串,被Job多线程访问查询.
可以参考使用类似的方法在job多线程中访问到其他的托管引用类型，
这将突破DOTS的使用限制，并一定程度上提升效能。  
**注意 StringNativeWrapper使用完后必须调用Dispose释放,
否则被引用的字符串会因为无法GC回收而导致内存泄露.**

