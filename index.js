'use strict';

// Import Blockly core.
import * as Blockly from 'blockly/core';
// Import the default blocks.
// import * as libraryBlocks from 'blockly/blocks';
// // Import a generator.
// import {javascriptGenerator} from 'blockly/javascript';
// // Import a message file.
// import * as En from 'blockly/msg/en';

const toolbox = {
    // There are two kinds of toolboxes. The simpler one is a flyout toolbox.
    "kind": "categoryToolbox",
    // The contents is the blocks and other items that exist in your toolbox.
    "contents": [
        { // Logic Category
            "kind": "category",
            "name": "Logic",
            "categorystyle": "logic_category",
            "contents": [
                { // If Statements
                    "kind": "block",
                    "type": "controls_if",
                },
                { // Switch Statement
                    "kind": "block",
                    "type": "controls_switch",
                },
                { //Compare Logic (=, >, <, >=, <=)
                    "kind": "block",
                    "type": "logic_compare",
                },
                { //Logical Operation (AND, OR, XOR, NOT)
                    "kind": "block",
                    "type": "logic_operation",
                },
                { // Boolean Logic (TRUE or FALSE)
                    "kind": "block",    
                    "type": "logic_boolean",
                },
                { // Null Value
                    "kind": 'block',
                    "type": 'logic_null',
                },
                { //
                    "kind": 'block',
                    "type": 'logic_ternary',
                },
            ]
        }, 
        { // Loops Category
            "kind": "category",
            "name": "Loops", 
            "categorystyle": "loop_category",
            "contents": [
                {
                    "kind": "block",
                    "type": "controls_repeat_ext",
                    "inputs": {
                      "TIMES": {
                        "shadow": {
                          "type": "math_number",
                          "fields": {
                            "NUM": 10,
                          },
                        },
                      },
                    },
                },
                { // While Loop
                    "kind": "block",
                    "type": "controls_whileUntil",
                },
                { // For Loop
                    "kind": "block",
                    "type": "controls_for",
                    "inputs": {
                        "FROM": {
                          "shadow": {
                            "type": "math_number",
                            "fields": {
                              "NUM": 1,
                            },
                          },
                        },
                        "TO": {
                          "shadow": {
                            "type": "math_number",
                            "fields": {
                              "NUM": 10,
                            },
                          },
                        },
                        "BY": {
                          "shadow": {
                            "type": "math_number",
                            "fields": {
                              "NUM": 1,
                            },
                          },
                        },
                    },
                },
                {
                    "kind": "block",
                    "type": "controls_forEach",
                },
                {
                    "kind": "block",
                    "type": "controls_flow_statements",
                },
            ]                    
        }, 
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
        }, 
        { // Math Category
            "kind": "category",
            "name": "math", 
            "content": [ 
                {// Arithmetic Operations (Add, Subtract, Divide, Multiply)
                    "kind": "block",
                    "type": "math_arithmetic",
                    "inputs": {
                        "A": {
                          "shadow": {
                            "type": "math_number",
                            "fields": {
                              "NUM": 1,
                            },
                          },
                        },
                        "B": {
                          "shadow": {
                            "type": "math_number",
                            "fields": {
                              "NUM": 1,
                            },
                          },
                        },
                    },
                }, // Arithmetic Operations
                { // Math Number
                    "kind": "block",
                    "type": "math_number",
                    "fields": {
                      "NUM": 123,
                    },
                }, 
                {// Math Trigonometry
                    "kind": 'block',
                    "type": 'math_trig',
                    "inputs": {
                      "NUM": {
                        "shadow": {
                          "type": 'math_number',
                          "fields": {
                            "NUM": 45,
                          },
                        },
                      },
                    },
                  },
                  {
                    "kind": "block",
                    "type": "math_constant",
                  },
                  {
                    "kind": "block",
                    "type": "math_modulo",
                    "inputs": {
                      "DIVIDEND": {
                        "shadow": {
                          "type": "math_number",
                          "fields": {
                            "NUM": 64,
                          },
                        },
                      },
                      "DIVISOR": {
                        "shadow": {
                          "type": "math_number",
                          "fields": {
                            "NUM": 10,
                          },
                        },
                      },
                    },
                  },
            ]
        }, 
        { // Category Separator
            "kind": "sep",
        },
        { // Variable Category
            "kind": "category",
            "name": "Variables",
            "categorystyle": "variable_category",
            "custom": "VARIABLE",
        },
        { // Functions Category
            "kind": "category",
            "name": "Functions",
            "categorystyle": "procedure_category",
            "custom": "PROCEDURE",
        },
    ]// Contents           
  };
  
  // Inject the toolbox into the workspace
  const workspace = Blockly.inject(
    document.getElementById('blocklyDiv'), {
    toolbox: toolbox,
    scrollbars: false,
    horizontallayout: true,
    toolboxPosition: "end",
  });
 
  console.log(Blockly.serialization.workspaces.save(Blockly.getMainWorkspace()));

