// Robert Laidley [8.28.24] [8189]

/// <summary>
/// A static class for ID things
/// </summary>
public static class ID {
	
	/// <summary>
	/// Get the item code in int,int from string
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	public static (int, int) Parse(string id)
	{
		string[] idSplit = id.Split("-");

		int type = int.Parse(idSplit[0]);
		int item = int.Parse(idSplit[1]);

		return (type, item);
	}

}
