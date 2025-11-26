using System;

public class BindableProperty<T>
{
    private T mValue;
    public Action<T> OnValueChanged;

    public T Value
    {
        get { return mValue; }
        set
        {
            if (!value.Equals(mValue))
            {
                mValue = value;
                OnValueChanged?.Invoke(value);
            }
        }
    }
}
