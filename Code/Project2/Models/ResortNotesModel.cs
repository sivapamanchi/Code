using System.Collections.Generic;

namespace BGModern.Models
{
    public class ResortNotesModel : MasterModel
    {
        public List<ResortNoteModel> Notes { get; set; }

        public ResortNotesModel()
        {
            Notes = new List<ResortNoteModel>();
        }
    }
}