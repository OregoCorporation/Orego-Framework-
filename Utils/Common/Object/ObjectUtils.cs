using System;

namespace OregoFramework.Util
{
    public static class ObjectUtils
    {
        public static T It<T>(this T it, Action<T> action)
        {
            action.Invoke(it);
            return it;
        }

        public static T As<T>(this object it)
        {
            return (T) it;
        }

        public static T To<T>(this object it, Func<T> func)
        {
            return func.Invoke();
        }
    }
}