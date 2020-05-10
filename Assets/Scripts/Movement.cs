using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{


   
    public List<GameObject> Boxes;
    int BoxIndex = 0;
    public Sprite[] Circle;
    bool Turn = false;
    public Text winner;
    public int Score;
    public Text turnTxt;
    public Text CountTxt;
    public TextAsset wordFile;                                // Text file (assigned from Editor)
    private List<string> lineList = new List<string>();


    // Use this for initialization

    void Start()
    {
      
        Boxes.AddRange(GameObject.FindGameObjectsWithTag("Boxes"));
        Boxes.Sort(delegate (GameObject x, GameObject y)
        {
            return string.Compare(x.name, y.name);
        });

    }

    public void ReadWordList()
    {
        // Check if file exists before reading
        if (wordFile)
        {
            string line;
            StringReader textStream = new StringReader(wordFile.text);
           
            while((line = textStream.ReadLine()) != null)
            {
                // Read each line from text file and add into list
                lineList.Add(line);
            }
           
            textStream.Close();
        }
    }
 
    public string GetRandomLine()
    {
        // Returns random line from list
        return lineList[UnityEngine.Random.Range(0, lineList.Count)];
    }
    void GameOver()
    {
        if(Boxes[0].gameObject.name== "cross" && Boxes[1].name== "cross"&& Boxes[2].gameObject.name == "cross")
        {
           
          

        }

       else if (Boxes[3].gameObject.name == "cross" && Boxes[4].name == "cross" && Boxes[5].gameObject.name == "cross")
        {
          
          

        }

       else if (Boxes[6].gameObject.name == "cross" && Boxes[7].name == "cross" && Boxes[8].gameObject.name == "cross")
        {

            

        }

        else if (Boxes[9].gameObject.name == "cross" && Boxes[10].name == "cross" && Boxes[11].gameObject.name == "cross")
        {


        }
        else if (Boxes[10].gameObject.name == "cross" && Boxes[11].name == "cross" && Boxes[12].gameObject.name == "cross")
        {

            

            CheckScore();
        }
    }
 void CheckScore()
    {
        if (Score == 10 ||Score>10)
        {
            winner.text = "Player Won";
        }
        else if (Score < 10)
        {
            winner.text = "Player Lost";

        }
    }
 

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                BoxCollider2D Box = hit.collider.gameObject.GetComponent<BoxCollider2D>();
                Box.enabled = false;



                ReadWordList();
               winner.text= "You found: " + GetRandomLine();


                Turn = !Turn;
                if (Turn == false)
                {
                    turnTxt.text = "Player 1 turn";
                    Score++;

                    if (Score > 11)
                    {
                        CountTxt.text = "Player Wins";
                    }
                    else if (Score == 11)
                    {
                        CountTxt.text = "Player Loses";
                    }
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = Circle[0];
                    GameOver();
                }
                else if (Turn == true)
                {
                    turnTxt.text = "Player 2 turn";
                    Score++;
                    if (Score > 11)
                    {
                        CountTxt.text = "Player Wins";
                    }
                    else if(Score == 11)
                    {
                        CountTxt.text = "Player Loses";
                    }
                    hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = Circle[1];
                    GameOver();
                }

               
            }
        }

       
    }
}













    