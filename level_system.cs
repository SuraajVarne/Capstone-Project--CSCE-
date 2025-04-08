using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;

/*
 TO DO:
    - Fix the non-object reference regarding the color sequence on add_level_color()

    Jan Smith - 11536897

*/

public class LevelManager : MonoBehaviour {

    public enum Colors {   // Block's Color Code 
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

    [System.Serializable]
    public class ColorSequence {
        public List<Colors> colors = new List<Colors>();
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
    string json_output;                             // The actual user's code in JSON format
    private bool next_level;                        // Check if it is valid to level up
    public int current_level;                       // Index of the current level
    private LevelData[] levels;                     // Levels, 5 levels 
    private LevelData predefined_level;             // Used to access the current level
    private List<Block> user_code_sequence;         // User's JSON Blocks
    private List<Colors> user_color_sequence;
    private ColorSequence[] levels_color_codes;
    private List<Colors> predefined_color_sequence;

    void Start() {
        levels = new LevelData[5];
        levels_color_codes = new ColorSequence[5];
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
        
        //Reset current level and preload current level definition
        current_level = 0;
        predefined_level = levels[current_level];
        predefined_color_sequence = levels_color_codes[current_level].colors;
    }

    // Level 1 : print("Hello World")
    public void set_lvl_1() {
        BlockSequence sequence = new BlockSequence();
        Block block = create_block("print", "Hello World");
        
        sequence.blocks.Add(block);
        Colors color = get_block_color_id(block.block_name);
        
        // Load all the data for this level here
        LevelData data = create_level_data(current_level + 1, sequence.blocks);

        add_level_color(color);        
        levels[current_level++] = data;
    }

    // Level 2 : print("I love omputers!")
    public void set_lvl_2() {
        BlockSequence sequence = new BlockSequence();
        Block block = create_block("print", "I love computers!");

        sequence.blocks.Add(block);
        Colors color = get_block_color_id(block.block_name);

        LevelData data = create_level_data(current_level + 1, sequence.blocks);

        add_level_color(color);
        levels[current_level++] = data;
    }

    // Level 3 : print("Make a 360 turn") turn_right(180){2}
    public void set_lvl_3() {
        BlockSequence sequence = new BlockSequence();
        Block block = create_block("print", "Make a 360 turn");

        sequence.blocks.Add(block);
        Colors color = get_block_color_id(block.block_name);
        add_level_color(color);

        // Add two blocks into the sequence
        for(int i = 0; i < levels.Count() - 3; i++) {
            Block b_1 = create_block("turn_right", "180");
            sequence.blocks.Add(b_1);
            color = get_block_color_id(b_1.block_name);
            add_level_color(color);
        }

        LevelData data = create_level_data(current_level + 1, sequence.blocks);

        levels[current_level++] = data;
    }

    // Level 4 : print("I'll move forward four times"); move_forward(i + 1){4}
    public void set_lvl_4() {
        BlockSequence sequence = new BlockSequence();
        Block block_1 = create_block("print", "I'll move forward four times"); 
        
        sequence.blocks.Add(block_1);
        Colors color = get_block_color_id(block_1.block_name);
        add_level_color(color);

        // Add four blocks into the sequence
        for(int i = 0; i < levels.Count() - 1; i++) {
            Block block =  create_block("move_forward", Convert.ToString($"{i + 1}"));
            sequence.blocks.Add(block);
            color = get_block_color_id(block.block_name);
            add_level_color(color);
        }

        LevelData data = create_level_data(current_level + 1, sequence.blocks);
        
        levels[current_level++] = data;
    }    

    // Level 5 : print("Draw a square"); move_forward(50); turn_right(90){4}
    public void set_lvl_5() {
        BlockSequence sequence = new BlockSequence();
        Block block = create_block("print", "draw a square");

        sequence.blocks.Add(block);
        Colors color = get_block_color_id(block.block_name);
        add_level_color(color);

        for(int i = 0; i < levels.Count() - 1; i++) {
            Block b_1 = create_block("move_forward", "50");
            sequence.blocks.Add(b_1);
            color = get_block_color_id(b_1.block_name);
            add_level_color(color);

            Block b_2 = create_block("turn_right", "90");
            sequence.blocks.Add(b_2);
            color = get_block_color_id(b_2.block_name);
            add_level_color(color);
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
            user_color_sequence.Add(get_block_color_id(block.block_name));          // Get the user color code
        }

        json_output = JsonUtility.ToJson(user_sequence, true);      // Needs to be an instance of an object, otherwise JSON doesn't work
        Debug.Log($"Converted to JSON {json_output}");

        user_code_sequence = user_sequence.blocks;      // Get the user's sequence of blocks into this list of blocks.
    }

    // Compare user and predefined code
    public void compare_code (List<Block> user_code) {
        bool check = check_block_count(user_code);          // Check the number of block matches

        // Check if color sequence matches
        if(check) check = check_color_sequences();

        // Check if the block sequence matches
        if(check) check = check_blocks_matches(user_code);

        // Output to display if matches; if it does, level up. 
        output.text = check ? "true" : "false";

        Debug.Log("Predefined color sequence:");
        print_color_sequence(predefined_color_sequence);

        Debug.Log("User color sequence");
        print_color_sequence(user_color_sequence);
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
            predefined_level = levels[current_level];           // Pre-loads the next level onto the current data for this round.       
            user_color_sequence = new List<Colors>();           // Reset the color sequence for the new level
            predefined_color_sequence = levels_color_codes[current_level].colors;
            Debug.Log($" You've passed to level #{current_level + 1}!");    
        }
    }   

    public void add_level_color(Colors color){
        levels_color_codes[current_level].colors.Add(color);
    }

    public bool check_color_sequences() {
        for (int i = 0; i < predefined_color_sequence.Count; i++) {
            if(user_color_sequence[i] != predefined_color_sequence[i])  return false; 
        }

        return true;
    }

    public void print_color_sequence(List<Colors> colors) {
        foreach(Colors color in colors) {
            Debug.Log($"This block is {color}, ID: {(int)color}");
        }
    }

    // Used for both user and programming logic
    public Colors get_block_color_id(string name){    
        switch(name) {
            case "print":
                return Colors.RED;

            case "turn_right":
               return Colors.GREEN;

            case "turn_left":
                return Colors.BLUE;
                
            case "move_forward":
                return Colors.ORANGE;    
                
            case "move_backward":
                return Colors.YELLOW;

            case "for_loop":
                return Colors.INDIGO;

            case "if_then":
                return Colors.VIOLET;
                
            case "if_elif_then":
                return Colors.PINK;

            case "true":
                return Colors.GRAY;

            case "false":
                return Colors.BLACK;
            
            default:  // Number
                return Colors.BROWN;  
        } 
    }
}