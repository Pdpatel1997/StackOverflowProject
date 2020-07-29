using System;
using System.Collections.Generic;
using System.Linq;
using StackOverflowProject.DomainModels;
using StackOverflowProject.Repositories;
using StackOverflowProject.ViewModels;
using AutoMapper;
using AutoMapper.Configuration;
using System.CodeDom.Compiler;

namespace StackOverflowProject.ServiceLayer
{
    public interface IQuestionsService
    {
        void InsertQuestion(NewQuestionViewModel qvm);
        void UpdateQuestionDetails(EditQuestionViewModel q);
        void UpdateQuestionVotesCount(int qid, int value);
        void UpdateQuestionAnswersCount(int qid, int value);
        void UpdateQuestionViewsCount(int qid, int value);
        void DeleteQuestion(int qid);
        List<QuestionViewModel> GetQuestions();
        QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID);
    }
    public class QuestionsService: IQuestionsService
    {
        IQuestionsRepository qr;

        public QuestionsService()
        {
            qr = new QuestionsRepository();
        }

        public void InsertQuestion(NewQuestionViewModel qvm)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<NewQuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<NewQuestionViewModel, Question>(qvm);
            qr.InsertQuestion(q);
        }
       public  void UpdateQuestionDetails(EditQuestionViewModel qvm)
        {
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<EditQuestionViewModel, Question>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Question q = mapper.Map<EditQuestionViewModel, Question>(qvm);
            qr.UpdateQuestionDetails(q);
        }
        public void UpdateQuestionVotesCount(int qid, int value)
        {
            qr.UpdateQuestionVotesCount(qid, value);
        }
        public void UpdateQuestionAnswersCount(int qid, int value)
        {
            qr.UpdateQuestionAnswersCount(qid, value);
        }
        public void UpdateQuestionViewsCount(int qid, int value)
        {
            qr.UpdateQuestionViewsCount(qid, value);
        }
        public void DeleteQuestion(int qid)
        {
            qr.DeleteQuestion(qid);
        }
        public List<QuestionViewModel> GetQuestions()
        {
            List<Question> q = qr.GetQuestions();
            MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<Question, QuestionViewModel>(); cfg.IgnoreUnmapped(); cfg.CreateMap<User, UserViewModel>();cfg.IgnoreUnmapped(); cfg.CreateMap<Category, CategoryViewModel>();cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<QuestionViewModel> qvm = mapper.Map<List<Question>, List<QuestionViewModel>>(q);
            return qvm;
        }
        public QuestionViewModel GetQuestionByQuestionID(int QuestionID, int UserID)
        {
           Question q = qr.GetQuestionByQuestionID(QuestionID).FirstOrDefault();
            QuestionViewModel qvm = null;
            if (q != null)
            {
                MapperConfiguration config = new MapperConfiguration(cfg => { cfg.CreateMap<Question, QuestionViewModel>(); cfg.IgnoreUnmapped(); cfg.CreateMap<User, UserViewModel>(); cfg.IgnoreUnmapped(); cfg.CreateMap<Category, CategoryViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                qvm = mapper.Map<Question, QuestionViewModel>(q);

                foreach (var item in qvm.Answers)
                {
                    item.CurrentUserVoteType = 0;

                    VoteViewModel vote = item.Votes.Where(s => s.UserID == UserID).FirstOrDefault();
                    if (vote != null)
                    {
                        item.CurrentUserVoteType = vote.VoteValue;
                    }

                }
            }
            return qvm;
        }
    }
}
