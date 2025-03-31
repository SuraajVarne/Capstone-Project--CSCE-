# Blockly Code Execution Validator
# Team Members: Suraj Varne Sheela
# Description: Compares user-generated Blockly code with correct solutions

from flask import Flask, request, jsonify

app = Flask(__name__)

EXPECTED_OUTPUT = {"task_1": "print('Hello, World!')", "task_2": "x = 5\ny = x * 2\nprint(y)"}

@app.route('/validate', methods=['POST'])
def validate():
    data = request.json
    task_id = data.get("task_id")
    user_code = data.get("user_code")

    if task_id in EXPECTED_OUTPUT:
        correct_code = EXPECTED_OUTPUT[task_id]
        is_correct = user_code.strip() == correct_code.strip()
        return jsonify({"correct": is_correct, "expected": correct_code, "submitted": user_code})

    return jsonify({"error": "Invalid task ID"}), 400

if __name__ == '__main__':
    app.run(debug=True)
