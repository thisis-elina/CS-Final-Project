using System.Collections.Generic;
using Newtonsoft.Json;

namespace libs
{
    public class RootObject
    {
        [JsonProperty("Dialogues")]
        public List<Dialogue> Dialogues { get; set; }
    }

    public class Dialogue
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("DialogueText")]
        public string DialogueText { get; set; }

        [JsonProperty("NextDialogID")]
        public int? NextDialogID { get; set; }

        [JsonProperty("PreivousDialogID")]
        public int? PreivousDialogID { get; set; }
    }
}
