using System;
public class EasyParse
{
    public static T Enumelate<T>(object o, T initval)
    {
        T otype = initval;

        if (o != null)
        {
            if (Enum.IsDefined(typeof(T), o) == true)
            {
                otype = (T)Enum.Parse(typeof(T), o.ToString());
            }
        }

        return otype;
    }
}