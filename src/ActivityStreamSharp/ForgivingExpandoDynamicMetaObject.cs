using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace ActivityStreamSharp
{
    public class ForgivingExpandoDynamicMetaObject : DynamicMetaObject
    {
        Type objType;

        public ForgivingExpandoDynamicMetaObject(Expression expression, BindingRestrictions restrictions, object value)
            : base(expression, restrictions, value)
        {
            objType = value.GetType();
        }

        public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
        {
            var self = this.Expression;
            var dynObj = (ForgivingExpandoObject)this.Value;
            var keyExpr = Expression.Constant(binder.Name);
            var getMethod = objType.GetMethod("GetValue", BindingFlags.NonPublic | BindingFlags.Instance);
            var target = Expression.Call(Expression.Convert(self, objType),
                                         getMethod,
                                         keyExpr);
            return new DynamicMetaObject(target,
                                         BindingRestrictions.GetTypeRestriction(self, objType));
        }

        public override DynamicMetaObject BindSetMember(SetMemberBinder binder, DynamicMetaObject value)
        {
            var self = this.Expression;
            var keyExpr = Expression.Constant(binder.Name);
            var valueExpr = Expression.Convert(value.Expression, typeof(object));
            var setMethod = objType.GetMethod("SetValue", BindingFlags.NonPublic | BindingFlags.Instance);
            var target = Expression.Call(Expression.Convert(self, objType),
                                         setMethod,
                                         keyExpr,
                                         valueExpr);
            return new DynamicMetaObject(target,
                                         BindingRestrictions.GetTypeRestriction(self, objType));
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            var dynObj = (ForgivingExpandoObject)this.Value;
            return dynObj.GetDynamicMemberNames();
        }
    }
}