using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Extension.Tools
{
    public static class MethodExtensions
    {
        public static Delegate CreateDelegate(this MethodInfo method, object target = null)
        {
            var args = method.GetParameters();
            if (args.Length <= 16 && !args.Any(q => q.ParameterType.IsByRef))
            {
                Type t = null;
                if (method.ReturnType.Equals(typeof(void)))
                {
                    if (args.Length == 0)
                    {
                        t = typeof(Action);
                    }
                    else
                    {
                        t = Type.GetType($"System.Action`{args.Length}")?.MakeGenericType(args.Select(p => p.ParameterType).ToArray());
                    }
                }
                else
                {
                    var list = new List<Type>();
                    list.AddRange(args.Select(q => q.ParameterType));
                    list.Add(method.ReturnType);
                    t = Type.GetType($"System.Func`{args.Length + 1}")?.MakeGenericType(list.ToArray());
                }
                if (t != null)
                {
                    return method.CreateDelegate(t, target);
                }
            }
            return null;
        }
    }
}
