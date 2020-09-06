using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadPhoneRecorder : MonoBehaviour
{
    Recorder recorder;
    GameObject ButtonRead;
    GameObject ButtonStop;
    GameObject ButtonErase;
    int nbHeadphones;
    TMPro.TextMeshPro textComponent;
    string eraseText = "Effacer ?";
    string textInit = string.Empty;


    // Start is called before the first frame update
    void Start()
    {
        var recorderObject = this.GetComponentInParent<Transform>();
        ButtonRead = recorderObject.Find(Names.READ).gameObject;
        ButtonStop = recorderObject.Find(Names.STOP).gameObject;
        ButtonErase = recorderObject.Find(Names.ERASE).gameObject;
        recorder = new Recorder(ButtonRead, ButtonStop, ButtonErase);
        string headPhoneName = recorderObject.GetComponentInParent<Transform>()
                                      .gameObject.name;
        nbHeadphones = int.Parse(headPhoneName.Replace(Names.HEADPHONE, string.Empty));
        textComponent = this.GetComponentInChildren<TMPro.TextMeshPro>();
        textInit = textComponent.text;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Equals(Names.MAILLOCHE))
        {
            recorder.DoAction(this.gameObject);
            var name = this.name;
            if (name.Equals(Names.READ))
            {
                PlayHeadPhone();
            }

            if (name.Equals(Names.STOP))
            {
                if (!textComponent.text.Equals(textInit))
                {
                    textComponent.text = textInit;
                }
                CommandManager.Instance.Stop();
            }

            if (name.Equals(Names.ERASE))
            {
                if (textComponent.text.Equals(eraseText))
                {
                    CommandManager.Instance.Delete(nbHeadphones);
                    Destroy(this);
                }
                else
                {
                    SetEraseText();
                }
                
            }

            
        }
    }

    private void PlayHeadPhone()
    {
        CommandManager.Instance.Play(nbHeadphones);
    }

    private void SetEraseText()
    {
        textComponent.text = eraseText;
    }
}
