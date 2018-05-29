using System;

namespace Game3D {

    /// <summary>
    /// Атрибут поля или свойста, показывающий, что оно будет сохраняться, если GO предназначен для сохранения
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class SaveableAttribute : Attribute {

		
		
	}
	
}