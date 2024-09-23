// Robert Laidley
// September 22nd, 2024

/// <summary>
/// A utility class for general static functions
/// </summary>
public static class Util
{
	/// <summary>
	/// Checks if a float value is in-range (min,max)
	/// </summary>
	/// <param name="min"></param>
	/// <param name="value"></param>
	/// <param name="max"></param>
	/// <returns></returns>
	public static bool InRange(float min, float value, float max)
	{
		return (value <= max && value >= min);
	}
}