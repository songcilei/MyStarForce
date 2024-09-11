using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//下面的Attribute是为了方便测试，可以让脚本在EditMode下面运行，而无需点击运行程序。
[ExecuteInEditMode]
public class SayHello : MonoBehaviour {
    public string Text;
    public int index;
    // Start is called before the first frame update
    void Start() {
    }
    // Update is called once per frame
    void Update() {
        // Debug.Log(Text);
    }
}