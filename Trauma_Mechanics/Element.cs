// Robert Laidley
// September 22nd, 2024

using System;

/// <summary>
/// Element for damage and healing (or damage type)
/// </summary>
[Serializable]
public class Element
{
	/// <summary>
	/// Type of element
	/// </summary>
	public Elements Type;

	/// <summary>
	/// Power level of element
	/// </summary>
	public float Power;

	/// <summary>
	/// Default constructor
	/// Sets the element to a power level 0 fire type
	/// </summary>
	public Element()
	{
		Type = Elements.fire;
		Power = 0;
	}

	/// <summary>
	/// Make a new element
	/// </summary>
	/// <param name="type">Type of Element</param>
	/// <param name="power">Power Level</param>
	public Element(Elements type, float power)
	{
		Type = type;
		Power = power;
	}
}
