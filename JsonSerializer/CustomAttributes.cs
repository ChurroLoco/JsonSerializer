using System;

namespace JsonSerializer
{
	[System.AttributeUsage(System.AttributeTargets.Class |
                       	   System.AttributeTargets.Struct)]
	public class JsonSerializable : System.Attribute
	{	
		public string Name { get; private set; }
		
		public JsonSerializable (string name)
		{
			this.Name = name;
		}
		
	}
	
	[System.AttributeUsage(System.AttributeTargets.Field)]
	public class JsonField : System.Attribute
	{	
		public string Name { get; private set; }
		
		public JsonField (string name)
		{
			this.Name = name;
		}
		
	}
}

