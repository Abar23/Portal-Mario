The level used in game can be defined in the level_1.json file in the LevelTileMap folder.

At the top of the file are the "Dimensions" which define the boundary or field of play
for the entire level. The values for "Width" and "Height" must be entered in an 
amount of pixels.

The rest of the JSON file consists of an array of "Entities" which allows for the simple
creation of any GameObject. Each entity requires a "Type" field and an "X" and "Y" which
defines the intial position of the entity. The entries also require a "Collidable" field
which tells which objects will be added to the Map for collision. The values for "Collidable"
are either "true" or "false". 

Entities also may have two optional fields: "InARow" and "Gap". The "InARow" allows you
to specify the amount of the same object you would like to appear at the same level in a row on
the Y axis. The "Gap" option allows you to space out the objects in a row by a number of pixels.
WARNING: Placing Hidden Blocks in a row does not work. 

Entities also have a "Layer" field controling where the object will exist in terms of the parallax
scrolling. Layers options are 0, 1, and 2. If you leave you the "Layer" field, it defaults to 0, which
is the layer where the action takes place. Layers 1 and 2 are for background objects. 

The "Type" field may have any of the following names for each GameObject:

"Player"

"Mushroom"
"OneUp"
"Star"
"FireFlower"
"BlockCoin"
"UndergroundCoin"

"Goomba"
"GreenKoopa"
"RedKoopa"
"PiranhaPlant"
"Bowser"

"BrickBlock"
"FloorBlock"
"HiddenBlock"
"PyramidBlock"
"QuestionBlock"

"SmallHill"
"BigHill"
"SmallBush"
"BigBush"
"SmallCloud"
"BigCloud"
"SmallTree"
"BigTree"
"Castle"
"Flag"

"Pipe"
"MediumPipe"
"TallPipe"

Blocks are 32x32 pixels, so picking values of "X" and "Y" that are multiples of 32
works best with the grid.



A small example of the JSON level format is shown below:

######################  EXAMPLE  ######################

{
  "Dimensions": {
    "Height": 480,
    "Width":  512 
  },
  
  "Entities": [
    {
      "Type": "BigHill",
      "X": 0,
      "Y": 346,
      "Collidable": false,
      "InARow": 2,
      "Gap": 32,
	  "Layer": 2
    },

    {
      "Type": "Mushroom",
      "X": "32",
      "Y": "32",
      "Collidable": "true" 
    },
	{
      "Type": "Goomba",
      "X": "200",
      "Y": "384",
      "Collidable": "true" 
    },
	{
      "Type": "QuestionBlock",
      "X": "32",
      "Y": "288",
      "Collidable": "true" 
    },
    {
      "Type": "FloorBlock",
      "X": 0,
      "Y": 416,
      "Collidable": true,
      "InARow": 16
    },
    {
      "Type": "FloorBlock",
      "X": 0,
      "Y": 448,
      "Collidable": false,
      "InARow": 16
    },

    {
      "Type": "Player",
      "X": 0,
      "Y": 352,
      "Collidable": true
    }
  ]
}

######################  END OF EXAMPLE  ######################
 