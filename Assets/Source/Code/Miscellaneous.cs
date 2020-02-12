using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Miscellaneous
{
    public class DictionaryCreator<T>
    {
        public int Length { get { return Pairs.Count; } }
        public Dictionary<int, T> Pairs { get; private set; }
        public DictionaryCreator(T[] array)
        {
            Pairs = new Dictionary<int, T>(array.Length);
            for (int i = 0; i < array.Length; ++i)
                Pairs.Add(i, array[i]);
        }
        public T this[int i]
        {
            get
            {
                if (i >= Length || i < 0)
                    throw new Exception("index is out of range for this dictionnary");
                Pairs.TryGetValue(i, out T valueToReturn);
                return valueToReturn;
            }
        }
    }
    public interface ITargetable
    {

    }
    public interface IDamageable
    {
        void TakeDamage(float damage);
        HP HP { get; }
    }
}
