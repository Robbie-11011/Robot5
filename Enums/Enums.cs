// Robert Laidley
// September 21st, 2024

/*
 * I primarily use enums for its use in Unity. 
 * It works as a dropdown menu for easy and guided selection.
 * They are in one file for organization, though it sacrifices some modularity
 */

/// <summary>
/// Targets for the stat bar
/// </summary>
public enum Targets
{
	player,
	enemy
}

/// <summary>
/// Stat types for the stat bar
/// </summary>
public enum Types
{
	hp,
	mana
}

/// <summary>
/// Colors for stat bar
/// </summary>
public enum Colors
{
	red,
	magenta,
	blue,
	cyan,
	green,
	yellow,
	white,
	grey,
	black,
	clear
}

/// <summary>
/// Elements for different magic/damage types.
/// Not set on Life being one of these.
/// May need to have magic types and damage types seperate.
/// </summary>
public enum Elements
{
	fire = 0,
	ice = 1,
	poison = 2,
	shock = 3,
	slash = 4,
	pierce = 5,
	force = 6,
	life = 7
}

/// <summary>
/// The types of items with their ID modifiers
/// This may very well change
/// ID and Item development was paused to focus on the Trauma Mechanics
/// </summary>
public enum ItemTypes
{
	fist = 0,
	sword = 1,
	dagger = 2,
	staff = 3,

	shirt = 11,
	leggings = 12,
	helmet = 13,
	gloves = 14,
	shoes = 15,
	
	potion = 20,
	food = 21
}

/// <summary>
/// An enum for function options for Button
/// </summary>
public enum ButtonFunction
{
	EgoAddBased = 10,
	EgoAddBlank = 0,
	IncrementEgoMax = 5
}