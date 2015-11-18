using System;
using System.Linq;

namespace NCubeSolvers.Core.Extensions
{
    public static class EnumEx
    {
        public static TEnum GetMaxValue<TEnum>()
            where TEnum : IComparable, IConvertible, IFormattable
        {
            Type type = typeof(TEnum);

            if (!type.IsSubclassOf(typeof(Enum)))
                throw new
                    InvalidCastException
                        ("Cannot cast '" + type.FullName + "' to System.Enum.");

            return (TEnum)Enum.ToObject(type, Enum.GetValues(type).Cast<int>().Max());
        }

        public static TEnum GetMinValue<TEnum>()
            where TEnum : IComparable, IConvertible, IFormattable
        {
            Type type = typeof(TEnum);

            if (!type.IsSubclassOf(typeof(Enum)))
                throw new
                    InvalidCastException
                        ("Cannot cast '" + type.FullName + "' to System.Enum.");

            return (TEnum)Enum.ToObject(type, Enum.GetValues(type).Cast<int>().Min());
        }
    }
}
