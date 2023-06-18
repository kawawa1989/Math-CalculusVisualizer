
public class PrecisionFloat
{
    private long _value;
    private long _precision;

    public long ActualValue => _value;
    public PrecisionFloat(double value, long precision)
    {
        _value = (long)(value * precision);
        _precision = precision;
    }

    public PrecisionFloat()
    {
    }

    public override string ToString()
    {
        double dbl = _value / (double)_precision;
        return dbl.ToString();
    }

    public static implicit operator PrecisionFloat((double value, long precision) arg) => new PrecisionFloat(arg.value, arg.precision);

    public static implicit operator double(PrecisionFloat x)
    {
        return x._value / (double)x._precision;
    }

    public static bool operator <(PrecisionFloat a, double b)
    {
        long lb = (long)(b * a._precision);
        return a._value < lb;
    }
    
    public static bool operator <=(PrecisionFloat a, double b)
    {
        long lb = (long)(b * a._precision);
        return a._value <= lb;
    }
    
    public static bool operator >(PrecisionFloat a, double b)
    {
        long lb = (long)(b * a._precision);
        return a._value > lb;
    }
    
    public static bool operator >=(PrecisionFloat a, double b)
    {
        long lb = (long)(b * a._precision);
        return a._value >= lb;
    }

    public static bool operator ==(PrecisionFloat a, PrecisionFloat b)
    {
        return a._value == b._value;
    }

    public static bool operator !=(PrecisionFloat a, PrecisionFloat b)
    {
        return a._value != b._value;
    }

    public static PrecisionFloat operator +(PrecisionFloat x, double value)
    {
        x._value += (long)(value * x._precision);
        PrecisionFloat ret = new PrecisionFloat();
        ret._value = x._value;
        ret._precision = x._precision;
        return ret;
    }
}
                
                