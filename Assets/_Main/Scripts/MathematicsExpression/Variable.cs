using System;

namespace MathematicsExpression
{
    /// <summary>
    /// x^3 などの変数 x を用いた単項式
    /// 指数を持つ
    /// </summary>
    public class Variable
    {
        public double Coefficient;
        public Variable Base;
        public Variable Exponent;

        public Variable(double coefficient)
        {
            Coefficient = coefficient;
        }
        
        public Variable(Variable baseValue, Variable exponent)
        {
            Base = baseValue;
            Exponent = exponent;
        }

        public static Variable operator *(Variable a, Variable b)
        {
            return new Variable(a.GetValue() * b.GetValue());
        }
        
        public static implicit operator Variable(double v)
        {
            return new Variable(v);
        }

        public static implicit operator double(Variable v)
        {
            return v.GetValue();
        }

        public double GetValue(Variable baseValue)
        {
            if (Exponent != null)
            {
                return Exponent.GetValue(baseValue);
            }

            if (Base != null)
            {
                return Math.Pow(baseValue.GetValue(), Base.GetValue());
            }

            return Math.Pow(baseValue, Coefficient);
        }

        public double GetValue()
        {
            if (Exponent != null)
            {
                return Exponent.GetValue(Base);
            }

            if (Base != null)
            {
                return Base.GetValue();
            }

            return Coefficient;
        }
    }
}