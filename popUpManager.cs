using DG.Tweening;
using UnityEngine;

/* 
    Jan Smith - 11536897 
*/

public class popUpManager : MonoBehaviour {
    
    public GameObject popUp_prefab;
    public GameObject canvas_object;

    void Start() {
        create_popUp("Congratulations", "You created a pop up!");
    }

    public void create_popUp(string msg, string description) {
        GameObject pop_up_object = Instantiate(popUp_prefab, canvas_object.transform);
        pop_up_object.GetComponent<popUp>().set_popUp_message(msg);
        pop_up_object.GetComponent<popUp>().set_popUp_description(description);   
        move_popUp(pop_up_object);     
    
    }

    public void move_popUp(GameObject pop_up) {
        pop_up.GetComponent<RectTransform>().DOAnchorPosX(-100, 3f).OnComplete(() => Destroy(pop_up));

    }

}
