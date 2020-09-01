using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StackOverflowProject.ViewModels
{
    public class NewAnswerViewModel
    {
        
        
        public string AnswerText { get; set; }

        public DateTime AnswerDateAndTime { get; set; }

        public int UserID { get; set; }

    
        public int QuestionID { get; set; }

        
        public int VotesCount { get; set; }
    }
}
