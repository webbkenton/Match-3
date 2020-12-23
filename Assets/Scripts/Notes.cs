using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes : MonoBehaviour
{

    /*--Imediate Needs--

    Need to replace Background Asset



    first things first find a way to bring the player back to another position other then the start position;
    playerPosition == ParentPosition

    Monsters defeated stored inside of playerToken?

    overworld is only getting the overworld script. therefore it does not contain a transform. Need to get the overworld game object in order to get the transform;


    Need to figure out how to bring the player back to the spot where the fight started
    ---When player moves to a new spot store the transform of that spot inside of a variable in the persistant data? ---

    Need to destory the enemy that has been defeated.
    Need to keep the floor tiles lit up
    Need to add animations and effects to the enemies to make them feel more alive
    Need to create a dialog box and attach the completed button to it;

    If 2 matches are made at the same time. Checkforbombs() is not happening.
    Need to make Enemy attack sound plays during enemy attack.
    

    --Imediate Ends--*/

    /* -- Game Idea Needs --
     * Instead of Destorying a match 4 or 5 Have them lerp to the currentIcon.position
     * Eventually create Modifiers for Damage Value.
     --Game Needs Ends --*/

    /* -- Bugs That Need Fixed --
     * 
     * Sometimes the hint indicator gives out bad hints. And incidactes nearby tile instead of the tile to be moved.
     * Column/row bombs do not chain with Adjacent bombs
     * 
     * Shuffle runs before the board Refills. The shuffle() may need to be moved
     * 
     * The current "TypeBomb" system sucks. Needs to be updated to replace the entire Icon.
     * 
     * Star particle still not perminant

     -- End of Bugs That Need Fixed


    //1.)Need to Chain Bombs
    //2.)Need to AutoGenerate LockedTiles
    //3.)Need to come up with Damages for Each Tile break
    //4.)Lots of other stuff.
    //5.)Ask the user to turn hintmanager on/off in settings.
    //6.)Need to reset the hint timer after each move. Timer goes off every 10 seconds no matter what.
    //7.)Score does not accurately score Match4
    //8.)//Fixed// Issue with Matches not being destoryed correctly After adding buf fixes. //FIXED//
    //9.)Need to create a system that replaces TypeBomb Tiles when created.


    //1.)Possible Enemy mechanic: Use the Shuffle Board Every 3 or 4 Turns
    //2.)Possible Enemy Mechanic: Randomly Creating Destroyable tiles- Set turn limit to Destroy before Damage.
    //3.)Increase damage over duration of fight
    //4.)Disable the players abilities

        //Completed//
        //Fixed//Currently not detecting matches on board after a match is destoryed...
        //Fixed//Not sure exactly why but the Delaytimer variable that was created was preventing the MatchesOnBoard() method from being called. //Fixed//

        **Do not overlap UI Elements, Disable certain UI Features, Uncheck Raycast to allow clicking behind a UI element**

        //DONE//Need to make a UI Object for the enemy health bar.
        //DONE//Need to make a UI Object for player health bar
        //DONE//Need to make a script that manages the currency for the player.
        //DONE//Need to add a method() that checks what the TileType is when it is destoyed and Then uses the effect of that tile Type.
        //Complete// Need To Get better Fonts and Text Styles. Looks very blocky right now.//Complete//
        //DONE//Long Term -- Consider adding a small shake effect to the Enemy image when the enemy is hit with enough Damage.
        //Complete// Need to add buttons to the UI Image. //Complete//
        //DONE//Create methods for what each one of the buttons should do.
        //DONE//Enemies will need to be Scriptable object that include info like total health and level.
        //Fixed//Create an Ability manager Script and attach it to each one of the ability icons. Create Ability SO's that include things like ability name and cost
        //Fixed//Still need to figure out where to put the ability effect. Could try doing if statements in the SO to see if that works.
        //Fixed//Then we call the abilitymanager from the battleManager.
        //Fixed//Cool Down System for Abilites
        //Fixed//Make a bool coolDown. Give each Ability a cooldown int. when the ability is activated set CD int = 0; Each time the player makes a match CD int ++
        //Fixed//How to count Turns..... So make a turn counter. And when an ability is used make a local variable that stores the int of when the ability was used.
        //Fixed// Abilities can currently be used when the board is not settled. This breaks the board. Need to only allow abilities if boardstate = move.
        //Recognize the type of Tile Used during the match Detected
     * //Use Gem Tiles as Currency and Damage.
     * //DONE//Establish an HP Bar for the player and Enemy.
     * //Establish Damage Value for each type of tile.
     * //FIXED//Drag System feels "Clunky" seems unresponsive and not smoothe
     * //FIXED//The Adjacent bomb particle effect has a 1second Delay before it starts.
     * //FIXED//Shuffle can happen while the board is refilling which will then glitch the board. Shuffle()Needs to be delayed.
     * //Cascading tiles do not create bombs corectly.
    //Completed//

    /Ability Ideas//
    Find and Replace
    Restore Health
    Damage Enemy
    Reduce Enemy rage Counter
    Create Random Adjacent Bomb
    Damage Yourself To Damage the enemy
    */
}

