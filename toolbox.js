'use strict';

let workspace = null;
const toolbox = {
    // There are two kinds of toolboxes. The simpler one is a flyout toolbox.
    kind: 'categoryToolbox',
    // The contents is the blocks and other items that exist in your toolbox.
    contents: [
        { // Logic Category
            kind: 'category',
            name: 'Logic',
            categorystyle: 'logic_category',
            contents: [
              { // Controls Subcategory
                kind: 'category',
                name: 'Controls',
                categorystyle: 'logic_category',
                contents: [
                  { // If Statements
                      kind: 'block',
                      type: 'controls_if',
                  },
                  { // If - Else
                      kind: 'block',
                      type: 'controls_if',
                      extraState: {
                        hasElse: 'true',
                      },
                  },
                  { // If - Else If - Else
                    kind: 'block',
                    type: 'controls_if',
                    extraState: {
                      hasElse: 'true',
                      elseIfCount: 1,
                    },
                  },
                ],
              },
              { // Boolean Subcategory
                kind: 'category',
                name: 'Boolean',
                categorystyle: 'logic_category',
                contents: [
                  { //Compare Logic (=, >, <, >=, <=)
                    kind: 'block',
                    type: 'logic_compare',
                  },
                  { //Logical Operation (AND, OR, XOR, NOT)
                      kind: 'block',
                      type: 'logic_operation',
                  },
                  {
                    kind: 'block',
                    type: 'logic_negate',
                  },
                  { // Boolean Logic (TRUE or FALSE)
                      kind: 'block',    
                      type: 'logic_boolean',
                  },
                  { // Null Value
                      kind: 'block',
                      type: 'logic_null',
                  },
                  { //
                      kind: 'block',
                      type: 'logic_ternary',
                  },
                ],
              },               
            ]
        }, 
        { // Loops Category
            kind: 'category',
            name: 'Loops', 
            categorystyle: 'loop_category',
            contents: [
                {
                    kind: 'block',
                    type: 'controls_repeat_ext',
                    inputs: {
                      TIMES: {
                        shadow: {
                          type: 'math_number',
                          fields: {
                            NUM: 10,
                          },
                        },
                      },
                    },
                },
                { // While Loop
                    kind: 'block',
                    type: 'controls_whileUntil',
                },
                { // For Loop
                    kind: 'block',
                    type: 'controls_for',
                    fields: {
                        VAR: 'i',
                    },
                    inputs: {
                        FROM: {
                          shadow: {
                            type: 'math_number',
                            fields: {
                              NUM: 1,
                            },
                          },
                        },
                        TO: {
                          shadow: {
                            type: 'math_number',
                            fields: {
                              NUM: 10,
                            },
                          },
                        },
                        BY: {
                          shadow: {
                            type: 'math_number',
                            fields: {
                              NUM: 1,
                            },
                          },
                        },
                    },
                },
                {
                    kind: 'block',
                    type: 'controls_forEach',
                },
                {
                    kind: 'block',
                    type: 'controls_flow_statements',
                },
            ]                    
        },       
        { // Math Category  
            kind: 'category',
            name: 'Math', 
            categorystyle: 'math_category',
            contents: [  
                { // Math Number
                  kind: 'block',
                  type: 'math_number',
                  fields: {
                    NUM: 123,
                  },
                },
                {
                  kind: 'block',
                  type: 'math_constant',
                  fields: {
                    CONSTANT: 'PI',
                  },
                },
                {
                  kind: 'block',
                  type: 'math_trig',
                  fields: {
                    OP: 'SIN',
                  },                    
                },
                {
                  kind: 'block',
                  type: 'math_round',
                  fields: {
                    OP: 'ROUND',
                  },
                },
                { // Arithmetic Operations (Add, Subtract, Divide, Multiply)
                  kind: 'block',
                  type: 'math_arithmetic',
                  fields: {
                    OP: 'ADD',
                  },
                },
                
                { 
                  kind: 'block',
                  type: 'math_arithmetic',
                  inputs: {
                      A: {
                        shadow: {
                          type: 'math_number',
                          fields: {
                            NUM: 1,
                          },
                        },                        },
                      B: {
                        shadow: {
                          type: 'math_number',
                          fields: {
                            NUM: 1,
                          },
                        },
                      },
                   },
                },
                {
                  kind: 'block',
                  type: 'math_single',
                  fields: {
                    OP: 'ROOT',
                  },
                }, 
                {
                  kind: 'block',
                  type: 'math_number_property',
                  extraState: '<mutation divisor_input="false"></mutation>',
                  fields: {
                    PROPERTY: 'EVEN',
                  },
                },
                {
                  kind: 'block',
                  type: 'math_on_list',
                  extraState: '<mutation op="SUM"></mutation>',
                  fields: {
                    OP: 'SUM',
                  },
                },
                {
                  kind: 'block',
                  type: 'math_random_float',
                },
                {
                  kind: 'block',
                  type: 'math_atan2'
                },
                {
                  kind: 'block',
                  type: 'math_modulo',
                },
                {
                  kind: 'block',
                  type: 'math_random_int',
                  inputs: {
                      FROM: {
                          block: {
                              type: 'math_number',
                              fields: {
                                  NUM: 1,
                              },
                          },
                      },
                      TO: {
                          block: {
                              type: 'math_number',
                              fields: {
                                  NUM: 100,
                              },
                          },
                      },
                  },
                },
                 
            ]
        }, 
        { // Variable Category
            kind: 'category',
            name: 'Variables',
            categorystyle: 'variable_category',
            custom: 'VARIABLE',
        },
        { // Functions Category
            kind: 'category',
            name: 'Functions',
            categorystyle: 'procedure_category',
            custom: 'PROCEDURE',
        },
        { // Category Separator
            kind: 'sep',
        },
        { // Move Category
            kind: 'category',
            name: 'Custom',
            contents: [  
                { // Move Forward 
                    kind: 'block',
                    type: 'move_forward'
                },
                { // Move Backwards
                    kind: 'block',
                    type: 'move_backwards'
                },
                { // Turn right
                    kind: 'block',
                    type: 'turn_right'
                },
                {// Turn Left
                    kind: 'block',
                    type: 'turn_left'
                },
                {// Move in an angle
                    kind: 'block',
                    type: 'move_in_angle'
                }
            ] 
        },
    ]// Contents           
  };

function start() {
    // Create main workspace
    workspace = Blockly.inject(document.getElementById('blocklyDiv'), {
        toolbox: toolbox,
        scrollbars: true,
    });
}

console.log(Blockly.serialization.workspaces.save(Blockly.getMainWorkspace()));