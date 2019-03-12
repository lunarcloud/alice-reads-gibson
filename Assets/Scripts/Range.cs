using System;
using System.Collections.Generic;

public class Range<T> where T : IComparable<T>
{
    public T min;
    public T max;

    public Range(T min, T max) {
        this.min = min;
        this.max = max;
    }

    public bool contains(T value) {
        var comparer = Comparer<T>.Default;
        return comparer.Compare(min, value) <= 0 
        && comparer.Compare(max, value) > 0;
    }
}
