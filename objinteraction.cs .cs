# VR Object Interaction Script
# Team Members: Suraj Varne Sheela
# Description: Implements grabbing and releasing objects in VR

extends Spatial

var grabbed_object = null
var grabbing = false

func _process(delta):
    if grabbing and grabbed_object:
        grabbed_object.global_transform.origin = $VRController.global_transform.origin

func _input(event):
    if event is InputEventJoypadButton:
        if event.pressed:
            grabbing = true
            var collider = $VRController.get_collider()
            if collider and collider.has_method("set_owner"):
                grabbed_object = collider
                grabbed_object.set_owner(self)
        else:
            grabbing = false
            if grabbed_object:
                grabbed_object.set_owner(null)
                grabbed_object = null
