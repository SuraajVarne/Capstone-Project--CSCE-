using TMPro;
using UnityEngine;

/* 
    Jan Smith - 11536897 
*/

public class popUp : MonoBehaviour {
    public TMP_Text pop_up_message;
    public TMP_Text pop_up_description;

    public void set_popUp_message(string msg) {
        pop_up_message.text = msg;
    }

} 