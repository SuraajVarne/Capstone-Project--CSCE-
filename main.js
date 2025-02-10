// Event listeners for level selection buttons
document.getElementById('level1-btn').addEventListener('click', () => loadBlocklyLevel(1));
document.getElementById('level2-btn').addEventListener('click', () => loadBlocklyLevel(2));
document.getElementById('level3-btn').addEventListener('click', () => loadBlocklyLevel(3));

// Function to load the selected level and initialize the Blockly workspace
function loadBlocklyLevel(level) {
    const blocklyContainer = document.getElementById('blockly-container');
    blocklyContainer.innerHTML = '';  // Clear the container to load new Blockly workspace

    // Display a message depending on the selected level
    if (level === 1) {
        alert("Loading Beginner Level...");
        // Initialize the Blockly workspace with beginner-level blocks
        initializeBlocklyWorkspace(level);
    } else if (level === 2) {
        alert("Loading Intermediate Level...");
        // Initialize the Blockly workspace with intermediate-level blocks
        initializeBlocklyWorkspace(level);
    } else if (level === 3) {
        alert("Loading Advanced Level...");
        // Initialize the Blockly workspace with advanced-level blocks
        initializeBlocklyWorkspace(level);
    }
}

// Function to initialize the Blockly workspace with appropriate blocks based on the level
function initializeBlocklyWorkspace(level) {
    const blocklyContainer = document.getElementById('blockly-container');
    
    // Example: Set up different toolboxes for each level
    const toolboxXML = getToolboxXML(level);

    // Inject Blockly workspace with the selected toolbox
    const workspace = Blockly.inject(blocklyContainer, {
        toolbox: toolboxXML,
    });
}

// Function to return different toolbox XML based on the selected level
function getToolboxXML(level) {
    let toolboxXML = '';
    
    if (level === 1) {
        toolboxXML = `
        <xml xmlns="https://developers.google.com/blockly/xml">
            <block type="controls_if"></block>
            <block type="logic_boolean"></block>
        </xml>`;
    } else if (level === 2) {
        toolboxXML = `
        <xml xmlns="https://developers.google.com/blockly/xml">
            <block type="controls_if"></block>
            <block type="logic_compare"></block>
            <block type="math_number"></block>
        </xml>`;
    } else if (level === 3) {
        toolboxXML = `
        <xml xmlns="https://developers.google.com/blockly/xml">
            <block type="controls_if"></block>
            <block type="math_arithmetic"></block>
            <block type="variables_set"></block>
        </xml>`;
    }

    return toolboxXML;
}

// Function to save the user's workspace and print the generated code
function saveWorkspace() {
    const xml = Blockly.Xml.workspaceToXml(Blockly.getMainWorkspace());
    const xmlText = Blockly.Xml.domToText(xml);
    console.log("Generated Blockly Code: ", xmlText);
    // You can save this code to a file or submit it based on your project needs
}
