# UI Feedback System for Blockly
# Team Members: Suraj Varne Sheela
# Description: Provides visual feedback to users based on Blockly assignment correctness

extends Control

var expected_output = {"task_1": "print('Hello, World!')", "task_2": "x = 5\ny = x * 2\nprint(y)"}

func validate_code(task_id, user_code):
    if task_id in expected_output:
        return user_code.strip_edges() == expected_output[task_id].strip_edges()
    return false

func _on_SubmitButton_pressed():
    var user_code = $CodeInput.text
    var task_id = $TaskSelector.text

    var correct = validate_code(task_id, user_code)

    if correct:
        $FeedbackLabel.text = "✅ Correct!"
        $FeedbackLabel.modulate = Color(0, 1, 0)
    else:
        $FeedbackLabel.text = "❌ Incorrect. Try Again."
        $FeedbackLabel.modulate = Color(1, 0, 0)
