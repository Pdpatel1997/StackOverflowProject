using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModels;

namespace StackOverflowProject.Repositories
{
    public interface IQuestionsRepository
    {
        void InsertQuestion(Question q);
        void UpdateQuestionDetails(Question q);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid,int value);
        void DeleteQuestion(int qid);
        List<Question> GetQuestions();
        List<Question> GetQuestionByQuestionID(int qid);


        

    }
    public class QuestionsRepository : IQuestionsRepository
    {
        StackOverflowDatabaseDbContext db;
        public QuestionsRepository()
        {
            db = new StackOverflowDatabaseDbContext();
        }

        public void InsertQuestion(Question q)
        {
            db.Questions.Add(q);
            db.SaveChanges();
        }

        public void UpdateQuestionDetails(Question q)
        {
            Question que = db.Questions.Where(s => s.QuestionID == q.QuestionID).FirstOrDefault();
            if (que != null)
            {
                que.QuestionName = q.QuestionName;
                que.QuestionDateAndTime = q.QuestionDateAndTime;
                que.CategoryID = q.CategoryID;
                db.SaveChanges();
            }
        }
        public void UpdateQuestionVotesCount(int qid, int value)
        {
            Question q = db.Questions.Where(s => s.QuestionID == qid).FirstOrDefault();
            if (q != null) 
            {
                q.VotesCount += value;
                db.SaveChanges();
            }
        }
        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            Question q = db.Questions.Where(s => s.QuestionID == qid).FirstOrDefault();
            if (q != null)
            {
                q.AnswersCount += value;
                db.SaveChanges();
            }
        }
        public void UpdateQuestionViewsCount(int qid,int value)
        {
            Question q = db.Questions.Where(s => s.QuestionID == qid).FirstOrDefault();
            if (q != null)
            {
                q.ViewsCount += value;
                db.SaveChanges();
            }
        }

        public void DeleteQuestion(int qid)
        {
            Question q = db.Questions.Where(s => s.QuestionID == qid).FirstOrDefault();
            db.Questions.Remove(q);
        }
        public List<Question> GetQuestions()
        {
            List<Question> questions = db.Questions.ToList();
            return questions;
        }
        public List<Question> GetQuestionByQuestionID(int qid)
        {
            List<Question> questions = db.Questions.Where(s => s.QuestionID == qid).ToList();
            return questions;
        }
    }
}
