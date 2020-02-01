using System;
using System.Diagnostics;

namespace PriorityQueue
{
    [DebuggerDisplay("[Value: {Value}, Priority: {Priority}]")]
    public readonly struct ValuePriority<T, TPriority> : System.IEquatable<ValuePriority<T, TPriority>>
    {
        public T Value { get; }
        public TPriority Priority { get; }

        public ValuePriority(T value, TPriority priority)
        {
            Value = value;
            Priority = priority;
        }

        public override bool Equals(object obj)
        {
            if (obj is ValuePriority<T, TPriority> vp)
            {
                return Equals(vp);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Priority);
        }

        public static bool operator ==(ValuePriority<T, TPriority> left, ValuePriority<T, TPriority> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ValuePriority<T, TPriority> left, ValuePriority<T, TPriority> right)
        {
            return !(left == right);
        }

        public bool Equals(ValuePriority<T, TPriority> other)
        {
            return Value.Equals(other.Value) && Priority.Equals(other.Priority);
        }
    }
}
