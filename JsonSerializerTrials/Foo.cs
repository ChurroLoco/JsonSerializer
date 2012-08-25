using System;
using JsonSerializer;

namespace JsonSerializerTrials
{
	[JsonSerializable("Apple")]
	public class Apple
	{
		[JsonField("apple_kind")]
		public string kind;
		
		public int id;
		
		public Apple(string kind, int id)
		{
			this.kind = kind;
			this.id = id;
		}
	}
	
	
	[JsonSerializable("Foo")]
	public class Foo
	{
		[JsonField("int_member")]
		public int x;
		
		[JsonField("float_member")]
		public float f;
		
		[JsonField("apple")]
		public Apple apple;
		
		public Foo (int x, float f)
		{
			apple = new Apple("Tasty", 5);
			
			this.x = x;
			this.f = f;
		}
	}
}

