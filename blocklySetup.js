// Initialize Blockly workspace with basic settings
function initializeBlockly() {
    const blocklyContainer = document.getElementById('blockly-container');
    const toolboxXML = `
        <xml xmlns="https://developers.google.com/blockly/xml">
            <block type="controls_if"></block>
            <block type="logic_boolean"></block>
        </xml>`;
    
    Blockly.inject(blocklyContainer, {
        toolbox: toolboxXML,
        media: 'https://unpkg.com/blockly/media/'
    });
}

// Initialize Blockly once the window loads
window.onload = function() {
    initializeBlockly();
};
