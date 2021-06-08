using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class HandleTextFile : MonoBehaviour
{

    [SerializeField] public List<QuizDataScriptable> quizDataList;
    private List<Question> questions;
    private Question selectedQuetion = new Question();
    private QuizDataScriptable dataScriptable;

    public List<QuizDataScriptable> QuizData { get => quizDataList; }


    public  TextAsset asset;


    public string[] lines;
    public void Start(){
        questions = new List<Question>();
        dataScriptable = quizDataList[0];
        questions.AddRange(dataScriptable.questions);
        WriteString();
        
    }
    void WriteString()
    {
        string path = "Assets/Resources/test.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        for(int i = 0; i < 80; i++){
            selectedQuetion = questions[i];
            writer.Write("Question"+(i+1)+ " = " + selectedQuetion.questionInfo + "\n");
            writer.Write("Option1 = " + selectedQuetion.options[0] + "\n");
            writer.Write("Option2 = " + selectedQuetion.options[1] + "\n");
            writer.Write("Option3 = " + selectedQuetion.options[2] + "\n");
            writer.Write("Option5 = " + selectedQuetion.options[3] + "\n");
            writer.Write("Answer = " + selectedQuetion.correctAns + "\n\n\n");
        }
        
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path); 

        //Print the text from the file
        Debug.Log(asset.text);
    }

    [MenuItem("Tools/Read file")]
    void ReadString()
    {
        string path = "Assets/Resources/test.txt";
        string text;
        
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        text = reader.ReadToEnd();
        lines = text.Split('\n');
        Debug.Log(text);


        int j = 0;
        for(int i = 60; i < 80; i++){
            questions[i].questionInfo = lines[j];
            questions[i].options[0] = lines[j+1];
            questions[i].options[1]= lines[j+2];
            questions[i].options[2] = lines[j+3];
            questions[i].options[3] = lines[j+4];
            if( lines[j+6] == "Answer: A"){
                questions[i].correctAns = lines[j+1];
            }
            else if( lines[j+6] == "Answer: B"){
                questions[i].correctAns = lines[j+2];
            }
            else if( lines[j+6] == "Answer: C"){
                questions[i].correctAns = lines[j+3];
            }
            else if( lines[j+6] == "Answer: D"){
                questions[i].correctAns = lines[j+4];
            }
            j =  j + 8;
            
        }
        
        reader.Close();
    }

}

