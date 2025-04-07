using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;

/*
 TO DO:
    - Incorporate color coding into the load_level
    - Integrate the user color sequence to be compared with predefined one
    - Posible refactor of color coding ideology

    The UI is not included in this folder, as the file is too heavy 
    for Github. 

    Jan Smith - 11536897

*/

public class LevelManager : MonoBehaviour {

    enum Colors {   // Block's Color Code 
        RED = 5,      // Print
        ORANGE,       // Move_forward
        YELLOW,       // Move_backward
        GREEN,        // turn_right
        BLUE,         // turn_left
        INDIGO,       // for loop
        VIOLET,       // if - then
        PINK,         // if - else if - then
        BROWN,        // Number    
        GRAY,         // True
        BLACK         // False
    }
    
    // This is the block data
    [System.Serializable]
    public class Block {
        public string block_name;
        public string value;  // Can handle both string and int
    }

    // Used for object instances purposes
    [System.Serializable]
    public class BlockSequence {
       public List<Block> blocks = new List<Block>();
    }

    // This is the level data
    [System.Serializable]
    public class LevelData {
        public int level;
        public List<Block> predefined_code = new List<Block>();
    }
   
    // Unity Variables
    public TextMeshProUGUI output; // Output to the screen
    public TMP_InputField user_input;
   
    // Global Variables
    string json_output;             // The actual user's code in JSON format
    private bool next_level;        // Check if it is valid to level up
    public int current_level;       // Index of the current level
    private LevelData[] levels;     // Levels, 5 levels 
    private LevelData predefined_level;              // Used to access the current level
    private List<Colors> color_sequence;
    private List<Block> user_code_sequence;          // User's JSON Blocks
    

    void Start() {
        levels = new LevelData[5];
        current_level = 0;      // Use current level to traverse the initialization process, reset later
        next_level = false;
        initialize_levels();
    }

    public void demo_btn() { 
        convert_txt_to_json();
        compare_code(user_code_sequence);
    }

    // Set each level predefined code
    public void initialize_levels() {
        set_lvl_1();
        set_lvl_2();
        set_lvl_3();
        set_lvl_4();
        set_lvl_5();
        
        //Reset current level and preload current game definition
        current_level = 0;
        predefined_level = levels[current_level];
    }

    // Level 1 : print("Hello World")
    public void set_lvl_1() {
        BlockSequence sequence = new BlockSequence();
        Block block = create_block("print", "Hello World");
        
        sequence.blocks.Add(block);
        
        // Load all the data for this level here
        LevelData data = create_level_data(current_level + 1, sequence.blocks);

        levels[current_level++] = data;
    }

    // Level 2 : print("I love omputers!")
    public void set_lvl_2() {
        BlockSequence sequence = new BlockSequence();
        Block block = create_block("print", "I love computers!");

        sequence.blocks.Add(block);

        LevelData data = create_level_data(current_level + 1, sequence.blocks);

        levels[current_level++] = data;
    }

    // Level 3 : print("Make a 360 turn") turn_right(180){2}
    public void set_lvl_3() {
        BlockSequence sequence = new BlockSequence();
        Block block = create_block("print", "Make a 360 turn");

        sequence.blocks.Add(block);
        
        // Add two blocks into the sequence
        for(int i = 0; i < levels.Count() - 3; i++) {
            Block b_1 =  create_block("turn_right", "180");
            sequence.blocks.Add(b_1);
        }

        LevelData data = create_level_data(current_level + 1, sequence.blocks);

        levels[current_level++] = data;
    }

    // Level 4 : print("I'll move forward four times"); move_forward(i + 1){4}
    public void set_lvl_4() {
        BlockSequence sequence = new BlockSequence();
        Block block_1 = create_block("print", "I'll move forward four times"); 
        
        sequence.blocks.Add(block_1);

        // Add four blocks into the sequence
        for(int i = 0; i < levels.Count() - 1; i++) {
            Block block =  create_block("move_forward", Convert.ToString($"{i + 1}"));
            sequence.blocks.Add(block);
        }

        LevelData data = create_level_data(current_level + 1, sequence.blocks);
        
        levels[current_level++] = data;
    }    

    // Level 5 : print("Draw a square"); move_forward(50); turn_right(90){4}
    public void set_lvl_5() {
        BlockSequence sequence = new BlockSequence();
        Block block = create_block("print", "draw a square");

        sequence.blocks.Add(block);

        for(int i = 0; i < levels.Count() - 1; i++) {
            Block b_1 = create_block("move_forward", "50");
            sequence.blocks.Add(b_1);

            Block b_2 = create_block("turn_right", "90");
            sequence.blocks.Add(b_2);
        }

        LevelData data = create_level_data(current_level + 1, sequence.blocks);

        levels[current_level++] = data;
    }

    // Parse function 
    public void convert_txt_to_json () {
        string input_txt = user_input.text;
        MatchCollection matches = Regex.Matches(input_txt, @"(\w+)\((.*?)\)");      // Parse user input  
        BlockSequence user_sequence = new BlockSequence();   

        foreach (Match match in matches) {
            Block block =  create_block(match.Groups[1].Value.Trim(), match.Groups[2].Value.Trim()) ;
            user_sequence.blocks.Add(block);
        }

        json_output = JsonUtility.ToJson(user_sequence, true);  // Needs to be an instance of an object, otherwise JSON doesn't work
        Debug.Log($"Converted to JSON {json_output}");

        user_code_sequence = user_sequence.blocks;  // Get the user's sequence of blocks into this list of blocks.
    }

    // Compare user and predefined code
    public void compare_code (List<Block> user_code) {
        bool check = check_block_count(user_code);  // Check the number of block matches

        // Check if the blocks match
        if(check) check = check_blocks_matches(user_code);

        // Output to display if matches; if it does, level up. 
        output.text = check ? "true" : "false";
        if(check) next_level = true; level_up();
    }       

    // Compare block name and its value
    public bool check_blocks_matches(List<Block> user_code) {
        for (int i = 0; i < user_code.Count; i++) { 
                if (user_code[i].block_name != predefined_level.predefined_code[i].block_name 
                    || user_code[i].value != predefined_level.predefined_code[i].value) return false;
            }

        return true;    
    }

    // Check user blocks count matches with predefined code
    public bool check_block_count (List<Block> user_code) {
        return (user_code.Count != predefined_level.predefined_code.Count) ? false : true; 
    }

    public Block create_block (string name, string data) {
        return new Block {
            block_name = name, 
            value = data
        };
    }

    public LevelData create_level_data(int level_num, List<Block> code) {
        return new LevelData {
            level = level_num,
            predefined_code = code
        };
    }

    public void level_up() {
        if(next_level && current_level < levels.Count()) {
            current_level++;
            load_level();
        }
        else if(current_level >= levels.Count()) {
            Debug.Log($"You have completed the game!");
        } 
    }

    public void load_level() {
        if(current_level == levels.Count()) {
            Debug.Log($"You have completed the game!");
        }
        else {
            next_level = false;
            Debug.Log($" You've passed to level #{current_level + 1}!");
            predefined_level = levels[current_level];   // Pre-loads the next level onto the current data for this round.
        }
    }

    public int get_block_color_id(int id){
        // Based on the block's code, return id. 
        int block_id = 5;
        
        // Here, get the block's color
        // Compare the block's color, 
        // Based on color, return id. 
        
        return block_id;
    }

    public void load_color_sequence () {
        // Use current level to check predefined code
        // Based on predefined code, go through the blocks name to make up the color sequence        
        // This color sequence will be the predefined color sequence for the level
        // This function will be called every time a new level needs to be loaded 
            // PD: At load_level();

        if(next_level) color_sequence = new List<Colors>();  // If next level set to true, reset the list. 

        foreach (Block block in predefined_level.predefined_code) {
            switch(block.block_name) {
                case "print":
                    color_sequence.Add(Colors.RED);
                    break;

                case "turn_right":
                    color_sequence.Add(Colors.GREEN);
                    break;

                case "turn_left":
                    color_sequence.Add(Colors.BLUE);
                    break;

                case "move_forward":
                    color_sequence.Add(Colors.ORANGE);    
                    break;

                case "move_backward":
                    color_sequence.Add(Colors.YELLOW);
                    break;    

                case "for_loop":
                    color_sequence.Add(Colors.INDIGO);
                    break;

                case "if_then":
                    color_sequence.Add(Colors.VIOLET);
                    break;
                
                case "if_elif_then":
                    color_sequence.Add(Colors.PINK);
                    break;    

                case "true":
                    color_sequence.Add(Colors.GRAY);
                    break;

                case "false":
                    color_sequence.Add(Colors.BLACK);
                    break;
            
                default:  // Number
                    color_sequence.Add(Colors.BROWN);
                    break;
            }
        }     
    }
}
