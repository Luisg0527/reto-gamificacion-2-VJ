using UnityEngine;

public class selectPreguntaMariposa : MonoBehaviour
{
    public void ChoosePregunta1(){
        MariposaGameControl.Instance.selectPregunta(1);
    }

    public void ChoosePregunta2(){
        MariposaGameControl.Instance.selectPregunta(2);
    }

    public void ChoosePregunta3(){
        MariposaGameControl.Instance.selectPregunta(3);
    }
}
