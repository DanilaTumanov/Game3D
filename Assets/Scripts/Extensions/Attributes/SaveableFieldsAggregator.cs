using System;

namespace Game3D
{

    /// <summary>
    /// Атрибут метода, показывающий, что метод будет использоваться как аггрегатор сохраняемых полей для типа, указанного в конструкторе
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class SaveableFieldsAggregator : Attribute
    {

        public Type Type { get; private set; }

        public SaveableFieldsAggregator(Type type)
        {
            Type = type;
        }

    }

}