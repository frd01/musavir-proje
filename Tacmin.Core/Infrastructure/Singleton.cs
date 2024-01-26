﻿using System;

namespace Tacmin.Core
{
    public abstract class Singleton<T> where T : class
    {
        private static readonly Lazy<T> _instance = new Lazy<T>(() => CreateInstanceOfT());

        public static T Instance => _instance.Value;

        private static T CreateInstanceOfT()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }
    }
}
