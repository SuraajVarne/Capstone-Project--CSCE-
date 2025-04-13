using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;

/*
    Only the script is included. 
    The Unity textfield and files are not included

    Jan Smith - 11536897
*/

public class LevelManager : MonoBehaviour {

    public enum Custom_Colors {   // Block's Color Code 
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

    // Used for object instances purposes
    [System.Serializable]
    public class ColorSequence {
        public List<Custom_Colors> predefined_colors = new List<Custom_Colors>();
    }
    // This is the level data
    [System.Serializable]
    public class LevelData {
        public int level;
        public List<Block> predefined_code = new List<Block>();
    }
   
    // Unity Variables
    public TextMeshProUGUI output;                  // Output to the screen
    public TMP_InputField user_input;
   
    // Global Variables
    string json_output;                             // The actual user's code in JSON format
    private bool next_level;                        // Check if it is valid to level up
    public int current_level;                       // Index of the current level
    private LevelData[] levels;                     // Levels, 5 levels 
    private LevelData predefined_level;             // Used to access the current level
    private List<Block> user_code_sequence;         // User's JSON Blocks
    private ColorSequence[] levels_color_codes; 
    private ColorSequence predefined_color_sequence;
    private List<Custom_Colors> user_color_sequence;

    void Start() {
        current_level = 0;              // Use current level to traverse the initialization process, reset later
        next_level = false;
        levels = new LevelData[5];
        levels_color_codes = new ColorSequence[5];
        
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
        
        // Reset current level and preload current level definition
        current_level = 0;
        predefined_level = levels[current_level];
        predefined_color_sequence = levels_color_codes[current_level];
    }

    // Level 1 : print("Hello World")
    public void set_lvl_1() {
        BlockSequence block_sequence = create_block_sequence();
        Block block = create_block("print", "Hello World");

        ColorSequence color_sequence = create_color_sequence();
        Custom_Colors color = get_block_color_id(block.block_name);
       
        add_block_to_sequence(block_sequence, block);        
        add_color_to_sequence(color_sequence, color);
        
        // Load all the data for this level here
        LevelData level_data = create_level_data(current_level + 1, block_sequence.blocks);

        add_level_color(color_sequence);        
        levels[current_level++] = level_data;
    }

    // Level 2 : print("I love omputers!")
    public void set_lvl_2() {
        BlockSequence block_sequence = create_block_sequence();
        Block block = create_block("print", "I love computers!");
        
        ColorSequence color_sequence = create_color_sequence();
        Custom_Colors color = get_block_color_id(block.block_name);
          
        add_block_to_sequence(block_sequence, block);
        add_color_to_sequence(color_sequence, color);

        LevelData level_data = create_level_data(current_level + 1, block_sequence.blocks);

        add_level_color(color_sequence);
        levels[current_level++] = level_data;
    }

    // Level 3 : print("Make a 360 turn") turn_right(180){2}
    public void set_lvl_3() {
        BlockSequence block_sequence = create_block_sequence();
        Block block = create_block("print", "Make a 360 turn");
        
        ColorSequence color_sequence = create_color_sequence();
        Custom_Colors color = get_block_color_id(block.block_name);
        
        add_block_to_sequence(block_sequence, block);
        add_color_to_sequence(color_sequence, color);

        // Add two blocks into the sequence
        for(int i = 0; i < levels.Count() - 3; i++) {
            Block b_1 = create_block("turn_right", "180");
            color = get_block_color_id(b_1.block_name);
        
            add_block_to_sequence(block_sequence, b_1);    
            add_color_to_sequence(color_sequence, color);
        }

        LevelData level_data = create_level_data(current_level + 1, block_sequence.blocks);

        add_level_color(color_sequence);
        levels[current_level++] = level_data;
    }

    // Level 4 : print("I'll move forward four times"); move_forward(i + 1){4}
    public void set_lvl_4() {
        BlockSequence block_sequence = create_block_sequence();
        Block block_1 = create_block("print", "I'll move forward four times");  
        
        ColorSequence color_sequence = create_color_sequence();
        Custom_Colors color = get_block_color_id(block_1.block_name);
        
        add_block_to_sequence(block_sequence, block_1);
        add_color_to_sequence(color_sequence, color);

        // Add four blocks into the sequence
        for(int i = 0; i < levels.Count() - 1; i++) {
            Block block =  create_block("move_forward", Convert.ToString($"{i + 1}"));
            color = get_block_color_id(block.block_name);

            add_block_to_sequence(block_sequence, block);
            add_color_to_sequence(color_sequence, color);
        }

        LevelData level_data = create_level_data(current_level + 1, block_sequence.blocks);
        
        add_level_color(color_sequence);
        levels[current_level++] = level_data;
    }    

    // Level 5 : print("Draw a square"); move_forward(50); turn_right(90){4}
    public void set_lvl_5() {
        BlockSequence block_sequence = create_block_sequence();
        Block block = create_block("print", "draw a square");
        
        ColorSequence color_sequence = create_color_sequence();
        Custom_Colors color = get_block_color_id(block.block_name);
        
        add_block_to_sequence(block_sequence, block);
        add_color_to_sequence(color_sequence, color);

        for(int i = 0; i < levels.Count() - 1; i++) {
            Block b_1 = create_block("move_forward", "50");
            color = get_block_color_id(b_1.block_name);
            
            add_block_to_sequence(block_sequence, b_1);
            add_color_to_sequence(color_sequence, color);

            b_1 = create_block("turn_right", "90");
            color = get_block_color_id(b_1.block_name);

            add_block_to_sequence(block_sequence, b_1);
            add_color_to_sequence(color_sequence, color);
        }

        LevelData level_data = create_level_data(current_level + 1, block_sequence.blocks);

        add_level_color(color_sequence);
        levels[current_level++] = level_data;
    }

    // Parse function 
    public void convert_txt_to_json () {
        string input_txt = user_input.text;
        MatchCollection matches = Regex.Matches(input_txt, @"(\w+)\((.*?)\)");      // Parse user input  
        BlockSequence block_sequence = create_block_sequence();   
        ColorSequence color_sequence = create_color_sequence();

        foreach (Match match in matches) {
            Block block = create_block(match.Groups[1].Value.Trim(), match.Groups[2].Value.Trim()) ;
            Custom_Colors color = get_block_color_id(block.block_name);

            add_block_to_sequence(block_sequence, block);            // Get the user's blocks of code
            add_color_to_sequence(color_sequence, color);           // Get the user's color code
        }

        json_output = JsonUtility.ToJson(block_sequence, true);      // Needs to be an instance of an object, otherwise JSON doesn't work
        Debug.Log($"Converted to JSON {json_output}");

        user_code_sequence = block_sequence.blocks;                  // Get the user's sequence of blocks into this list of blocks.
        user_color_sequence = color_sequence.predefined_colors;     // Get the color sequence
    }

    // Compare user and predefined code
    public void compare_code (List<Block> user_code) {
        bool check = check_block_count(user_code);              // Check the number of block matches      

        if(check) check = check_color_matches();                // Check if color sequence matches

        if(check) check = check_blocks_matches(user_code);      // Check if the block sequence matches
 
        output.text = check ? "true" : "false";                 // Output to display if matches

        Debug.Log("Predefined color sequence:");
        print_color_sequence(predefined_color_sequence.predefined_colors);

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

    public bool check_color_matches() {
        for (int i = 0; i < predefined_color_sequence.predefined_colors.Count; i++) {
            if(user_color_sequence[i] != predefined_color_sequence.predefined_colors[i])  return false; 
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

    public BlockSequence create_block_sequence() {
        return new BlockSequence();
    }

    public ColorSequence create_color_sequence() {
        return new ColorSequence();
    }  

    public void add_level_color(ColorSequence color) {
        levels_color_codes[current_level] = color;
    }

    public void add_block_to_sequence(BlockSequence sequence, Block block) {
        sequence.blocks.Add(block);
    }

    public void add_color_to_sequence(ColorSequence sequence, Custom_Colors color) {
        sequence.predefined_colors.Add(color);
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
            predefined_level = levels[current_level];                   // Pre-loads the next level onto the current data for this round.       
            user_color_sequence = new List<Custom_Colors>();            // Reset the color sequence for the new level
            predefined_color_sequence = levels_color_codes[current_level];
            Debug.Log($" You've passed to level #{current_level + 1}!");    
        }
    } 

    // Used for both user and programming logic
    public Custom_Colors get_block_color_id(string name) {    
        switch(name) {
            case "print":
                return Custom_Colors.RED;

            case "turn_right":
               return Custom_Colors.GREEN;

            case "turn_left":
                return Custom_Colors.BLUE;
                
            case "move_forward":
                return Custom_Colors.ORANGE;    
                
            case "move_backward":
                return Custom_Colors.YELLOW;

            case "for_loop":
                return Custom_Colors.INDIGO;

            case "if_then":
                return Custom_Colors.VIOLET;
                
            case "if_elif_then":
                return Custom_Colors.PINK;

            case "true":
                return Custom_Colors.GRAY;

            case "false":
                return Custom_Colors.BLACK;
            
            default:  // Number
                return Custom_Colors.BROWN;  
        } 
    }

    public void print_color_sequence(List<Custom_Colors> colors) {
        foreach(Custom_Colors color in colors) {
            Debug.Log($"This block is {color}, ID: {(int)color}");
        }
    }
}