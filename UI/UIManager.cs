// Robert Laidley
// September 20th, 2024

using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// A manager object for the UI for the menu and terminal
/// NOTE: When a number is not entered for setting values, it will default to 0 for now
/// </summary>
public class UIManager : MonoBehaviour
{
	/// <summary>
	/// Cycles up the list of previous commands
	/// </summary>
	public KeyCode Key_CycleUp;

	/// <summary>
	/// Cycles down the list of previous commands
	/// </summary>
	public KeyCode Key_CycleDown;

	/// <summary>
	/// The key code for the menu button
	/// </summary>
	public KeyCode Key_ToggleMenu;

	/// <summary>
	/// The key code for closing the game
	/// </summary>
	public KeyCode Key_EndGame;

	/// <summary>
	/// The key code for executing a command
	/// </summary>
	public KeyCode Key_ExecuteCommand;

	/// <summary>
	/// The bool for the menu button being pressed
	/// </summary>
	private bool ToggleMenuPressed;

	/// <summary>
	/// The bool for the end the game button being pressed
	/// </summary>
	private bool EndGamePressed;

	/// <summary>
	/// The bool for the executing a command button being pressed
	/// </summary>
	private bool ExecuteCommandPressed;

	/// <summary>
	/// The menu canvas
	/// </summary>
	public Canvas MenuCanvas;

	/// <summary>
	/// The inventory canvas
	/// </summary>
	public Canvas InventoryCanvas;

	/// <summary>
	/// The input field object
	/// </summary>
	public TMP_InputField Input_Field;

	/// <summary>
	/// The text input for commands
	/// </summary>
	private string InputText;

	/// <summary>
	/// The output text field object
	/// </summary>
	public Line Output;

	/// <summary>
	/// The bool for whether the menu is active or not
	/// </summary>
	public bool IsActive;

	/// <summary>
	/// Dev mode for higher power commands
	/// Default off
	/// </summary>
	public bool DevMode = false;

	/// <summary>
	/// The target object to use commands on
	/// </summary>
	private GameObject TargetObject;

	/// <summary>
	/// The target stats to use commands on
	/// </summary>
	private Stats TargetStats;

	/// <summary>
	/// The target manager to use commands on
	/// </summary>
	private Player_Manager TargetManager;

	/// <summary>
	/// The target ego list to use commands on
	/// </summary>
	private Egos TargetEgos;

	/// <summary>
	/// The target ego to use commands on
	/// </summary>
	private Ego TargetEgo;

	/// <summary>
	/// The input string being processed
	/// </summary>
	private string[] Words;

	/// <summary>
	/// The commands previously executed
	/// </summary>
	private List<string> PreviousCommands;

	/// <summary>
	/// Current previous command index
	/// </summary>
	private int CmdIndex;

	/// <summary>
	/// Called once
	/// Default menu to not active
	/// </summary>
	private void Start()
	{
		PreviousCommands = new List<string>();

		MenuCanvas.gameObject.SetActive(false);
		PreviousCommands.Add("");
	}

	/// <summary>
	/// Called once per frame
	/// </summary>
	private void Update()
	{
		ToggleMenuPressed = Input.GetKeyDown(Key_ToggleMenu);
		EndGamePressed = Input.GetKeyDown(Key_EndGame);
		ExecuteCommandPressed = Input.GetKeyDown(Key_ExecuteCommand);

		bool cycleUp = Input.GetKeyDown(Key_CycleUp);
		bool cycleDown = Input.GetKeyDown(Key_CycleDown);

		if (cycleUp && CmdIndex < PreviousCommands.Count - 1)
		{
			CmdIndex++;
			Debug.Log($"{CmdIndex}/{PreviousCommands.Count - 1}");
		}

		if (cycleDown && CmdIndex > 0)
		{
			CmdIndex--;
			Debug.Log($"{CmdIndex}/{PreviousCommands.Count - 1}");
		}

		if (cycleUp || cycleDown)
		{
			Input_Field.text = PreviousCommands[CmdIndex];
		}

		if (Input_Field.text != PreviousCommands[CmdIndex])
		{
			CmdIndex = 0;
		}

		if (ToggleMenuPressed)
		{
			ToggleMenu();
		}

		if (EndGamePressed)
		{
			Application.Quit();

#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
		}

		if (ExecuteCommandPressed && IsActive)
		{
			Execute();
		}
	}

	/// <summary>
	/// Toggles the Menu open/close
	/// </summary>
	public void ToggleMenu()
	{
		IsActive = !IsActive;

		//gameobject.GetComponent<InventoryManager>().
		if (IsActive)
		{
			InventoryCanvas.gameObject.SetActive(false);
		}
		else
		{
			InventoryCanvas.gameObject.SetActive(gameObject.GetComponent<InventoryManager>().IsActive);
		}


		MenuCanvas.gameObject.SetActive(IsActive);

		// Select the input field when the UI is activated
		if (IsActive)
		{
			EventSystem.current.SetSelectedGameObject(Input_Field.gameObject);

			// Simulate a pointer click to open the keyboard on touch devices
			Input_Field.OnPointerClick(new PointerEventData(EventSystem.current));
		}

		RefreshTimeSpeed();
	}

	/// <summary>
	/// Executes a command
	/// </summary>
	public void Execute()
	{
		// Get the text from the input field
		InputText = Input_Field.text;

		// Split the input
		Words = InputText.Split(" ");

		if (Words.Length > 0)
		{
			if (DevMode)
			{


				switch (Words[0])
				{
					case "set":

						if (Words.Length < 2)
						{
							UnknownCmdError();
							break;
						}

						switch (Words[1])
						{
							case "player":
								TargetObject = GameObject.Find("Player");
								TargetStats = TargetObject.GetComponent<Stats>();
								TargetManager = TargetObject.GetComponent<Player_Manager>();
								TargetEgos = TargetObject.GetComponent<Egos>();
								TargetEgo = TargetEgos.EgoList[TargetManager.Ego_Index];

								if (Words.Length < 3)
								{
									UnknownCmdError();
									break;
								}

								switch (Words[2])
								{
									case "mana":
										SetStat(ref TargetStats.Mana);
										Output.Text = $"[ MANA UPDATED ]";
										Output.TextColor = Color.green;
										break;
									case "hp":
										SetStat(ref TargetStats.Hp);
										Output.Text = $"[ HP UPDATED ]";
										Output.TextColor = Color.green;
										break;
									case "a1":
										SetAbility(ref TargetManager.Ability1.Stats);
										TargetManager.UpdateCost();
										Output.Text = $"[ ABILITY 1 UPDATED ]";
										Output.TextColor = Color.green;
										break;
									case "a2":
										SetAbility(ref TargetManager.Ability2.Stats);
										TargetManager.UpdateCost();
										Output.Text = $"[ ABILITY 2 UPDATED ]";
										Output.TextColor = Color.green;
										break;
									case "ego":
										if (Words.Length < 4)
										{
											UnknownCmdError();
											break;
										}

										switch (Words[3])
										{
											case "res":
												SetDamageTypes(ref TargetEgo.Resistances);
												Output.Text = $"[ EGO RESISTANCES UPDATED ]";
												Output.TextColor = Color.green;
												break;
											case "cnt":
												SetDamageTypes(ref TargetEgo.Damage_Counters);
												Output.Text = $"[ EGO COUNTER UPDATED ]";
												Output.TextColor = Color.green;
												break;
											case "new":
												TargetEgo = new Ego(0, 500);
												TargetEgo.Initialize();
												TargetEgos.AddEgo(TargetEgo);
												Output.Text = $"[ EGO ADDED ]";
												Output.TextColor = Color.green;
												break;
											case "max":
												TargetEgos.Size.Max = GetInputFloat(4);
												Output.Text = $"[ EGO SIZE UPDATED ]";
												Output.TextColor = Color.green;
												break;
											case "?":
												Output.Text = "[ TRY ]\n - res\n - cnt\n - new\n - max";
												Output.TextColor = Color.cyan;
												break;
											default:
												UnknownCmdError();
												break;
										}
										break;
									case "credit":
										Inventory targetInventory = TargetObject.GetComponent<Inventory>();

										if (Words.Length < 4)
										{
											UnknownCmdError();
											break;
										}

										switch (Words[3])
										{
											case "add":
												targetInventory.AddCredits((int)GetInputFloat(4));
												break;
											case "set":
												targetInventory.SetCredits((int)GetInputFloat(4));
												break;
											case "?":
												Output.Text = "[ TRY ]\n - add #\n - set #";
												Output.TextColor = Color.cyan;
												break;
											default:
												UnknownCmdError();
												break;
										}
										break;
									case "?":
										Output.Text = "[ TRY ]\n - mana\n - hp\n - a1\n - a2\n - ego\n - credit";
										Output.TextColor = Color.cyan;
										break;
									default:
										UnknownCmdError();
										break;
								}

								break;
							case "?":
								Output.Text = "[ TRY ]\n - player";
								Output.TextColor = Color.cyan;
								break;
							default:
								UnknownCmdError();
								break;
						}
						break;
					case "toggledevmode":
						DevMode = !DevMode;
						Output.Text = "[ DEVMODE DEACTIVATED ]";
						Output.TextColor = Color.white;
						break;
					case "?":
						Output.Text = "[ TRY ]\n - set\n - toggledevmode";
						Output.TextColor = Color.cyan;
						break;
					default:
						UnknownCmdError();
						break;
				}
			}
			else
			{
				switch (Words[0])
				{
					case "set":

						switch (Words[1])
						{
							case "player":
								TargetObject = GameObject.Find("Player");
								TargetManager = TargetObject.GetComponent<Player_Manager>();

								if (Words.Length < 3)
								{
									UnknownCmdError();
									break;
								}

								switch (Words[2])
								{
									case "a1":
										if (SetAbility(ref TargetManager.Ability1.Stats))
										{
											Output.Text = $"[ ABILITY 1 UPDATED ]";
											Output.TextColor = Color.green;
										}

										TargetManager.UpdateCost();
										break;
									case "a2":
										;
										if (SetAbility(ref TargetManager.Ability2.Stats))
										{
											Output.Text = $"[ ABILITY 2 UPDATED ]";
											Output.TextColor = Color.green;
										}

										TargetManager.UpdateCost();
										break;
									case "?":
										Output.Text = "[ TRY ]\n - a1\n - a2";
										Output.TextColor = Color.cyan;
										break;
									default:
										UnknownCmdError();
										break;
								}
								break;
							case "?":
								Output.Text = "[ TRY ]\n - player";
								Output.TextColor = Color.cyan;
								break;
							default:
								UnknownCmdError();
								break;
						}
						break;
					case "toggledevmode":
						DevMode = !DevMode;

						Output.Text = "[ DEVMODE ACTIVATED ]";

						Output.TextColor = Color.white;
						break;
					case "?":
						Output.Text = "[ TRY ]\n - set\n - toggledevmode";
						Output.TextColor = Color.cyan;
						break;
					default:
						UnknownCmdError();
						break;
				}
			}
		} else
		{
			Output.Text = "[ TRY ]\n - set\n - toggledevmode";
			Output.TextColor = Color.cyan;
		}

		// Saves the current command executed
		PreviousCommands.Insert(1, InputText);

		if (PreviousCommands.Count > 2)
		{
			if (PreviousCommands[1] == PreviousCommands[2])
			{
				PreviousCommands.RemoveAt(1);
			}
		}

		// Clear the input field
		Input_Field.text = "";

		EventSystem.current.SetSelectedGameObject(Input_Field.gameObject);
		Input_Field.OnPointerClick(new PointerEventData(EventSystem.current));
	}

	/// <summary>
	/// Gets the input of the float in the designated index
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	private float GetInputFloat(int index)
	{
		float mod;

		if (Words.Length >= index)
		{
			return (float.TryParse(Words[index], out mod)) ? mod : 0;
		}

		Output.Text = ($"No Value Argument Found");
		Output.TextColor = Color.red;
		return 0;
	}

	/// <summary>
	/// Set a stat to a user-defined number
	/// </summary>
	/// <param name="stat"></param>
	/// <returns></returns>
	private bool SetStat(ref Stat stat)
	{
		float mod = 0;

		if (Words.Length == 5)
		{
			mod = GetInputFloat(4);
		}

		if (!Util.InRange(0, mod, stat.Max) && Words[3] == "value")
		{
			Debug.Log($"Value Out of Range");
			return false;
		}

		switch (Words[3])
		{
			case "value":
				stat.Value = mod;
				break;
			case "base":
				stat.BaseMax = mod;
				break;
			case "max":
				stat.Max = mod;
				break;
			case "?":
				Output.Text = "[ TRY ]\n - value #\n - base #\n - max #";
				Output.TextColor = Color.cyan;
				return false;
			default:
				UnknownCmdError();
				return false;
		}
		return true;
	}

	/// <summary>
	/// Sets the ability at a low-authority level
	/// </summary>
	/// <param name="ability"></param>
	/// <returns></returns>
	private bool SetAbility(ref AbilityStats ability)
	{
		float mod = 0;

		if (Words.Length == 5)
		{
			mod = GetInputFloat(4);
		}

		switch (Words[3])
		{
			case "cool":
				ability.Timer.Duration = mod;
				break;
			case "power":
				ability.Element_Type.Power = mod;
				break;
			case "duration":
				ability.Duration = mod;
				break;
			case "x":
				ability.Offset.x = mod;
				break;
			case "y":
				ability.Offset.y = mod;
				break;
			case "?":
				Output.Text = "[ TRY ]\n - cool #\n - power #\n - duration #\n - x #\n - y #";
				Output.TextColor = Color.cyan;
				return false;
			default:
				UnknownCmdError();
				return false;
		}

		return true;
	}

	/// <summary>
	/// Set a damagetype (Resistance or Damage_Counter element) to a user-defined number
	/// </summary>
	/// <param name="types"></param>
	/// <returns></returns>
	private bool SetDamageTypes(ref DamageTypes types)
	{
		float mod = 0;

		if (Words.Length == 6)
		{
			mod = GetInputFloat(5);
		}

		if (!Util.InRange(types.Bounds.Min, mod, types.Bounds.Max))
		{
			Debug.Log($"Value Out of Range");
			return false;
		}

		switch (Words[4])
		{
			case "fire":
				types.Types[0].Power = mod;
				break;
			case "ice":
				types.Types[1].Power = mod;
				break;
			case "poison":
				types.Types[2].Power = mod;
				break;
			case "shock":
				types.Types[3].Power = mod;
				break;
			case "slash":
				types.Types[4].Power = mod;
				break;
			case "pierce":
				types.Types[5].Power = mod;
				break;
			case "force":
				types.Types[6].Power = mod;
				break;
			case "?":
				Output.Text = "[ TRY ]\n - fire #\n - ice #\n - poison #\n - shock #\n - slash #\n - pierce #\n - force #";
				Output.TextColor = Color.cyan;
				return false;
			default:
				UnknownCmdError();
				return false;
		}
		return true;
	}

	/// <summary>
	/// Pauses time if the menu is active
	/// </summary>
	private void RefreshTimeSpeed()
	{
		// Stops time when the menu is active
		if (IsActive)
		{
			Time.timeScale = 0f;
		}
		else
		{
			Time.timeScale = 1f;
		}
	}

	private void UnknownCmdError()
	{
		Output.Text = $"[ UNKNOWN COMMAND ]\n\"{InputText}\"\n[ TRY '?' ]";
		Output.TextColor = Color.red;
	}
}