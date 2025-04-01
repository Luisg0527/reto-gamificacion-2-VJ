using UnityEngine;

public class selectPreguntaMariposa : MonoBehaviour
{
    public void ChoosePregunta1(){
        MariposaGameControl.Instance.Select(1);
    }

    public void ChoosePregunta2(){
        MariposaGameControl.Instance.Select(2);
    }

    public void ChoosePregunta3(){
        MariposaGameControl.Instance.Select(3);
    }
}
