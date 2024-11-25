// Import Blockly core.
import * as Blockly from 'blockly/core';
// Import the default blocks.
import * as libraryBlocks from 'blockly/blocks';
// Import a generator.
import {javascriptGenerator} from 'blockly/javascript';
// Import a message file.
import * as En from 'blockly/msg/en';

Blockly.setLocale(En);

// Toolbox
var toolbox = {
    // There are two kinds of toolboxes. The simpler one is a flyout toolbox.
    kind: 'categoryToolbox',
    // The contents is the blocks and other items that exist in your toolbox.
    contents: [
        { // Control Category
            "kind": "category",
            "name": "Control",
            "contents": [
                { // If Statements
                    "kind": "block",
                    "type": "controls_if"
                },
                { // Switch Statement
                    "kind": "block",
                    "type": "controls_switch"
                }
            ]
        }, // Control
        { // Logic Category
            "kind": "category",
            "name": "Logic",
            "contents": [
                { //Compare Logic (=, >, <, >=, <=)
                    "kind": "block",
                    "type": "logic_compare"
                },
                { //Logical Operation (AND, OR, XOR, NOT)
                    "kind": "block",
                    "type": "logic_operation"
                },
                { // Boolean Logic (TRUE or FALSE)
                    "kind": "block",    
                    "type": "logic_boolean"
                }
            ]
        }, // Logic
        { // Loops Category
            "kind": "category",
            "name": "Loops", 
            "contents": [
                { // While Loop
                    "kind": "block",
                    "type": "controls_whileUntil"
                },
                { // For Loop
                    "kind": "block",
                    "type": "controls_for"
                }
            ]                    
        }, //Loops 
        { // Move Category
            "kind": "category",
            "name": "Custom",
            "content": [  
                { // Move Forward 
                    "kind": "block",
                    "type": "move_forward"
                },
                { // Move Backwards
                    "kind": "block",
                    "type": "move_backwards"
                },
                { // Turn right
                    "kind": "block",
                    "type": "turn_right"
                },
                {// Turn Left
                    "kind": "block",
                    "type": "turn_left"
                },
                {// Move in an angle
                    "kind": "block",
                    "type": "move_in_angle"
                }
            ] 
        }, // Move Category
        { // Math Category
            "kind": "category",
            "name": "math", 
            "content": [ 
                {// Arithmetic Operations (Add, Subtract, Divide, Multiply)
                    "kind": "block",
                    "type": "math_arithmetic"
                    // Develop this block to accept the different arithmetic operations
                }
            ]
        }, // Math
        { // Functions
            "kind": "category", 
            "name": "functions",
            "content": [
                { // Function Category
                    /* This section is to develop unique functions, 
                    just as with any programming language */
                }
            ]

        } // Functions
    ]           
  };
  
  // The toolbox gets passed to the configuration struct during injection.
  const workspace = Blockly.inject('blocklyDiv', {toolbox: toolbox});
  
