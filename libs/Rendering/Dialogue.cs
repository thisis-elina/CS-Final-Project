using System.Collections.Generic;
using Newtonsoft.Json;

namespace libs
{
    // This class represents the root object that contains a list of dialogues.
    public class RootObject
    {
        // Property representing a list of dialogues. It will be deserialized from the "Dialogues" JSON property.
        [JsonProperty("Dialogues")]
        public List<Dialogue> Dialogues { get; set; }
    }

    // This class represents an individual dialogue.
    public class Dialogue
    {  
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("DialogueText")]
        public string DialogueText { get; set; }

        [JsonProperty("NextDialogID")]
        public int? NextDialogID { get; set; } // This is nullable because there might not always be a next dialogue.

        [JsonProperty("PreivousDialogID")]
        public int? PreivousDialogID { get; set; } // This is nullable because there might not always be a previous dialogue.
    }
}
