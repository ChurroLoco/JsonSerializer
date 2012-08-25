using System;
using System.Reflection;

namespace JsonSerializer
{
	public static class JSON
	{
		public static string Serialize(object obj)
		{
			if (obj != null)
			{
				string objectName = null;
				
				// Loop through the attributes of this type to see if it is JsonSerializable
				Type t = obj.GetType();
				Attribute[] attributes = Attribute.GetCustomAttributes(t);
				foreach (Attribute attribute in attributes)
				{
					if (attribute is JsonSerializable)
					{
						objectName = ((JsonSerializable)attribute).Name;
					}	
				}
				
				if (string.IsNullOrEmpty(objectName))
				{
					return null;
				}
				
				string objectMembers = "";
				bool first = true;
				
				foreach (MemberInfo member in t.GetFields())
				{
					Type memberType = member.GetType();
					bool serializeField = false;
					bool serializeRecursive = false;
					
					if (memberType.IsClass)
					{		
						FieldInfo field = t.GetField(member.Name);
						
						// Go through each attribute to see if it is marked to be serialized
						Attribute[] memberAttributes = member.GetCustomAttributes(true) as Attribute[];
						if (memberAttributes != null)
						{
							foreach (Attribute attribute in memberAttributes)
							{			
								// Check if field is marked to be serialized
								if (attribute is JsonField)
								{	
									object fieldValue = field.GetValue(obj);
									Type valueType = fieldValue.GetType();
									// If this is one of the JSON supported primitive than use it.  Otherwise
									// see if we can serialize the object recursively.
									if (valueType == typeof(int) ||
										valueType == typeof(float) ||
										valueType == typeof(bool))
										
									{
										if (first == false)
										{
											objectMembers += ",";
										}
										first = false;
										
										objectMembers += string.Format("\"{0}\":{1}", ((JsonField)attribute).Name, fieldValue);
									}
									else if (valueType == typeof(string))
									{
										if (first == false)
										{
											objectMembers += ",";
											
										}
										first = false;
								
										// Handle the string being null
										if (fieldValue != null)
										{
											objectMembers += string.Format("\"{0}\":\"{1}\"", ((JsonField)attribute).Name, fieldValue);
										}
										else
										{
											objectMembers += string.Format("\"{0}\":null", ((JsonField)attribute).Name);
										}
									}
									else
									{
										// Handle the object being null
										if (fieldValue != null)
										{
											string objectString;
											objectString = Serialize(fieldValue);
											
											// If the objectString is null then this object failed to serialize.
											// Just leave it out if it failed.
											if (objectString != null)
											{
												if (first == false)
												{
													objectMembers += ",";
													
												}
												first = false;
												
												objectMembers += string.Format("\"{0}\":{1}", ((JsonField)attribute).Name, objectString);
											}
										}
										else
										{
											if (first == false)
											{
												objectMembers += ",";
												
											}
											first = false;
											
											objectMembers += string.Format("\"{0}\":null", ((JsonField)attribute).Name);
										}
									}
								}
							}
						}
						else
						{
							break;
						}
					}
				}
				
				//string jsonString = string.Format("{{\"{0}\":{{{1}}}}}", objectName, objectMembers);
				
				string jsonString = string.Format("{{{0}}}", objectMembers);
				
				return jsonString;

			}
			
			return null;
		}
	}
}

