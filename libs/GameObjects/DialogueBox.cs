using System;
using System.Collections.Generic;
using System.Linq;

namespace libs
{
    public class DialogueBox
    {
        private static DialogueBox instance;
        public static DialogueBox Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DialogueBox();
                }
                return instance;
            }
        }

        private DialogueBox()
        {
            GenerateDialogue(0); // Default to the first dialogue
        }

        private int sizeX = 40;
        char boxBorder = '=';
        private string _title;
        private string _dialogueText;
        private int _currentId;

        public void GenerateDialogue(int id)
        {
            // Fetch the dialogue by ID
            Dialogue currentDialogue = GetDialogue(id);
            _currentId = id;
            _title = currentDialogue.Title;
            _dialogueText = currentDialogue.DialogueText;
        }

        public void DrawBorder()
        {
            Console.WriteLine();
            for (int i = 0; i <= this.sizeX; i++)
            {
                Console.Write(this.boxBorder);
            }
            Console.WriteLine();
        }

        public Dialogue GetDialogue(int id)
        {
            List<Dialogue> dialogues = FileHandler.LoadDialogue().Dialogues;
            return dialogues.FirstOrDefault(x => x.Id == id);
        }

        public void RenderDialogue()
        {
            DrawBorder();
            Console.WriteLine($"Title: {_title}\n");

            string[] words = _dialogueText.Split(' ');
            string line = "";

            foreach (string word in words)
            {
                if (line.Length + word.Length + 1 > sizeX)
                {
                    Console.WriteLine(line);
                    line = "";
                }

                line += (line.Length == 0) ? word : " " + word;
            }

            if (line.Length > 0)
            {
                Console.WriteLine(line);
            }

            DrawBorder();
        }

        public bool ProceedDialogue()
        {
            Dialogue currentDialogue = GetDialogue(_currentId);
            if (currentDialogue.NextDialogID.HasValue)
            {
                GenerateDialogue(currentDialogue.NextDialogID.Value);
                return true;
            }
            return false;
        }
    }
}