// Robert Laidley
// September 23rd, 2024

Welcome to Robot5 (pronounced Robots)!

This game has nothing to do with robots, 
    but a certain show gave me the idea, 
    so I named it after that for now.

NOTES {
    1. The shadow creature has no animations other than idle.
    2. The ego creation is not natural. Simply point-buy at the moment.
    3. There is currently no way to spawn in more enemies or gain more mana naturally (other than collecting mana from fallen enemies)
    4. The loot image currently says '- E -', but it should be '- F -' (Switched the key. It should be auto-switched, but that takes time I do not have.)
    5. The inventory is not developed. (Paused for focus on the main goal)
}

TIPS {
    ------------------------ LOOT -----------------------------------------------
    Fallen enemies drop mana.
    Collect this mana by looting them. (Loot = F)
    -----------------------------------------------------------------------------

    ------------------------ TRAUMA MECHANICS -----------------------------------
    The weird looking spike thing is a button. (Up-right)
    When activated use the interact key, it will give the player a new ego if the ego list is not full.
    The cost is 1 credit for now.

    The other spike thing is another button. (On the wall)
    This increases the number of egos you can have.
    The cost is 1 credit for now.

    New egos will be your currently selected ego, plus its damage counters.

    When taking damage, the resistances will determine how much you take of each attack.
    The damage taken will be added to your damage counters for that particular ego.
    -----------------------------------------------------------------------------

    ------------------------ MENU TERMINAL --------------------------------------
    You may put a '?' at any point to see the next possible commands.
    Type 'toggledevmode' for switching in and out of dev mode. (Default is false)
    Any number modifier that is not input defaults to 0.
    Use the up and down arrows for previous commands.
    -----------------------------------------------------------------------------
}

Controls:
Tab 
{
    Toggles Menu
}

Enter (in-menu) 
{
    Execute Command
}

Up-Arrow (in-menu) 
{
    Cycles previous commands back
}

Down-Arrow (in-menu) 
{
    Cycles previous commands forth
}

I 
{
    Toggles Inventory
}

F 
{
    Interact
}

Escape 
{
    Closes Game
}

Movement 
{
    A = Left
    D = Right
    Space = Jump
    Shift = Sprint
}

Left Mouse Click 
{
    Use Ability 1 (fireball)
}

Right Mouse Click
{
    Use Ability 2 (heal)
}

Commands 
--------------------------------

DEVMODE '
{
    EXAMPLES:
    set player mana value 750
    set player ego res force 100
    set player a1 power 10
    get player mana

    set
    {
        player 
        {
            mana 
            {
                value #
                base #
                max #
            }
            hp 
            {
                value #
                base #
                max #
            }
            a1 
            {
                cool #
                power #
                duration #
                x #
                y #
            }
            a2 
            {
                cool #
                power #
                duration #
                x #
                y #
            }
            ego 
            {
                res 
                {
                    fire #
                    ice #
                    poison #
                    shock #
                    slash #
                    pierce #
                    force #
                }
                cnt 
                {
                    fire #
                    ice #
                    poison #
                    shock #
                    slash #
                    pierce #
                    force #
                }
                new #
                max #
            }
            credit
            {
                add #
                set #
            }
        }
    }
    toggledevmode
}

NON-DEVMODE 
{
    set
    {
        player
        {
            a1 
            {
                cool #
                power #
                duration #
                x #
                y #
            }
            a2 
            {
                cool #
                power #
                duration #
                x #
                y #
            }
        }
    }
    toggledevmode
}

